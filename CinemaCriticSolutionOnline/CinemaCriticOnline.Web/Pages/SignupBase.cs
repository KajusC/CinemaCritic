using CinemaCritic.Web.Models;
using CinemaCritic.Web.Services;
using CinemaCritic.Web.Services.Contracts;
using Microsoft.AspNetCore.Components;
using System;
using System.Threading.Tasks;

namespace CinemaCritic.Web.Pages
{
    public class SignupBase : ComponentBase
    {
        protected string FirstName { get; set; }
        protected string LastName { get; set; }
        protected string UserName { get; set; }
        protected string Email { get; set; }
        protected string Password { get; set; }
        protected string confirmPassword;

        [Inject]
        protected IRegisterService RegisterService { get; set; }

        protected virtual async Task HandleValidSubmit()
        {
            
            var model = new RegisterModel
            {
                FirstName = FirstName,
                LastName = LastName,
                UserName = UserName,
                Email = Email,
                Password = Password
            };

            if (model.Password != confirmPassword)
            {
                // Handle password mismatch
                return;
            }

            try
            {
                // Call the RegisterAsync method of RegisterService
                await RegisterService.RegisterAsync(model);

                // Registration successful, redirect or show success message
                // You can redirect to another page or show a success message here
            }
            catch (Exception ex)
            {
                // Handle registration failure
                // You can display an error message to the user or log the error
            }
        }
    }
}
