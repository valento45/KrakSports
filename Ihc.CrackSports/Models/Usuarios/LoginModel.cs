﻿namespace Ihc.CrackSports.WebApp.Models.Usuarios
{
    public class LoginModel
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public string PasswordHash { get; set; }
        

        public bool PreenchidoCorretamente()
        {
            return !string.IsNullOrEmpty(UserName) && !string.IsNullOrEmpty(PasswordHash);
        }



    }
}
