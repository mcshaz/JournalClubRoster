using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Data.Entity;
using PropertyMappingUtils;
using System;

namespace JournalClub
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class RegexTester : Window, IDisposable
    {
        private RegexMapExperimenter<Article> _articleProperties;
        private JournalClubContext _db = new JournalClubContext();
        CollectionViewSource _journalReferenceRegExpsViewSource;
        public RegexTester()
        {
            InitializeComponent();
            _articleProperties = new RegexMapExperimenter<Article>();
        }


        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            // Load data into JournalReferenceRegExps. You can modify this code as needed.
            _journalReferenceRegExpsViewSource = ((System.Windows.Data.CollectionViewSource)(this.FindResource("journalReferenceRegExpsViewSource")));

            // Load is an extension method on IQueryable, 
            // defined in the System.Data.Entity namespace.
            // This method enumerates the results of the query, 
            // similar to ToList but without creating a list.
            // When used with Linq to Entities this method 
            // creates entity objects and adds them to the context.
            _db.JournalReferenceRegExps.Load();

            // After the data is loaded call the DbSet<T>.Local property 
            // to use the DbSet<T> as a binding source.
            _journalReferenceRegExpsViewSource.Source = _db.JournalReferenceRegExps.Local;

            _articleProperties.ParseString = TestOn.Text;
            _articleProperties.RegularExpression = UserRegex.Text;
            Result.Text = _articleProperties.ToString();
        }

        private void backButton_Click(object sender, RoutedEventArgs e)
        {
            if (_journalReferenceRegExpsViewSource.View.CurrentPosition > 0)
            { 
                _journalReferenceRegExpsViewSource.View.MoveCurrentToPrevious();
                UserRegex_LostFocus(sender, e);
            }
        }

        private void nextButton_Click(object sender, RoutedEventArgs e)
        {
            if (_journalReferenceRegExpsViewSource.View.CurrentPosition < ((CollectionView)_journalReferenceRegExpsViewSource.View).Count - 1)
            { 
                _journalReferenceRegExpsViewSource.View.MoveCurrentToNext();
                UserRegex_LostFocus(sender, e);
            
            }
        }

        private void AcceptRegExClick(object sender, RoutedEventArgs e)
        {
            _db.SaveChanges();
        }

        private void TestOn_LostFocus(object sender, RoutedEventArgs e)
        {
            _articleProperties.ParseString = TestOn.Text;
            Result.Text = _articleProperties.ToString();
        }

        private void UserRegex_LostFocus(object sender, RoutedEventArgs e)
        {
            _articleProperties.RegularExpression = UserRegex.Text;
            Result.Text = _articleProperties.ToString();
        }

        private void AddRegEx(object sender, RoutedEventArgs e)
        {
            var newrx = new JournalReferenceRegExp();
            _db.JournalReferenceRegExps.Add(newrx);
            _journalReferenceRegExpsViewSource.View.MoveCurrentToLast();
            Result.Text = "Enter Value Above";
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            e.Cancel = !CloseWindowDialog.CloseWithoutSaving(_db);
        }


        private void SingleLine_Checked(object sender, RoutedEventArgs e)
        {
            bool isSingle = ((CheckBox)sender).IsChecked.Value;
            _articleProperties.RegexOptions = isSingle ?RegexOptions.Singleline:RegexOptions.Multiline;
        }


        private bool disposedValue = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposedValue)
            {
                if (disposing)
                {
                    _db.Dispose();
                }
            }
            this.disposedValue = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

    }
}
