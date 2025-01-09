using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SampleApp.Models
{
    public class RegistrationModel
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public DateTime DOB { get; set; }
        public string Gender { get; set; }
        public int StateId { get; set; }
        public string StateName { get; set; }
        public string Phone { get; set; }
        public DateTime? CreatedDate { get; set; }
    }
}