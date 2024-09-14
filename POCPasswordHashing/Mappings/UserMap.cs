using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FluentNHibernate.Mapping;
using POCPasswordHashing.Models;

namespace POCPasswordHashing.Mappings
{
    public class UserMap:ClassMap<User>
    {
        public UserMap()
        {
            Table("Users");
            Id(u=>u.Id).GeneratedBy.Identity();
            Map(u => u.Name);
            Map(u => u.Password);
        }
    }
}