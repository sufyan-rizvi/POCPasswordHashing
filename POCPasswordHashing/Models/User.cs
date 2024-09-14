using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace POCPasswordHashing.Models
{
    public class User
    {
        public virtual int Id { get; set; }
        [Required]
        public virtual string Name {  get; set; }
        [Required]
        [PasswordPropertyText]
        public virtual string Password { get; set; }
    }
}