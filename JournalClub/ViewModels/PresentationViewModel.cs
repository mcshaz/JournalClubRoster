using MicroMvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JournalClub.ViewModels
{
    class PresentationViewModel : ObservableObject
    {
        #region Members

        #endregion
        public Presentation Presentation {get; set;}
        public TeachingSession TeachingSession
        {
            get { return Presentation.TeachingSession; }
            set 
            {
                if (value != Presentation.TeachingSession)
                {
                    Presentation.TeachingSession = value;
                    RaisePropertyChanged("PresentationDate");
                }
            }
        }
        public String PresentationDate
        {
            get 
            { 
                var ts = Presentation.TeachingSession;
                if (ts==null) {return "Not Assigned";}
                return ts.SessionDate.ToString("d");
            }
        }
        public String ArticleTitle
        {
            get 
            { 
                var art = Presentation.Articles.FirstOrDefault();
                if (art == null) { return "Not Assigned"; }
                return art.Title;
            }
        }
        public DateTime? EmailSent
        {
            get
            {
                if (Presentation==null) {return null;}
                return Presentation.EmailSent;
            }
            set
            {
                Presentation.EmailSent = value;
            }
        }
    }
}
