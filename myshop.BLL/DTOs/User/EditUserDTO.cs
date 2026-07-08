using System;
using System.Collections.Generic;
using System.Text;

namespace myshop.BLL.DTOs.User
{
    public class EditUserDTO
    {
        public string UserID { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string UserName { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string Role { get; set; }
        public bool IsLocked { get; set; } = false;
    }
}
