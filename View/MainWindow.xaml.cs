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
using GeoFuel.ViewModel;
using System.Globalization;

namespace GeoFuel.View
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    /// http://api.geonames.org/findNearbyPostalCodes?lat=49.87470783&lng=19.22661408&country=PL&radius=30&username=cptdaniels
    public partial class MainWindow : MetroWindow
    {
        
        public MainWindow()
        {
            InitializeComponent();
            DataContext = new MainViewModel();
        }
        
        private void LaunchGitHubSite(object sender, RoutedEventArgs e)
        {
            // Launch the GitHub site...
        }

        private void DeployCupCakes(object sender, RoutedEventArgs e)
        {
            // deploy some CupCakes...
        }


        private void TextBox_KeyDown(object sender, KeyEventArgs e)
        {
            //if (e.Key == Key.Enter)
            //{
            //    UpdateSource();
            //}
        }
        
        private void TextBox_KeyDown2(object sender, KeyEventArgs e)
        {
            //if (e.Key == Key.Enter)
            //{
            //    Button_Click(sender, e);
            //}
        }

    }
}