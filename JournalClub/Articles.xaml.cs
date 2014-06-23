using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Data.Entity;
using PropertyMappingUtils;
using JournalClub.ViewModels;
using DateExtensions;
using System.Text.RegularExpressions;

namespace JournalClub
{
    /// <summary>
    /// Interaction logic for Articles.xaml
    /// </summary>
    public partial class Articles : Window, IDisposable
    {

        private JournalClubContext _db = new JournalClubContext();
        private int _defaultArticleTypeId;

        public Articles()
        {
            InitializeComponent();
        }

        private RegexPropertyMap<Article> _articleMap;
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            var articlesViewSource = ((System.Windows.Data.CollectionViewSource)(this.FindResource("articlesViewSource")));

            var termYear = TermCalculations.GetAttachment(DateTime.Now.AddMonths(1));

            var currentPresenters = GetPresenterList(termYear.Dates.Start, termYear.Dates.Finish, _db);
            var presenterIds = currentPresenters.Where(c=>c.Presentation!=null).Select(c=>c.Presentation.PresenterId);

            (from a in _db.Articles
             where a.Presentation==null || presenterIds.Contains(a.Presentation.PresenterId)
             select a).Load();

            articlesViewSource.Source = _db.Articles.Local;

            var articleTypesViewSource = ((System.Windows.Data.CollectionViewSource)(this.FindResource("articleTypesViewSource")));
            var _articleTypes = _db.ArticleTypes.ToArray();
            articleTypesViewSource.Source = _articleTypes;
            _defaultArticleTypeId = _articleTypes.First().Id;

            var currentPresenterViewSource = ((System.Windows.Data.CollectionViewSource)(this.FindResource("currentPresentersViewSource")));
            //_db.Presenters.Where(p=>p.PicuAttachments.Any(a=>a.FinishDate>_startDate && a.StartDate<_endDate)).Load();
            currentPresenterViewSource.Source = currentPresenters;

            RefRegEx.ItemsSource = _db.JournalReferenceRegExps.ToArray();
        }

        private void AddFreeText(object sender, RoutedEventArgs e)
        {
            if (RefRegEx.SelectedIndex == -1) { return; }
            _articleMap.ParseString = FreeText.Text;
            Article art = new Article();
            _articleMap.AssignTo(ref art);
            art.Title = trimAndSingleLine(art.Title);
            art.Journal = trimAndSingleLine(art.Journal);
            art.Authors = trimAndSingleLine(art.Authors);
            if (art.ArticleTypeId == 0) { art.ArticleTypeId = _defaultArticleTypeId; }
            _db.Articles.Add(art);
            articlesDataGrid.SelectedItem = art;

        }
        private static string trimAndSingleLine(string text)
        {
            return text.Trim().Replace('\n', ' ').Replace("\r", "");
        }
        private void RefRegEx_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var selectedRegex = (JournalReferenceRegExp)RefRegEx.SelectedItem;
            _articleMap = new RegexPropertyMap<Article>(new Regex(selectedRegex.Regex, selectedRegex.SingleLine ? RegexOptions.Singleline : RegexOptions.Multiline)); 
        }

        private void SaveChangesClick(object sender, RoutedEventArgs e)
        {
            _db.SaveChanges();
        }
        private void AddArticleClick(object sender, RoutedEventArgs e)
        {
            Article art = new Article();
            _db.Articles.Add(art);
            articlesDataGrid.SelectedItem = art;
        }
        private void ArticleLocationClick(object sender, RoutedEventArgs e)
        {
            var dlg = new Microsoft.Win32.OpenFileDialog
            {
                Title = "Select article",
                DefaultExt = ".pdf",
                Filter = "Acrobat File (.pdf)|*.pdf",
                CheckFileExists = true,
                Multiselect=false
            };
            if (dlg.ShowDialog() == true)
            {
                TextBox tb = FindSibbling<TextBox>((DependencyObject)sender, "articleLocation");
                tb.Text = dlg.FileName;
            }
        }        
        private void PowerPointClick(object sender, RoutedEventArgs e)
        {
            var dlg = new Microsoft.Win32.OpenFileDialog
            {
                Title = "Select presentation",
                DefaultExt = ".ppt",
                Filter = "Powerpoint file (.ppt)|*.ppt;*.pptx",
                CheckFileExists = true,
                Multiselect=false
            };
            if (dlg.ShowDialog() == true)
            {
                TextBox tb = FindSibbling<TextBox>((DependencyObject)sender, "powerPointLocation");
                tb.Text = dlg.FileName;
            }
        }

        private static IEnumerable<PresenterListItem> GetPresenterList(DateTime startDate, DateTime finishDate, JournalClubContext db)
        {
            var allPresenters = new List<PresenterListItem>();
            allPresenters.Add(new PresenterListItem { Available = true });
            allPresenters.AddRange((from p in db.Presentations
                                    where p.TeachingSession == null || (p.TeachingSession.SessionDate >= startDate && p.TeachingSession.SessionDate <= finishDate)
                                   select p).Select(p => new PresenterListItem { Presentation = p, Available = !p.Articles.Any() }).ToList());
            return allPresenters;
        }

        private void Presenter_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //List = (PresenterListItem)(((ComboBox)sender).);
            if (e.RemovedItems.Count > 0) //skip first assignment
            {
                var newSelection = ((PresenterListItem)e.AddedItems[0]);
                if (newSelection.Presentation != null) { newSelection.Available = false; }
                ((PresenterListItem)e.RemovedItems[0]).Available = true;
            }
        }
        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            e.Cancel = !CloseWindowDialog.CloseWithoutSaving(_db);
        }
        //http://stackoverflow.com/questions/636383/wpf-ways-to-find-controls

        private static T FindSibbling<T>(DependencyObject knownSibbling, string sibblingName)where T : DependencyObject
        {
            var parent = VisualTreeHelper.GetParent(knownSibbling);
            if (parent == null) return null;

            int childrenCount = VisualTreeHelper.GetChildrenCount(parent);
            for (int i = 0; i < childrenCount; i++)
            {
                var child = VisualTreeHelper.GetChild(parent, i);
                T childType = child as T;
                if (childType != null)
                {
                    if (string.IsNullOrEmpty(sibblingName))
                    {
                        return childType;
                    }
                    else
                    {
                        var frameworkElement = child as FrameworkElement;
                        if (frameworkElement != null && frameworkElement.Name == sibblingName)
                        {
                            return childType;
                        }
                    }
                }
            }
            return null;
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
