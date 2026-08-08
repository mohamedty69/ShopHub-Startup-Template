using System;
using System.Collections.Generic;
using System.Text;

namespace myshop.DAL.Models
{
    public interface ISoftDelete
    {
        public bool IsDeleted { get; set; }
    }
}
