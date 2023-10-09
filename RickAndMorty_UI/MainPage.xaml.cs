using RickAndMorty_API_CORE.Data.API;
using RickAndMorty_API_CORE.Domain.Models;
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
    /// Логика взаимодействия для MainPage.xaml
    /// </summary>
    public partial class MainPage : Page
    {
        private RAM_API_ENGINE engine { get; set; }
        private int maxPages;
        private int currentPageNumber = 1;
        private bool IsFirstLoad = true;
        List<Character> characters;
        public MainPage()
        {
            InitializeComponent();
        }

        private void UpdateDataSource()
        {
            string page = engine.GetPage(currentPageNumber);

            listViewMain.ItemsSource = null;
            characters = JsonProvider.FromJsonToCharacterList(page);
            listViewMain.ItemsSource = characters;
        }

        private void UpdateDataSource(List<Character> newCharacters)
        {
            listViewMain.ItemsSource = null;
            listViewMain.ItemsSource = newCharacters;
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            if (IsFirstLoad)
            {
                engine = new RAM_API_ENGINE();

                string page = engine.GetPage(currentPageNumber);
                characters = JsonProvider.FromJsonToCharacterList(page);
                listViewMain.ItemsSource = characters;
                maxPages = JsonProvider.GetPagesMaxNumberFromJson(page);

                IsFirstLoad = false;
            }

            Window mainWindow = Application.Current.MainWindow;
            mainWindow.Width = this.Width + 50;
            mainWindow.Height = this.Height + 50;
        }

        private void btnPrev_Click(object sender, RoutedEventArgs e)
        {
            if (currentPageNumber > 1)
            {
                currentPageNumber--;
                UpdateDataSource();
                btnNext.Visibility = Visibility.Visible;
                if (currentPageNumber == 1)
                {
                    btnPrev.Visibility = Visibility.Hidden;
                }

                UpdateBySearchBars();
            }
        }

        private void btnNext_Click(object sender, RoutedEventArgs e)
        {
            if (currentPageNumber < maxPages)
            {
                currentPageNumber++;
                UpdateDataSource();
                btnPrev.Visibility = Visibility.Visible;
                if (currentPageNumber == maxPages)
                {
                    btnNext.Visibility = Visibility.Hidden;
                }

                UpdateBySearchBars();
            }
        }

        private void listViewMain_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Character? selectedCharacter = listViewMain.SelectedItem as Character;

            if (selectedCharacter != null)
            {
                CharacterInfoPage characterPage = new CharacterInfoPage(selectedCharacter, this);

                NavigationService.Navigate(characterPage);
            }
        }

        private void tb_Name_TextChanged(object sender, TextChangedEventArgs e)
        {
            UpdateBySearchBars();
        }

        private void UpdateBySearchBars()
        {
            string name = tb_Name.Text.ToLower();
            string location = tb_Location.Text.ToLower();

            if(string.IsNullOrWhiteSpace(name) && string.IsNullOrWhiteSpace(location))
            {
                UpdateDataSource(characters);
                return;
            }

            if(string.IsNullOrWhiteSpace(location))
            {
                var newCharacters = characters.Where(c => c.Name.ToLower().Contains(name)).ToList();

                UpdateDataSource(newCharacters);
            }
            else if(string.IsNullOrWhiteSpace(name))
            {
                var newCharacters = characters.Where(c => c.Location.ToLower().Contains(location)).ToList();

                UpdateDataSource(newCharacters);
            }
            else
            {
                var newCharacters = characters.Where(c => c.Name.ToLower().Contains(name) 
                                                  && c.Location.ToLower().Contains(location)).ToList();

                UpdateDataSource(newCharacters);
            }
        }
    }
}
