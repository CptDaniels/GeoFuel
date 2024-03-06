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

namespace GeoFuel
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : MetroWindow
    {
        public static readonly String URI = "https://api.ure.gov.pl";
        public MainWindow()
        {
            InitializeComponent();
            //LoadStations();
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
                    string postcode = textBox.Text;
                    //string kod = "32-650";
                    HttpResponseMessage response = await client.GetAsync($"{URI}/api/InfrastructureFuelStation?InfraKod={postcode}");
                    response.EnsureSuccessStatusCode();
                    
                    var stations = await response.Content.ReadAsAsync<List<gas_station>>();
                    textBlock.Text = textBox.Text;
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

        private void TextBox(object sender, TextCompositionEventArgs e)
        {
        }
    }
}