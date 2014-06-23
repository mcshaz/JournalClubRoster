using System.Windows;
using JournalClub.ViewModels;

namespace JournalClub
{
    /// <summary>
    /// Interaction logic for AssignPresentations.xaml
    /// </summary>
    public partial class AssignPresentations : Window
    {
        public AssignPresentations()
        {
            InitializeComponent();
            var viewModel = new AssignPresentationsViewModel();
            this.DataContext = viewModel; 
            Closing += viewModel.OnWindowClosing;
        }

    }
}
