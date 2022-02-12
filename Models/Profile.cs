using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace portfolioWebAPI.Models
{
    public class Profile
    {
        public int ProfileId { get; set; }
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public string Adress { get; set; }
        public string ZipeCode { get; set; }
        public string Bitrhday { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string ImgUrl { get; set; }
        public string CvUrl { get; set; }
        public string Job { get; set; }
    }
}
