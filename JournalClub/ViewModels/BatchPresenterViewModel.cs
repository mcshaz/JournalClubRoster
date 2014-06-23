using DateExtensions;
using MicroMvvm;
using PropertyMappingUtils;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Input;

namespace JournalClub.ViewModels
{
    class BatchPresenterViewModel : ObservableObject, IDisposable
    {
        #region members
        JournalClubContext _db;
        RegexMapExperimenter<Presenter> _mapExperimenter;
        #endregion
        #region constructors
        public BatchPresenterViewModel()
        {
            _db = new JournalClubContext();
            Presenters = new ObservableCollection<PresenterViewModel>();
            _mapExperimenter = new RegexMapExperimenter<Presenter> 
            {
                GlobalMatch = true, 
                RegexOptions = RegexOptions.Multiline,
                RegularExpression=@"^(?<FullName>[^,]*),(?<WorkEmail>[^,]*),(?<PersonalEmail>.*)$",
                TrimProperties =true,
                AutoTruncateToAttrLen = true
            };
            PicuAttachDates = new TermDatesViewModel();
        }
        #endregion
        #region properties
        public ObservableCollection<PresenterViewModel> Presenters { get; private set; }
        public TermDatesViewModel PicuAttachDates { get; private set; }
        public string UserRegEx 
        {
            get
            {
                return _mapExperimenter.RegularExpression;
            }
            set 
            {
                if (_mapExperimenter.RegularExpression != value)
                {
                    _mapExperimenter.RegularExpression = value;
                    RaisePropertyChanged("TestResult");
                }
            } 
        }
        public string UserMailList 
        {
            get 
            {
                return _mapExperimenter.ParseString;
            }
            set 
            {
                if (_mapExperimenter.ParseString != value)
                {
                    _mapExperimenter.ParseString = value;
                    RaisePropertyChanged("TestResult");
                }
            } 
        }
        public string TestResult { get { return _mapExperimenter.ToString(); } }
        public bool MultiLine
        {
            get { return _mapExperimenter.RegexOptions.HasFlag(RegexOptions.Multiline); }
            set
            {
                if (value != MultiLine)
                {
                    _mapExperimenter.RegexOptions = value?RegexOptions.Multiline:RegexOptions.Singleline;
                    RaisePropertyChanged("TestResult");
                }
            }
        }
        #endregion
        #region ICommands
        void SaveChangesExecute()
        {
            var selectedDates = new DateRange { Start = PicuAttachDates.StartDate.Value, Finish = PicuAttachDates.FinishDate.Value };
            foreach (var pVM in Presenters)
            {
                var p = pVM.Presenter;
                var dbPresenter = (from d in _db.Presenters
                                   where d.PersonalEmail == p.PersonalEmail || d.WorkEmail == p.WorkEmail
                                   select d).FirstOrDefault();
                if (dbPresenter == null)
                {
                    p.Id = Guid.NewGuid();
                    _db.Presenters.Add(p);
                    _db.PicuAttachments.Add(new PicuAttachment { Id = Guid.NewGuid(), Presenter = p, StartDate = selectedDates.Start, FinishDate = selectedDates.Finish });
                }
                else
                {
                    if (!string.IsNullOrWhiteSpace(p.FullName)) { dbPresenter.FullName = p.FullName; }
                    if (!string.IsNullOrWhiteSpace(p.PersonalEmail)) { dbPresenter.PersonalEmail = p.PersonalEmail; }
                    if (!string.IsNullOrWhiteSpace(p.WorkEmail)) { dbPresenter.WorkEmail = p.WorkEmail; }
                    var attach = p.AdjustedOverlappingDates(selectedDates);
                    if (attach == null)
                    {
                        attach = new PicuAttachment { Id = Guid.NewGuid(), Presenter = p, StartDate = selectedDates.Start, FinishDate = selectedDates.Finish };
                        _db.PicuAttachments.Add(attach);
                    }
                }
            }
            _db.SaveChanges();
            Presenters.Clear();
        }
        bool CanSaveChangesExecute()
        {
            return Presenters.Any() && PicuAttachDates.StartDate.HasValue && PicuAttachDates.FinishDate.HasValue;
        }
        public ICommand SaveChanges { get { return new RelayCommand(SaveChangesExecute, CanSaveChangesExecute); } }

        void ApplyRegexExecute()
        {
            var map = _mapExperimenter.PropertyMap;
            if (map==null) {return;}
            Presenters.Clear();
            foreach (var p in map.ToList().Select(p => new PresenterViewModel { Presenter = p }))
            {
                Presenters.Add(p);
            }
        }
        bool CanApplyRegexExecute()
        {
            return true;
        }
        public ICommand ApplyRegex { get { return new RelayCommand(ApplyRegexExecute, CanApplyRegexExecute); } }

        void ClearPresentersExecute()
        {
            Presenters.Clear();
        }
        bool CanClearPresentersExecute()
        {
            return true;
        }
        public ICommand ClearPresenters { get { return new RelayCommand(ClearPresentersExecute, CanClearPresentersExecute); } }
        #endregion
        #region InterfaceImplementation

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
        #endregion
    }
}
