using CinemaCritic.Web.Services.Contracts;
using CinemaCritic.Web.Models;
using Blazored.SessionStorage;
using CinemaCritic.Models.Dto;
using System.Net.Http;
using System;
using Microsoft.Extensions.Logging;
using System.Net.Http.Json; // Add this namespace for ILogger

namespace CinemaCritic.Web.Services
{
    public class UserTakenException : Exception
    {
        public UserTakenException(string message) : base(message)
        {
        }
    }

    public class FatalRegisterException : Exception
    {
        public FatalRegisterException(string message) : base(message)
        {
        }
    }



    public class RegisterService : IRegisterService
    {
        private readonly HttpClient _httpClient;

        public RegisterService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task RegisterAsync(RegisterModel model)
        {
            //Need to implement function which finds if user already exists
            try
            {
                var users = await _httpClient.GetFromJsonAsync<UserDto[]>("api/user");
                foreach (var user in users)
                {
                    if (user.Username == model.UserName)
                    {
                        throw new UserTakenException("User already exists.");
                    }
                }
            }
            catch (UserTakenException ex)
            {
                throw new UserTakenException(ex.Message); // Re-throw the exception for handling it in the caller method
            }

            try
            {
                var newUser = new UserDto
                {
                    //autoincrement Id for users
                    Username = model.UserName,
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    Email = model.Email,
                    Password = model.Password,
                };

                var response = await _httpClient.PostAsync($"api/WebApiAuthentication/Register", JsonContent.Create(model));

                if (response.IsSuccessStatusCode)
                {
                    // You can return some response or just return void
                }
                else
                {
                    var errorMessage = await response.Content.ReadAsStringAsync();
                    throw new FatalRegisterException($"Failed to create user: {errorMessage}");
                }
            }
            catch (Exception ex)
            {
                throw new FatalRegisterException(ex.ToString()); // Re-throw the exception for handling it in the caller method
            }
        }
    }
}
