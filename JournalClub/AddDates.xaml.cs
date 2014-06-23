using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Data.Entity;
using DateExtensions;

namespace JournalClub
{
    /// <summary>
    /// Interaction logic for AddDates.xaml
    /// </summary>
    public partial class AddDates : Window, IDisposable
    {
        private JournalClubContext _db = new JournalClubContext();
        
        private DateTime _startDate;
        private DateTime _endDate;
        public AddDates(DateTime startDate, DateTime endDate)
        {
            _startDate = startDate;
            _endDate = endDate;
            InitializeComponent();
            DatesHeader.Content = String.Format((string)DatesHeader.Content, startDate, endDate);
        }

        private void SetMissingDates_Click(object sender, RoutedEventArgs e)
        {
            var wed = _startDate.ClosestWeekDay(DayOfWeek.Wednesday, true);
            while (wed <= _endDate)
            {
                if (_db.TeachingSessions.All(t=>t.SessionDate!=wed)) {_db.TeachingSessions.Add(new TeachingSession { SessionDate = wed });}
                wed = wed.AddDays(7);
            }
        }

        private void RemoveDate_Click(object sender, RoutedEventArgs e)
        {
            if (SelectedDates.SelectedIndex == -1) { return; }
            _db.TeachingSessions.Remove((TeachingSession)SelectedDates.SelectedItem);
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            var teachingSessionViewSource = ((System.Windows.Data.CollectionViewSource)(this.FindResource("teachingSessionViewSource")));
            _db.TeachingSessions.Load();
            teachingSessionViewSource.Source = _db.TeachingSessions.Local;
        }


        private void SaveClick(object sender, RoutedEventArgs e)
        {
            _db.SaveChanges();
        }

        private void AddDateClick(object sender, RoutedEventArgs e)
        {
            var newSession = new TeachingSession{ SessionDate=DateTime.Now.Date};
            _db.TeachingSessions.Add(newSession);
            SelectedDates.SelectedItem = newSession;
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            e.Cancel = !CloseWindowDialog.CloseWithoutSaving(_db);
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
