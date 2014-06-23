using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using DateExtensions;

namespace JournalClub
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }


        private void ArticlesClick(object sender, RoutedEventArgs e)
        {
            var newWindow = new Articles();
            newWindow.ShowDialog();
        }

        private void JournalRegExClick(object sender, RoutedEventArgs e)
        {
            var newWindow = new RegexTester();
            newWindow.ShowDialog();
        }

        private void AssignPresentationsClick(object sender, RoutedEventArgs e)
        {
            var newWindow = new AssignPresentations();
            newWindow.ShowDialog();
        }

        private void CreateSeedClick(object sender, RoutedEventArgs e)
        {
            PropertyMappingUtils.MapExistingInstaces.CreateSeedFile(new JournalClubContext(), PropertyMappingUtils.MapExistingInstaces.SeedClassType.DBinitializer);
        }

        private void BatchPresenterRegExClick(object sender, RoutedEventArgs e)
        {
            var newWindow = new BatchPresenter();
            newWindow.ShowDialog();
        }
    }
}
