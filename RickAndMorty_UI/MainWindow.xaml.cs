using RickAndMorty_API_CORE.Data.API;
using RickAndMorty_API_CORE.Domain.ProviderJson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace RickAndMorty_UI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private RAM_API_ENGINE engine { get; set; }
        private int maxPages;
        private int currentPageNumber = 1;
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            engine = new RAM_API_ENGINE();

            string page = engine.GetPage(currentPageNumber);
            JsonProvider.FromJsonToCharacterList(page);
            maxPages = JsonProvider.GetPagesMaxNumberFromJson(page);
            MessageBox.Show(maxPages.ToString());
        }
    }
}
