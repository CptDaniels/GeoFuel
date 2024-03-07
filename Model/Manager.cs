﻿using System;
using System.IO;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;
using Newtonsoft.Json;
using System.Linq;

namespace GeoFuel.Model
{
    public class Manager
    {
        public ObservableCollection<gas_station> _DatabaseStation { get; set; } = new ObservableCollection<gas_station>()
        {
        };
        public ObservableCollection<gas_station> GetStations()
        {
            return _DatabaseStation;
        }
        public ObservableCollection<gas_station> FilterAndDeserializeJsonToList(string jsonFilePath, string filter)
        {
            string jsonContent = File.ReadAllText(jsonFilePath);
            List<gas_station> dataList = JsonConvert.DeserializeObject<List<gas_station>>(jsonContent);
            var filteredData = new ObservableCollection<gas_station>(dataList.Where(station => station.infraKod == filter));
            return filteredData;
        }
    }

    
}
