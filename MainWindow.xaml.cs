using GeoFuel.Model;
using System.IO;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using MahApps.Metro.Controls;
using System.Collections.ObjectModel;
using Newtonsoft.Json;

namespace GeoFuel
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    /// http://api.geonames.org/findNearbyPostalCodes?lat=49.87470783&lng=19.22661408&country=PL&radius=30&username=cptdaniels
    public partial class MainWindow : MetroWindow
    {
        Manager manager;
        public ObservableCollection<gas_station> GasStations { get; set; }
        public static readonly String URI = "https://api.ure.gov.pl";
        public MainWindow()
        {
            InitializeComponent();
            LoadStations();
            manager = new Manager();
            DataContext = this;
            GasStations = new ObservableCollection<gas_station>();

        }
        private void LaunchGitHubSite(object sender, RoutedEventArgs e)
        {
            // Launch the GitHub site...
        }

        private void DeployCupCakes(object sender, RoutedEventArgs e)
        {
            // deploy some CupCakes...
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string postcode = textBox.Text;
            ObservableCollection<gas_station> stations = manager.FilterAndDeserializeJsonToList("fuelstations.json", postcode);
            ResetCollection();
            if (stations != null)
            {
                foreach (var station in stations)
                {
                    GasStations.Add(station);
                }
            }
            else
            {
                Console.WriteLine("No gas stations found.");
            }   
            
            
        }
        private void ResetCollection()
        {
            GasStations.Clear();
        }
        private async void LoadStations()
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

        private void TextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                Button_Click(sender, e);
            }
        }
        private void TextBox_KeyDown2(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                Button_Click(sender, e);
            }
        }

    }
}