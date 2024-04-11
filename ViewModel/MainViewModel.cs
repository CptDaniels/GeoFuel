using GeoFuel.Model;
using System.IO;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Net.Http;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Input;
using GalaSoft.MvvmLight.Command;
using Newtonsoft.Json;

namespace GeoFuel.ViewModel
{
    public class MainViewModel : INotifyPropertyChanged
    {
        public static readonly string URI = "https://api.ure.gov.pl";
        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        private string searchText;
        private string selectedCity;
        private string _postcode;
        private List<string> _allCities;
        private string _coords;
        private string _lat;
        private string _lng;
        private Manager manager;
        private ObservableCollection<string> searchList;
        private ObservableCollection<gas_station> _gasStations;

        public ObservableCollection<gas_station> GasStations
        {
            get { return _gasStations; }
            set
            {
                _gasStations = value;
                OnPropertyChanged();
            }
        }
        public List<string> Allcities
        {
            get { return _allCities; }
            set
            {
                _allCities = value;
                OnPropertyChanged();
            }
        }
        public ObservableCollection<string> SearchList
        {
            get { return searchList; }
            set
            {
                searchList = value;
                OnPropertyChanged(nameof(SearchList));
            }
        }
        public string SelectedCity
        {
            get { return selectedCity; }
            set
            {
                selectedCity = value;
                OnPropertyChanged(nameof(SelectedCity));
            }
        }
        public string Postcode
        {
            get { return _postcode; }
            set
            {
                _postcode = value;
                OnPropertyChanged();
            }
        }
        public string Coords
        {
            get { return _coords; }
            set
            {
                _coords = value;
                OnPropertyChanged();
                string[] parts = value.Split(',');
                if (parts.Length == 2)
                {
                    Lat = parts[0].Trim();
                    Lng = parts[1].Trim();
                }
            }
        }
        public string Lat
        {
            get { return _lat; }
            set
            {
                _lat = value;
                OnPropertyChanged();
            }
        }
        public string Lng
        {
            get { return _lng; }
            set
            {
                _lng = value;
                OnPropertyChanged();
            }
        }
        public string SearchText
        {
            get { return searchText; }
            set
            {
                searchText = value;
                OnPropertyChanged(nameof(SearchText));
                HandleSearchTextChanged();
            }
        }
        public ICommand LoadStationsCommand { get; }
        public ICommand FilterStationsCommand { get; }
        public ICommand LoadPostCodesCommand { get; }
        public ICommand GetLocationCommand { get; }
        public MainViewModel()
        {
            
            manager = new Manager();
            LoadStationsCommand = new RelayCommand(async () => await LoadStations());
            LoadStationsCommand.Execute(null);
            FilterStationsCommand = new RelayCommand(async () => await FilterStations());
            LoadPostCodesCommand = new RelayCommand(async () => await LoadPostCodes());
            GetLocationCommand = new RelayCommand(async () => await GetLocationAndLoadPostCodes());
        }
        private async Task GetLocation()
        {
            using (HttpClient client = new HttpClient())
            {
                try
                {
                    HttpResponseMessage response = await client.GetAsync("http://ip-api.com/json/");
                    response.EnsureSuccessStatusCode();
                    var location = await response.Content.ReadAsStringAsync();
                    Location? locationResponse = JsonConvert.DeserializeObject<Location>(location);
                    Lat = locationResponse.lat;
                    Lng = locationResponse.lon;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
        }
        private async Task LoadStations()
        {
            using (HttpClient client = new HttpClient())
            {
                try
                {
                    HttpResponseMessage response = await client.GetAsync($"{URI}/api/InfrastructureFuelStation?");
                   
                    response.EnsureSuccessStatusCode();
                    
                    var stations = await response.Content.ReadAsStringAsync();

                    //Console.WriteLine($"GET https://api.ure.gov.pl/api/InfrastructureFuelStation HTTP/1.1");
                    if (stations != null)
                    {
                        File.WriteAllText("fuelstations.json", stations);
                        Console.WriteLine("Json Downloaded.");
                    }
                    else
                    {
                        Console.WriteLine("No gas stations found.");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }
        private async Task LoadPostCodes()
        {
            using (HttpClient client = new HttpClient())
            {
                try
                {
                    //Lat = lat;
                    //Lng = lng;
                    HttpResponseMessage response = await client.GetAsync($"http://api.geonames.org/findNearbyPostalCodesJSON?lat={Lat}&lng={Lng}&country=PL&radius=5&maxRows=100&username=cptdaniels");
                   
                    response.EnsureSuccessStatusCode();
                    
                    var postalcodes = await response.Content.ReadAsStringAsync();
                    var postalCodeResponse = JsonConvert.DeserializeObject<PostalCodeResponse>(postalcodes);
                    var postCodesList = new List<string>(postalCodeResponse?.postalCodes?.Select(code => code.postalcode)?.ToList());
                    ObservableCollection<gas_station> stations = await manager.FilterAndDeserializeListJsonToListAsync("fuelstations.json", postCodesList);
                    GasStations = stations;
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error filtering stations: {ex.Message}");
                }
            }
        }
        private async Task FilterStations()
        {
            try
            {
                ObservableCollection<gas_station> stations = await manager.FilterAndDeserializeJsonToListAsync("fuelstations.json", SearchText);
                GasStations = stations;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error filtering stations: {ex.Message}");
            }
        }
        private async Task GetLocationAndLoadPostCodes()
        {
            await GetLocation();
            await LoadPostCodes();
        }
        private async void HandleSearchTextChanged()
        {
            if (!string.IsNullOrEmpty(SearchText))
            {
                if (_allCities == null || !_allCities.Any())
                {
                    _allCities = await manager.GetListFromStations("fuelstations.json");
                }

                List<string> filteredCities = manager.FilterCities(_allCities, SearchText);
                SearchList = new ObservableCollection<string>(filteredCities);
            }
            else
            {
                SearchList.Clear();
            }

        }
    }
    
}
