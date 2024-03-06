using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;
using Newtonsoft.Json;

namespace GeoFuel.Model
{
    public class Manager
    {
        public ObservableCollection<gas_station> _DatabaseStation = new ObservableCollection<gas_station>()
        {
        };
        public ObservableCollection<gas_station> GetStations()
        {
            return _DatabaseStation;
        }
    }

    
}
