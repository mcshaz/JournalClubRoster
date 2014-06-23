using JournalClub.ViewModels;
using System.Windows;

namespace JournalClub
{
    /// <summary>
    /// Interaction logic for BatchPresenter.xaml
    /// </summary>
    public partial class BatchPresenter : Window
    {
        public BatchPresenter()
        {
            InitializeComponent();
            this.DataContext = new BatchPresenterViewModel();
        }
    }
}
