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
            SizeChanged += Window_SizeChanged;
        }
        
        private void LaunchGitHubSite(object sender, RoutedEventArgs e)
        {
            // Launch the GitHub site...
        }

        private void DeployCupCakes(object sender, RoutedEventArgs e)
        {
            // deploy some CupCakes...
        }
        private void Window_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            // Calculate proportional column widths
            double totalWeight = 1.2 + 3 + 2 + 1 + 1 + 1 + 1; // Total weight of all columns
            double column1Width = (GeoGrid.ActualWidth / totalWeight) * 1.2;
            double column2Width = (GeoGrid.ActualWidth / totalWeight) * 3;
            double column3Width = (GeoGrid.ActualWidth / totalWeight) * 2;
            double column4Width = (GeoGrid.ActualWidth / totalWeight) * 1;
            double column5Width = (GeoGrid.ActualWidth / totalWeight) * 1;
            double column6Width = (GeoGrid.ActualWidth / totalWeight) * 1;
            double column7Width = (GeoGrid.ActualWidth / totalWeight) * 1;

            // Set calculated column widths
            GridViewColumn1.Width = column1Width;
            GridViewColumn2.Width = column2Width;
            GridViewColumn3.Width = column3Width;
            GridViewColumn4.Width = column4Width;
            GridViewColumn5.Width = column5Width;
            GridViewColumn6.Width = column6Width;
            GridViewColumn7.Width = column7Width;
        }
    }
}