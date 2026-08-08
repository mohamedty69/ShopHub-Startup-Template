using System;
using System.Collections.Generic;
using System.Text;

namespace myshop.DAL.Models
{
    public interface IAuditable
    {
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
