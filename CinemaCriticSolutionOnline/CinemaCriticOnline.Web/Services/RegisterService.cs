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
    public class RegisterService : IRegisterService
    {
        private readonly HttpClient _httpClient;

        public RegisterService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task RegisterAsync(RegisterModel model)
        {
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

                var response = await _httpClient.PostAsync($"api/Authentication/Register", JsonContent.Create(model));

                if (response.IsSuccessStatusCode)
                {
                    // You can return some response or just return void
                }
                else
                {
                    var errorMessage = await response.Content.ReadAsStringAsync();
                    throw new Exception($"Failed to create user: {errorMessage}");
                }
            }
            catch (Exception ex)
            {
                throw; // Re-throw the exception for handling it in the caller method
            }
        }
    }
}
