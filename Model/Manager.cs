using System.IO;
using System.Collections.ObjectModel;
using Newtonsoft.Json;
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
        public async Task<List<string>> GetListFromStations(string jsonFilePath)
        {
            try
            {
                string jsonContent = await File.ReadAllTextAsync(jsonFilePath);
                List<gas_station> dataList = JsonConvert.DeserializeObject<List<gas_station>>(jsonContent);
                List<string> cities = dataList
                    .Where(station => !string.IsNullOrEmpty(station.infraPoczta))
                    .Select(station => station.infraPoczta.Trim())
                    .Distinct(StringComparer.OrdinalIgnoreCase)
                    .Select(city => char.ToUpper(city[0]) + city.Substring(1).ToLower())
                    .ToList();
                return cities;
            }
            catch (Exception ex)
            {
                return new List<string>();
            }
        }
        public List<string> FilterCities(List<string> allCities, string searchTerm)
        {
            return allCities
                .Where(city => city.StartsWith(searchTerm, StringComparison.OrdinalIgnoreCase))
                .ToList();
        }
        public async Task<ObservableCollection<gas_station>> FilterAndDeserializeJsonToListAsync(string jsonFilePath, string filter)
        {
            try
            {
                string jsonContent = await File.ReadAllTextAsync(jsonFilePath);
                List<gas_station> dataList = JsonConvert.DeserializeObject<List<gas_station>>(jsonContent);
                var filteredData = new ObservableCollection<gas_station>(
                dataList.Where(station => string.Equals(station.infraPoczta, filter, StringComparison.OrdinalIgnoreCase))
        );
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
                return new ObservableCollection<gas_station>();
            }
        }
    }
    

    
}
