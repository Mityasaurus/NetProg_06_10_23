using RickAndMorty_API_CORE.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;

namespace RickAndMorty_UI
{
    /// <summary>
    /// Логика взаимодействия для CharacterInfoPage.xaml
    /// </summary>
    public partial class CharacterInfoPage : Page
    {
        private Character character;
        private MainPage parentPage;

        public CharacterInfoPage(Character character, MainPage parentPage)
        {
            InitializeComponent();

            this.character = character;
            this.parentPage = parentPage;
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            characterImage.Source = new BitmapImage(new Uri(character.ImageURL));
            tbName.Text += string.IsNullOrWhiteSpace(character.Name) ? "Undefined" : character.Name;
            tbStatus.Text += string.IsNullOrWhiteSpace(character.Status) ? "Undefined" : character.Status;
            tbSpecies.Text += string.IsNullOrWhiteSpace(character.Species) ? "Undefined" : character.Species;
            tbGender.Text += string.IsNullOrWhiteSpace(character.Gender) ? "Undefined" : character.Gender;
            tbLocation.Text += string.IsNullOrWhiteSpace(character.Location) ? "Undefined" : character.Location;

            Window mainWindow = Application.Current.MainWindow;
            mainWindow.Width = this.Width + 20;
            mainWindow.Height = this.Height;
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            Window mainWindow = Application.Current.MainWindow;

            NavigationService.Navigate(parentPage);
        }
    }
}
