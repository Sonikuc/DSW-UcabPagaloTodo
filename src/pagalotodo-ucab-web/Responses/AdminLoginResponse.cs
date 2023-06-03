using System;

public class AdminLoginResponse
{
    public string? Username { get; set; }
    public string? Password { get; set; }
    public bool? Success { get; set; }// indica si la conexion fue exitosa o no
    public string? Message { get; set; }// Mensaje de respuesta del servidor
    public string? Token { get; set; }// Token de autenticacion del usuario
    public string? ID { get; set; } // ID del usuario en la BD

}