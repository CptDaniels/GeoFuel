using System;
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
using System.Windows.Input;
using System.Security.Policy;
using System.Windows;
using System.ComponentModel.DataAnnotations;

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
        public async Task<List<string>> GetListFromStations(string jsonFilePath, string searchTerm)
        {
            try
            {
                string jsonContent = await File.ReadAllTextAsync(jsonFilePath);
                List<gas_station> dataList = JsonConvert.DeserializeObject<List<gas_station>>(jsonContent);
                List<string> data = dataList
                .Where(station => station.infraPoczta.StartsWith(searchTerm, StringComparison.OrdinalIgnoreCase))
                .Select(station => station.infraPoczta)
                .ToList();
                return data;
            }
            catch (Exception ex)
            {
                return new List<string>();
            }
        }
        public async Task<ObservableCollection<gas_station>> FilterAndDeserializeJsonToListAsync(string jsonFilePath, string filter)
        {
            try
            {
                string jsonContent = await File.ReadAllTextAsync(jsonFilePath);
                List<gas_station> dataList = JsonConvert.DeserializeObject<List<gas_station>>(jsonContent);
                var filteredData = new ObservableCollection<gas_station>(dataList.Where(station => station.infraKod == filter));
                return filteredData;
            }
            catch (Exception ex)
            {
                return new ObservableCollection<gas_station>();
            }
        }
        public async Task<ObservableCollection<gas_station>> FilterAndDeserializeListJsonToListAsync(string jsonFilePath, List<string> filter)
        {
            try
            {
                string jsonContent = await File.ReadAllTextAsync(jsonFilePath);
                List<gas_station> dataList = JsonConvert.DeserializeObject<List<gas_station>>(jsonContent);
                var filteredData = new ObservableCollection<gas_station>(dataList.Where(station =>
                    filter.Contains(station.infraKod)));
                return filteredData;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return new ObservableCollection<gas_station>();
            }
        }
    }
    

    
}
