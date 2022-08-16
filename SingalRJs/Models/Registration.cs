using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SingalRJs.Models
{
    public class Registration
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public DateTime NewAccountDate { get; set; }
        public string Password2 { get; set; }
        public string Password3 { get; set; }
        public int IsActive { get; set; }
        public int IsLoggedin { get; set; }
    }
}