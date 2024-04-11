using System.Windows;
using MahApps.Metro.Controls;
using GeoFuel.ViewModel;

namespace GeoFuel.View
{
    //http://api.geonames.org/findNearbyPostalCodes?lat=49.87470783&lng=19.22661408&country=PL&radius=30&username=cptdaniels
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
            double totalWeight = 1.2 + 3 + 2 + 1 + 1 + 1 + 1;
            double column1Width = (GeoGrid.ActualWidth / totalWeight) * 1.2;
            double column2Width = (GeoGrid.ActualWidth / totalWeight) * 3;
            double column3Width = (GeoGrid.ActualWidth / totalWeight) * 2;
            double column4Width = (GeoGrid.ActualWidth / totalWeight) * 1;
            double column5Width = (GeoGrid.ActualWidth / totalWeight) * 1;
            double column6Width = (GeoGrid.ActualWidth / totalWeight) * 1;
            double column7Width = (GeoGrid.ActualWidth / totalWeight) * 1;

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