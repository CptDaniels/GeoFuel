using GeoFuel.Model;
using System;
using System.IO;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Net.Http;
using System.Runtime.CompilerServices;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Controls;
using GalaSoft.MvvmLight.Command;
using System.Windows.Data;
using System.Windows.Interactivity;
using System.Reflection.Metadata;

namespace GeoFuel.ViewModel
{
    public class MainViewModel : INotifyPropertyChanged
    {
        public static readonly string URI = "https://api.ure.gov.pl";
        private readonly string username = "cptdaniels";
        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        

        private Manager manager;
        private GeoManager geoManager;
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
        private List<string> _postCodes;
        public List<string> PostCodes
        {
            get { return _postCodes; }
            set
            {
                _postCodes = value;
                OnPropertyChanged();
            }
        }
        private string _postcode;
        public string Postcode
        {
            get { return _postcode; }
            set
            {
                _postcode = value;
                OnPropertyChanged();
            }
        }
        private string _coords;
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
                    // Assign the first part to the Latitude property
                    Lat = parts[0].Trim();
                    // Assign the second part to the Longitude property
                    Lng = parts[1].Trim();
                }
            }
        }
        private string _lat;
        public string Lat
        {
            get { return _lat; }
            set
            {
                _lat = value;
                OnPropertyChanged();
            }
        }

        private string _lng;
        public string Lng
        {
            get { return _lng; }
            set
            {
                _lng = value;
                OnPropertyChanged();
            }
        }
        public ICommand LoadStationsCommand { get; }
        public ICommand FilterStationsCommand { get; }
        public ICommand FilterPostalCodesCommand{ get; }
        public MainViewModel()
        {
            
            manager = new Manager();
            geoManager = new GeoManager();
            LoadStationsCommand = new RelayCommand(async () => await LoadStations());
            LoadStationsCommand.Execute(null);
            FilterStationsCommand = new RelayCommand(async () => await FilterStations());
            FilterPostalCodesCommand = new RelayCommand(async () => await FilterPostalCodes());
        }

        private async Task LoadStations()
        {
            using (HttpClient client = new HttpClient())
            {
                try
                {
                    //string kod = "32-650";
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

        private void ResetCollection()
        {
            GasStations.Clear();
        }


        private async Task FilterPostalCodes()
        {
            try
            {
                List<string> postCodesList = await geoManager.LoadPostCodes(Lat,Lng);
        
                ObservableCollection<gas_station> stations = await manager.FilterAndDeserializeListJsonToListAsync("fuelstations.json", postCodesList);
                GasStations = stations;
        
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error filtering stations: {ex.Message}");
            }
        }
        private async Task FilterStations()
        {
            try
            {
                ObservableCollection<gas_station> stations = await manager.FilterAndDeserializeJsonToListAsync("fuelstations.json", Postcode);
                GasStations = stations;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error filtering stations: {ex.Message}");
            }
        }
    }
    
}
