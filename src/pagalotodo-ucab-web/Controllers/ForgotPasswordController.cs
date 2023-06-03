using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Diagnostics;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using UCABPagaloTodoWeb.Models;
using Newtonsoft.Json.Linq;
using UCABPagaloTodoWeb.Responses;
using System.Reflection;

namespace UCABPagaloTodoWeb.Controllers
{
    public class ForgotPasswordController : Controller 
    {
        private readonly ILogger<ForgotPasswordController> _logger;
        private HttpClient _httpClient;
       // private string? _email;
        public ForgotPasswordController(ILogger<ForgotPasswordController> logger)
        {
            _logger = logger;
            _httpClient = new HttpClient();
           
        }
        public IActionResult InsertEmail( )
        {
            InsertEmailModel model = new InsertEmailModel();
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> SendEmail(string email)
        {
            var apiUrl = "https://localhost:44339/api/recoverypassword/recoverypassword";
            TempData["correo"]= email;
            var requestBody = new { Email = email };
            var jsonBody = JsonConvert.SerializeObject(requestBody, new JsonSerializerSettings
            {
                    NullValueHandling = NullValueHandling.Ignore
            });
            var response = await _httpClient.PostAsync(apiUrl, new StringContent(jsonBody, Encoding.UTF8, "application/json"));
                // Envía la solicitud POST con el body en formato JSON
             if (response.IsSuccessStatusCode)
                {
                    var responseContent = await response.Content.ReadAsStringAsync();
                    var forgotPasswordResponse= JsonConvert.DeserializeObject<ForgotPasswordResponse>(responseContent);
                    if (forgotPasswordResponse != null && forgotPasswordResponse.Send == true)
                    {

                    return RedirectToAction("InsertVerificationCode");// , "ForgotPassword");// enviar a la vista para ingresar codigo de verificacion
                    }
                }
            
                return RedirectToAction("InsertEmail");
               

        }

        public IActionResult InsertVerificationCode()
        {
                      

            InsertVerificationCodeModel model = new InsertVerificationCodeModel();
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> InsertCode(string VerificationCode)
        {
          
                string? correo = TempData["correo"] as string;

                var apiUrl = "https://localhost:44339/api/recoverypassword/verifycode";
                var requestBody = new { VerificationCode = VerificationCode, Email=correo };
                var jsonBody = JsonConvert.SerializeObject(requestBody, new JsonSerializerSettings
                {
                    NullValueHandling = NullValueHandling.Ignore
                });
                var response = await _httpClient.PostAsync(apiUrl, new StringContent(jsonBody, Encoding.UTF8, "application/json"));
                // Envía la solicitud POST con el body en formato JSON
                if (response.IsSuccessStatusCode)
                {
                    var responseContent = await response.Content.ReadAsStringAsync();
                    var codeResponse = JsonConvert.DeserializeObject<InsertCodeVerificationResponse>(responseContent);
                    if (codeResponse != null && codeResponse.Veryfy == true)
                    {
                        return RedirectToAction("ChangePassword"); // enviar a la vista para ingresar codigo de verificacion
                    }
                }

                return RedirectToAction("InsertVerificationCode");
            

        }

        public IActionResult ChangePassword()
        {
            NewPasswordModel model = new NewPasswordModel();
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> NewPassword(string Password, string ConfirmPassword)
        {
            string? correo = TempData["correo"] as string;

            var apiUrl = "https://localhost:44339/api/recoverypassword/changepassword";
            var requestBody = new { password = Password, verifyPassword = ConfirmPassword, email = correo }; // valores del request
            var jsonBody = JsonConvert.SerializeObject(requestBody, new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore
            });
            var response = await _httpClient.PostAsync(apiUrl, new StringContent(jsonBody, Encoding.UTF8, "application/json"));
            // Envía la solicitud POST con el body en formato JSON
            if (response.IsSuccessStatusCode)
            {
                var responseContent = await response.Content.ReadAsStringAsync();
                var codeResponse = JsonConvert.DeserializeObject<InsertCodeVerificationResponse>(responseContent);
                if (codeResponse != null && codeResponse.Veryfy == true)
                {
                    return RedirectToAction("LoginView","Login"); // enviar a la vista para ingresar codigo de verificacion
                }
            }

            return RedirectToAction("ChangePassword");
        }

    }
}
