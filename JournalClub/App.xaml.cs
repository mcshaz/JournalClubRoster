using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace JournalClub
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        void App_Startup(object sender, StartupEventArgs e)
        {
            //Database.SetInitializer(new JournalClubContextSeedInitializer());
            var db = new JournalClubContext();

            try
            {
                db.Articles.Any();
            }
            catch (ArgumentException)
            {
                MessageBox.Show("Unable to access data source. Please check you have Microsoft SQL Server Compact 4.0 installed (http://www.microsoft.com/en-us/download/details.aspx?id=17876)");
                Application.Current.Shutdown();
            }
        }

    }
}
