using MicroMvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace JournalClub.ViewModels
{
    class PresenterListItem : ObservableObject
    {
        private bool _available;
        public string PresenterName
        {
            get 
            {
                if (Presentation == null) { return "Please Select"; }
                return (Presentation.Presenter == null) ? "Not Assigned" : Presentation.Presenter.FullName; 
            }
        }
        public Presentation Presentation { get; set; }
        public bool Available
        {
            get { return _available; }
            set
            {
                if (_available != value)
                {
                    _available = value;
                    RaisePropertyChanged("Available");
                }
            }
        }
    }
}
