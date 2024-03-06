using GeoFuel.Model;
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

namespace GeoFuel
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : MetroWindow
    {
        public ObservableCollection<gas_station> GasStations { get; set; }
        public static readonly String URI = "https://api.ure.gov.pl";
        public MainWindow()
        {
            InitializeComponent();
            //LoadStations();
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

        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            using (HttpClient client = new HttpClient())
            {
               try
                {
                    ResetCollection();
                    string postcode = textBox.Text;
                    //string kod = "32-650";
                    HttpResponseMessage response = await client.GetAsync($"{URI}/api/InfrastructureFuelStation?InfraKod={postcode}");
                    response.EnsureSuccessStatusCode();
                    
                    var stations = await response.Content.ReadAsAsync<List<gas_station>>();
                    //textBlock.Text = stations.ToString();
                    //Console.WriteLine($"GET https://api.ure.gov.pl/api/InfrastructureFuelStation?InfraKod={kod} HTTP/1.1");
                    if (stations != null)
                    {
                        foreach (var station in stations)
                        {
                            //Console.WriteLine(station);
                            GasStations.Add(station);
                        }
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
        async void LoadStations()
        {
            using (HttpClient client = new HttpClient())
            {
                try
                {
                    string kod = "32-650";
                    HttpResponseMessage response = await client.GetAsync($"{URI}/api/InfrastructureFuelStation?InfraKod={kod}");
                    response.EnsureSuccessStatusCode();
                    
                    var stations = await response.Content.ReadAsAsync<List<gas_station>>();

                    //Console.WriteLine($"GET https://api.ure.gov.pl/api/InfrastructureFuelStation?InfraKod={kod} HTTP/1.1");
                    if (stations != null)
                    {
                        foreach (var station in stations)
                        {
                            Console.WriteLine(station);
                        }
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

    }
}