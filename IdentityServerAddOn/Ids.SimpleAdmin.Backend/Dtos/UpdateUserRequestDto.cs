﻿namespace Ids.SimpleAdmin.Backend.Dtos
{
    public class UpdateUserRequestDto
    {
        public string Userid { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string Phonenumber { get; set; }
        public bool Use2Fa { get; set; }
        public bool ConfirmEmail { get; set; }
        public bool ConfirmPhoneNumber { get; set; }
        public bool EnableLockout { get; set; }
        public bool EndLockout { get; set; }
    }
}
