using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace myshop.BLL.DTOs.User
{
    public class LoginDTO
    {
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; } = string.Empty;
        [DataType(DataType.Password)]
        public string Password { get; set; } = string.Empty;
        public bool IsPersistent { get; set; } = false;
    }
}
