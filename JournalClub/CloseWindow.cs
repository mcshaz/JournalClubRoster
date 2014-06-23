using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Windows;

namespace JournalClub
{
    public static class CloseWindowDialog
    {
        public static bool CloseWithoutSaving(DbContext context)
        {
            if (!context.ChangeTracker.Entries()
                      .Any(e => e.State == EntityState.Added
                             || e.State == EntityState.Deleted
                             || e.State == EntityState.Modified)) { return true; }
            return (MessageBox.Show("The data has changed since last save - do you wish to close WITHOUT saving changes?", "Close Without Saving?", MessageBoxButton.OKCancel, MessageBoxImage.Warning) == MessageBoxResult.OK);
        }
    }
}
