using MicroMvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.Text;

namespace JournalClub.ViewModels
{
    class PresenterViewModel : ObservableObject
    {
        #region Members
        private bool _isAvailable;
        private ObservableCollection<PresentationViewModel> _presentations;
        private ObservableCollection<PicuAttachmentViewModel> _attachments;
#endregion
        #region constructors
#endregion
        #region Methods
        private void Presentations_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            RaisePropertyChanged("HasPresentations");
        }
        public void NotifyTeachingSessionChanged()
        {
            foreach (var p in Presentations.Where(p => p.Presentation.TeachingSession == null))
            {
                p.EmailSent = null;
            }
            RaisePropertyChanged("HasUnassignedPresentations");
        }
        #endregion
        #region Properties
        public Presenter Presenter
        {
            get;
            set;
        }
        public ObservableCollection<PresentationViewModel> Presentations
        {
            get
            {
                if (_presentations == null)
                {
                    _presentations = new ObservableCollection<PresentationViewModel>(Presenter.Presentations.Select(p => new PresentationViewModel { Presentation = p }));
                    _presentations.CollectionChanged +=_presentations_CollectionChanged;
                }
                return _presentations;
            }
        }

        private void _presentations_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            RaisePropertyChanged("HasPresentations");
        }
        public ObservableCollection<PicuAttachmentViewModel> PicuAttachments
        {
            get
            {
                return _attachments ?? (_attachments = new ObservableCollection<PicuAttachmentViewModel>(Presenter.PicuAttachments.Select(a => new PicuAttachmentViewModel { PicuAttachment = a })));
            }
        }
        public string PresenterName
        {
            get 
            {
                return Presenter.FullName; 
            }
            set
            {
                if (value != Presenter.FullName)
                {
                    Presenter.FullName = value;
                    RaisePropertyChanged("PresenterName");
                }
            }
        }
        public string PresenterPersonalEmail
        {
            get
            {
                return Presenter.PersonalEmail;
            }
            set
            {
                if (value != Presenter.PersonalEmail)
                {
                    Presenter.PersonalEmail = value;
                    RaisePropertyChanged("PresenterPersonalEmail");
                }
            }
        }
        public string PresenterWorkEmail
        {
            get
            {
                return Presenter.WorkEmail;
            }
            set
            {
                if (value != Presenter.WorkEmail)
                {
                    Presenter.WorkEmail = value;
                    RaisePropertyChanged("PresenterWorkEmail");
                }
            } 
        }
        public bool IsAvailable
        {
            get { return _isAvailable; }
            set
            {
                if (_isAvailable != value)
                {
                    _isAvailable = value;
                    RaisePropertyChanged("IsAvailable");
                }
            }
        }

        public bool HasPresentations { get { return Presentations.Any(); } }
        public bool HasUnassignedPresentations { get { return Presentations.Any(p => p.Presentation.TeachingSession == null); } }
#endregion

    }
}
