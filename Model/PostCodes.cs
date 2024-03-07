using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeoFuel.Model
{
    public class PostCodes
    {
        public string postalcode {get; set;}
        public double lat {get; set;}
        public double lng {get; set;}
        public string adminCode2 { get; set; }
        public string adminCode3 { get; set; }
        public string adminName3 { get; set; }
        public string adminCode1 { get; set; }
        public string adminName2 { get; set; }
        public string countryCode { get; set; }
        public string adminName1 { get; set; }
        public string placeName { get; set; }

    }
    public class PostalCodeResponse
    {
        public List<PostCodes> postalCodes { get; set; }
    }
}
