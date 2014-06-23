using MicroMvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Windows.Input;

namespace JournalClub.ViewModels
{
    class TeachingSessionListItem : ObservableObject
    {
        private bool _isDuringAttachment;
        private bool _isSelectedPresenter;
        public DateTime SessionDate
        {
            get { return _teachingSession.SessionDate; }
            set
            {
                if (_teachingSession.SessionDate != value)
                {
                    _teachingSession.SessionDate = value;
                    RaisePropertyChanged("SessionDate");
                }
            }
        }
        public string SessionDateString
        {
            get 
            {
                if (TeachingSession == null) { return "None Selected"; }
                return TeachingSession.SessionDate.ToString("d");
            }
        }
        public string PresenterInitials
        {
            get
            {
                var presentation = this.Presentation;
                if (presentation == null || presentation.Presenter==null) { return string.Empty; }
                return presentation.Presenter.FullName.ToInitials(); 
            }
        }
        private TeachingSession _teachingSession;
        public TeachingSession TeachingSession
        {
            get
            {
                return _teachingSession;
            }
            set
            {
                _teachingSession = value;
            }
        }
        public Presenter Presenter
        {
            get
            {
                var presentation = this.Presentation;
                if (presentation==null || presentation.Presenter==null ){ return null; }
                return presentation.Presenter;
            }
        }
        public Presentation Presentation
        {
            get
            {
                if (_teachingSession == null || _teachingSession.Presentations == null) { return null; }
                return _teachingSession.Presentations.FirstOrDefault();
            }
            set
            {
                var pres = this.Presentation;
                if (value != pres)
                {
                    if (pres != null) 
                    {
                        pres.TeachingSession = null;
                        _teachingSession.Presentations.Clear(); 
                    }
                    if (value != null) 
                    {
                        value.TeachingSession = _teachingSession;
                        _teachingSession.Presentations.Add(value); 
                    }
                    RaisePropertyChanged("IsAvailable");
                    RaisePropertyChanged("PresenterInitials");
                }
            }
        }

        public bool IsAvailable
        {
            get { return (_teachingSession==null || !_teachingSession.Presentations.Any()); }
        }
        public bool IsDuringAttachment
        {
            get { return _isDuringAttachment; }
            set
            {
                if (_isDuringAttachment != value)
                {
                    _isDuringAttachment = value;
                    RaisePropertyChanged("IsDuringAttachment");
                }
            }
        }
        public bool IsSelectedPresenter
        {
            get { return _isSelectedPresenter; }
            set
            {
                if (_isSelectedPresenter != value)
                {
                    _isSelectedPresenter = value;
                    RaisePropertyChanged("IsSelectedPresenter");
                }
            }
        }
    }
    public static class stringExtensions
    {
        public static string ToInitials(this string inpt)
        {
            return string.Join("", inpt.Split(' ').Select(s => char.ToUpper(s[0])).Where(s => char.IsLetter(s)));
        }
    }
}

