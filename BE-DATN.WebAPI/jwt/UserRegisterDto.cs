﻿namespace BE_DATN.WebAPI.jwt
{
    public class UserRegisterDto
    {
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string FullName { get; set; }

        public string Role { get; set; }
        public bool IsAdmin { get; set; }
    }
}
