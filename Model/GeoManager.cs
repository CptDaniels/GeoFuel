using Newtonsoft.Json;
using System;
using System.IO;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.WebRequestMethods;
using System.Net.Http;
using System.Security.Policy;
using System.Windows;
using System.Net.Http.Json;

namespace GeoFuel.Model
{
    public class GeoManager
    {
        public ObservableCollection<PostCodes> _postCodes { get; set; } = new ObservableCollection<PostCodes> 
        { 
        };

        public ObservableCollection<PostCodes> GetPostCodes()
        {
            return _postCodes;
        }
        //http://api.geonames.org/findNearbyPostalCodes?lat=49.87470783&lng=19.22661408&country=PL&radius=30&username=cptdaniels
        
    }
}
