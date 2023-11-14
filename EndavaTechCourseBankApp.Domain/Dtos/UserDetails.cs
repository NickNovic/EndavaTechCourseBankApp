using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EndavaTechCourseBankApp.Domain.Dtos
{
    public class UserDetails 
    {
        public string Id { get; set; }
        public string Username { get; set; }
        public string[] Roles {  get; set; }
    }
}
