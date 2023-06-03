using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Diagnostics;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using UCABPagaloTodoWeb.Models;
using UCABPagaloTodoMS.Application.Responses;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json.Linq;
using System.Security.Cryptography.X509Certificates;

namespace UCABPagaloTodoWeb.Controllers
{
    public class LoginController : Controller
    {
        private readonly ILogger<LoginController> _logger;
        private HttpClient _httpClient;

        public LoginController(ILogger<LoginController> logger)
        {
            _logger = logger;
            _httpClient = new HttpClient();
        }




        public IActionResult LoginView()
        {
            LoginViewModel model = new LoginViewModel();
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Login(string username, string password)
        {
            // Si las credenciales son válidas, redirigir al usuario a otra página
            var apiUrl = "https://localhost:44339/api/login/login";
            var requestBody = new { UserName = username, Password = password };
            var jsonBody = JsonConvert.SerializeObject(requestBody, new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore
            }); // Serializa el body a formato JSON
            var response = await _httpClient.PostAsync(apiUrl, new StringContent(jsonBody, Encoding.UTF8, "application/json"));
            // Envía la solicitud POST con el body en formato JSON
            if (response.IsSuccessStatusCode)
            {
                var responseContent = await response.Content.ReadAsStringAsync();
                var loginResponse = JsonConvert.DeserializeObject<UserLoginResponse>(responseContent);
                if (loginResponse != null && loginResponse.Success == true)
                {
                    if (loginResponse.isAdmin == true) {
                        HttpContext.Session.SetString("UserId", loginResponse.Id.ToString());
                        return View("~/Views/Administration/AdminHomeView.cshtml");
                    }
                    if (loginResponse.Status == false) { 
                        return View("~/Views/Login/BlockedUser.cshtml");
                    }

                    if (loginResponse.isProvider == true)
                    {
                        HttpContext.Session.SetString("UserId", loginResponse.Id.ToString());
                        HttpContext.Session.SetString("UserName", loginResponse.UserName.ToString());
                        return View("~/Views/Provider/HomeProvider.cshtml"); //cambiar para que retorne a una vista de prestador de servicios
                    }

                    HttpContext.Session.SetString("UserId", loginResponse.Id.ToString());
                    HttpContext.Session.SetString("UserName", loginResponse.UserName.ToString());
                    //Guid userId = Guid.Parse(HttpContext.Session.GetString("UserId"));
                    // Lo de arriba es para optener el GUID 
                    return RedirectToAction("Index", "Home");
                }
            }
            else
            {
                // Manejar errores aquí
            }
            return View("~/Views/Login/InvalidLogin.cshtml");

            /* 
                 // Si las credenciales son inválidas, mostrar un mensaje de error al usuario
                 ModelState.AddModelError("", "El nombre de usuario o la contraseña son incorrectos");
                 return View();
            */
        }

        public IActionResult SignUpView()
        {
            SignUpViewModel model = new SignUpViewModel();
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> SignUp(SignUpViewModel usuario)
        {
            // Si las credenciales son válidas, redirigir al usuario a otra página
            var apiUrl = "https://localhost:44339/api/signup/signupuser";
            var requestBody = new {
                DNI = usuario.DNI,  
                Name = usuario.Name,
                Lastname = usuario.LastName,
                Username = usuario.UserName,
                Email = usuario.Email,
                Password = usuario.Password,
                PhoneNumber = usuario.PhoneNumber
            };
            var jsonBody = JsonConvert.SerializeObject(requestBody, new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore
            }); // Serializa el body a formato JSON
            var response = await _httpClient.PostAsync(apiUrl, new StringContent(jsonBody, Encoding.UTF8, "application/json"));
            // Envía la solicitud POST con el body en formato JSON
            if (response.IsSuccessStatusCode)
            {
                var responseContent = await response.Content.ReadAsStringAsync();
                //var loginResponse = JsonConvert.DeserializeObject<UserSignUpResponse>(responseContent);
                //if (loginResponse != null)
                //{
                    return RedirectToAction("Index", "Home");
                //}
            }
            else
            {
                // Manejar errores aquí
            }
            return RedirectToAction("Privacy", "Home");

        }

        public IActionResult SignUpProviderView()
        {
            SignUpProviderViewModel model = new SignUpProviderViewModel();
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> SignUpProvider(SignUpProviderViewModel usuario)
        {
            // Si las credenciales son válidas, redirigir al usuario a otra página
            var apiUrl = "https://localhost:44339/api/signup/signupprovider";
            var requestBody = new
            {
                DNI = usuario.DNI,
                Name = usuario.Name,
                Lastname = usuario.LastName,
                Username = usuario.UserName,
                Email = usuario.Email,
                Password = usuario.Password,
                PhoneNumber = usuario.PhoneNumber,
                CompanyName = usuario.CompanyName
            };
            var jsonBody = JsonConvert.SerializeObject(requestBody, new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore
            }); // Serializa el body a formato JSON
            var response = await _httpClient.PostAsync(apiUrl, new StringContent(jsonBody, Encoding.UTF8, "application/json"));
            // Envía la solicitud POST con el body en formato JSON
            if (response.IsSuccessStatusCode)
            {
                var responseContent = await response.Content.ReadAsStringAsync();
                //var loginResponse = JsonConvert.DeserializeObject<UserSignUpResponse>(responseContent);
                //if (loginResponse != null)
                //{
                return RedirectToAction("Index", "Home");
                //}
            }
            else
            {
                // Manejar errores aquí
            }
            return RedirectToAction("Privacy", "Home");
        }

        public IActionResult Logout() 
        {
            HttpContext.Session.Clear();
            return RedirectToAction("LoginView", "Login");
        }
    }
}
        

