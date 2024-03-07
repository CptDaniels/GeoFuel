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
        public async Task<List<string>> LoadPostCodes(string lat, string lng)
        {
            using (HttpClient client = new HttpClient())
            {
                try
                {
                    HttpResponseMessage response = await client.GetAsync($"http://api.geonames.org/findNearbyPostalCodesJSON?lat={lat}&lng={lng}&country=PL&radius=30&username=cptdaniels");
                   
                    response.EnsureSuccessStatusCode();
                    
                    var postalcodes = await response.Content.ReadAsStringAsync();
                    var postalCodeResponse = JsonConvert.DeserializeObject<PostalCodeResponse>(postalcodes);
                    var postCodesList = new List<string>(postalCodeResponse?.postalCodes?.Select(code => code.postalcode)?.ToList());
                    return postCodesList;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                    return new List<string>();
                }
            }
        }
        
    }
}
