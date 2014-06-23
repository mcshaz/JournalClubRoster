using DateExtensions;
using MicroMvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Data.Entity;
using System.Windows.Input;
using System.Data;
using System.Diagnostics;
using System.ComponentModel.DataAnnotations.Schema;

namespace JournalClub.ViewModels
{
    class AssignPresentationsViewModel : ObservableObject, IDisposable
    {
        #region Members
        JournalClubContext _db;
        ObservableCollection<PresenterViewModel> _presenters;
        ObservableCollection<TeachingSessionListItem> _teachingSessions;
        PresenterViewModel _selectedPresenter;
        TeachingSessionListItem _selectedTeachingSession;
        PresentationViewModel _selectedPresentation;
        PicuAttachmentViewModel _selectedPicuAttachment;
        public TermDatesViewModel FormDates { get; private set; }
        public TermDatesViewModel PicuAttachDates { get; private set; }

        #endregion
        #region Constructors
        public AssignPresentationsViewModel()
        {
            
            _db = new JournalClubContext();

            FormDates = new TermDatesViewModel();
            FormDates.ApplyTermExecute = FormDates_DatesAccepted;

            PicuAttachDates = new TermDatesViewModel();
            PicuAttachDates.ApplyTermExecute = PicuAttachDates_DatesAccepted;
        }
        #endregion
        #region Methods

        #endregion
        #region Properties
        public ObservableCollection<PresenterViewModel> Presenters
        {
            get
            {
                return _presenters ?? (_presenters = new ObservableCollection<PresenterViewModel>(from p in _db.Presenters
                                                                                                  where p.PicuAttachments.Any(a => (a.FinishDate==null || a.FinishDate >= FormDates.StartDate)
                                                                                                           && a.StartDate <= FormDates.FinishDate)
                                                                                                  orderby p.FullName
                                                                                                  select new PresenterViewModel { Presenter = p }));
            }
        }
        public ObservableCollection<PresentationViewModel> Presentations
        {
            get
            {
                if (_selectedPresenter == null) { return null; }
                return _selectedPresenter.Presentations;
            }
        }
        public ObservableCollection<PicuAttachmentViewModel> PicuAttachments
        {
            get
            {
                if (_selectedPresenter == null) { return null; }
                return _selectedPresenter.PicuAttachments;
            }
        }
        public ObservableCollection<TeachingSessionListItem> TeachingSessions
        {
            get
            {
                return _teachingSessions ?? (_teachingSessions=new ObservableCollection<TeachingSessionListItem>(from t in _db.TeachingSessions
                                                                                                                 where t.SessionDate >= FormDates.StartDate && t.SessionDate <= FormDates.FinishDate
                                                                                                                 orderby t.SessionDate
                                                                                                                 select new TeachingSessionListItem { TeachingSession = t }));
            }
        }
        public PresenterViewModel SelectedPresenter
        {
            get
            {
                return _selectedPresenter;
            }
            set
            {
                if (_selectedPresenter != value)
                {
                    _selectedPresenter = value;
                    RaisePropertyChanged("SelectedPresenter");
                    SelectedPresentation = null;
                    SelectedPicuAttachment = null;
                    RaisePropertyChanged("Presentations");
                    RaisePropertyChanged("PicuAttachments");

                    var d = PicuAttachments.Select(p=>new DateRange{ Start = p.StartDate, Finish = p.FinishDate ?? DateTime.MaxValue});
                    foreach (var t in TeachingSessions)
                    {
                        t.IsDuringAttachment = d.Any(r => r.IsInRange(t.TeachingSession.SessionDate));
                        t.IsSelectedPresenter = SelectedPresenter != null && t.Presenter == SelectedPresenter.Presenter;
                    }
                }
            }
        }
        public TeachingSessionListItem SelectedTeachingSession
        {
            get
            {
                return _selectedTeachingSession;
            }
            set
            {
                if (_selectedTeachingSession != value)
                {
                    _selectedTeachingSession = value;
                    RaisePropertyChanged("SelectedTeachingSession");
                }
            }
        }
        public PresentationViewModel SelectedPresentation
        {
            get
            {
                return _selectedPresentation;
            }
            set
            {
                if (_selectedPresentation != value)
                {
                    _selectedPresentation = value;
                    RaisePropertyChanged("SelectedPresentation");
                }
            }
        }
        public PicuAttachmentViewModel SelectedPicuAttachment
        {
            get
            {
                return _selectedPicuAttachment;
            }
            set
            {
                if (_selectedPicuAttachment != value)
                {
                    _selectedPicuAttachment = value;
                    RaisePropertyChanged("SelectedPicuAttachment");
                    if (_selectedPicuAttachment != null)
                    {
                        PicuAttachDates.SetTermYear(_selectedPicuAttachment.PicuAttachment.StartDate, _selectedPicuAttachment.PicuAttachment.FinishDate);
                    }
                    else
                    {
                        PicuAttachDates.TermYear = FormDates.TermYear;
                    }
                }
            }
        }
        #endregion
        #region Commands
        void SaveChangesExecute()
        {
            /*
#if DEBUG
            foreach (var ent in _db.ChangeTracker.Entries().Where(p => p.State == System.Data.EntityState.Modified))
            {
                // Get the Table() attribute, if one exists
                TableAttribute tableAttr = ent.Entity.GetType().GetCustomAttributes(typeof(TableAttribute), false).SingleOrDefault() as TableAttribute;
                // Get table name (if it has a Table attribute, use that, otherwise get the pluralized name)
                string tableName = tableAttr != null ? tableAttr.Name : ent.Entity.GetType().Name;
                foreach (string propertyName in ent.OriginalValues.PropertyNames)
                {
                    if (!object.Equals(ent.OriginalValues.GetValue<object>(propertyName), ent.CurrentValues.GetValue<object>(propertyName)))
                    {
                        string modifiedVals = "TableName:" + tableName + ',' +
                                    "ColumnName:" + propertyName + ',' +
                                    "OriginalValue:" + (ent.OriginalValues.GetValue<object>(propertyName) == null ? "" : ent.OriginalValues.GetValue<object>(propertyName).ToString()) + ',' +
                                    "NewValue:" + (ent.CurrentValues.GetValue<object>(propertyName) == null ? "" : ent.CurrentValues.GetValue<object>(propertyName).ToString());
                        Debug.WriteLine(modifiedVals);
                    }
                }
            }
#endif
             * */
            _db.SaveChanges();

        }
        bool CanSaveChangesExecute()
        {
            return _db.RequiresSave();
        }
        public ICommand SaveChanges { get { return new RelayCommand(SaveChangesExecute, CanSaveChangesExecute); } }

        void AddPresenterExecute()
        {
            var presenter = new PresenterViewModel {Presenter = new Presenter { Id = Guid.NewGuid() }};
            var presentation = new PresentationViewModel { Presentation = new Presentation { Id=Guid.NewGuid(), Presenter = presenter.Presenter }};
            var attach = new PicuAttachmentViewModel { PicuAttachment = new PicuAttachment { Id = Guid.NewGuid(), Presenter = presenter.Presenter, StartDate = FormDates.StartDate.Value, FinishDate = FormDates.FinishDate } };

            _db.Presenters.Add(presenter.Presenter);
            _db.Presentations.Add(presentation.Presentation);
            _db.PicuAttachments.Add(attach.PicuAttachment);

            Presenters.Add(presenter);
            SelectedPresenter = presenter;
            SelectedPresentation = null;
            SelectedPicuAttachment = null;
        }
        bool CanAddPresenterExecute()
        {
            return true;
        }
        public ICommand AddPresenter { get { return new RelayCommand(AddPresenterExecute, CanAddPresenterExecute); } }

        void RemovePresenterExecute()
        {
            foreach (var s in TeachingSessions.Where(t => t.Presenter == SelectedPresenter.Presenter))
            {
                s.IsSelectedPresenter = false;
                s.Presentation = null;
            }
            
            _db.Presenters.Remove(SelectedPresenter.Presenter);

            Presenters.Remove(SelectedPresenter);

            SelectedPresenter = null;

            //remove from picuAttachments & presentations

            //reset list
            
        }
        bool CanRemovePresenterExecute()
        {
            return SelectedPresenter != null;
        }
        public ICommand RemovePresenter { get { return new RelayCommand(RemovePresenterExecute, CanRemovePresenterExecute); } }

        void AddPresentationExecute()
        {
            var presentation = new PresentationViewModel { Presentation = new Presentation { Id = Guid.NewGuid(), Presenter = SelectedPresenter.Presenter } };

            _db.Presentations.Add(presentation.Presentation);

            Presentations.Add(presentation);

            SelectedPresentation = presentation;
        }
        bool CanAddPresentationExecute()
        {
            return SelectedPresenter !=null;
        }
        public ICommand AddPresentation { get { return new RelayCommand(AddPresentationExecute, CanAddPresentationExecute); } }

        void RemovePresentationExecute()
        {
            _db.Presentations.Remove(SelectedPresentation.Presentation);

            Presentations.Remove(SelectedPresentation);

            SelectedPresentation = null;
        }
        bool CanRemovePresentationExecute()
        {
            return SelectedPresentation != null;
        }
        public ICommand RemovePresentation { get { return new RelayCommand(RemovePresentationExecute, CanRemovePresentationExecute); } }

        void AddAttachmentExecute()
        {
            var attach = new PicuAttachmentViewModel { PicuAttachment = new PicuAttachment {Id= Guid.NewGuid(), Presenter = SelectedPresenter.Presenter } };

            _db.PicuAttachments.Add(attach.PicuAttachment);

            PicuAttachments.Add(attach);

            SelectedPicuAttachment = attach;
        }
        bool CanAddAttachmentExecute()
        {
            return SelectedPresenter != null;
        }
        public ICommand AddAttachment { get { return new RelayCommand(AddAttachmentExecute, CanAddAttachmentExecute); } }

        void RemoveAttachmentExecute()
        {
            _db.PicuAttachments.Remove(SelectedPicuAttachment.PicuAttachment);

            PicuAttachments.Remove(SelectedPicuAttachment);

            SelectedPicuAttachment = null;
        }
        bool CanRemoveAttachmentExecute()
        {
            return SelectedPicuAttachment!=null;
        }
        public ICommand RemoveAttachment { get { return new RelayCommand(RemoveAttachmentExecute, CanRemoveAttachmentExecute); } }

        void AssignPresentationToSessionExecute()
        {
            var presenter = (SelectedTeachingSession.Presentation == null)?null:SelectedTeachingSession.Presentation.Presenter;
            //remove initials etc if another presentation assigned to same presenter
            if (SelectedPresentation.TeachingSession != null) 
            {
                var oldTS = TeachingSessions.FirstOrDefault(t => t.TeachingSession == SelectedPresentation.TeachingSession);
                if (oldTS != null) { oldTS.Presentation = null; }
            }
            SelectedTeachingSession.Presentation = SelectedPresentation.Presentation;
            _db.ChangeTracker.DetectChanges();

            if (presenter != null)
            {
                Presenters.First(p => p.Presenter == presenter).NotifyTeachingSessionChanged();
            }
            presenter = SelectedTeachingSession.Presentation.Presenter;
            Presenters.First(p => p.Presenter == presenter).NotifyTeachingSessionChanged();

        }
        bool CanAssignPresentationToSessionExecute()
        {
            return SelectedPresentation!=null && SelectedTeachingSession != null && SelectedPresentation.TeachingSession != SelectedTeachingSession.TeachingSession;
        }
        public ICommand AssignPresentationToSession { get { return new RelayCommand(AssignPresentationToSessionExecute, CanAssignPresentationToSessionExecute); } }

        void UnAssignPresentationExecute()
        {
            var presenter = SelectedTeachingSession.Presentation.Presenter;
            var associatedPresenter = Presenters.FirstOrDefault(p => p.Presenter == presenter);
            SelectedTeachingSession.Presentation = null;
            SelectedTeachingSession.IsSelectedPresenter = false;
            if (associatedPresenter != null) 
            { associatedPresenter.NotifyTeachingSessionChanged(); }
        }
        bool CanUnAssignPresentationExecute()
        {
            return (SelectedTeachingSession!=null && !SelectedTeachingSession.IsAvailable);
        }
        public ICommand UnAssignPresentation { get { return new RelayCommand(UnAssignPresentationExecute, CanUnAssignPresentationExecute); } }

        void CopyDatesExecute()
        {
            string dates = string.Join(Environment.NewLine,
                                           from t in TeachingSessions
                                           where t.Presenter != null
                                           select t.SessionDateString + '\t' + t.Presenter.FullName);
            System.Windows.Clipboard.SetData(System.Windows.DataFormats.Text, dates);
        }
        bool CanCopyDatesExecute()
        {
            return (TeachingSessions.Any(t=>t.Presentation!=null));
        }
        public ICommand CopyDates { get { return new RelayCommand(CopyDatesExecute, CanCopyDatesExecute); } }

        private void SetAllSessionsExecute()
        {
            var wed = FormDates.StartDate.Value.ClosestWeekDay(DayOfWeek.Wednesday, true);
            while (wed <= FormDates.FinishDate)
            {
                if (_db.TeachingSessions.All(t => t.SessionDate != wed)) { AddSessionExecute(wed); }
                wed = wed.AddDays(7);
            }
        }
        bool CanSetAllSessionsExecute()
        {
            return FormDates.StartDate.HasValue && FormDates.FinishDate.HasValue;
        }
        public ICommand SetAllSessions { get { return new RelayCommand(SetAllSessionsExecute, CanSetAllSessionsExecute); } }

        private void RemoveSessionExecute()
        {
            _db.TeachingSessions.Remove(SelectedTeachingSession.TeachingSession);

            TeachingSessions.Remove(SelectedTeachingSession);

            SelectedTeachingSession = null;
        }
        bool CanRemoveSessionExecute()
        {
            return SelectedTeachingSession !=null;
        }
        public ICommand RemoveSession { get { return new RelayCommand(RemoveSessionExecute, CanRemoveSessionExecute); } }

        private void AddSessionExecute()
        {
            SelectedTeachingSession = AddSessionExecute((SelectedTeachingSession == null) ? DateTime.Now.Date.ClosestWeekDay(DayOfWeek.Wednesday, true, null) : SelectedTeachingSession.SessionDate);
        }
        private TeachingSessionListItem AddSessionExecute(DateTime sessionDate)
        {
            var ts = new TeachingSessionListItem 
            { 
                TeachingSession = new TeachingSession 
                { 
                    Id = Guid.NewGuid(), 
                    SessionDate = sessionDate,
                     Presentations = new List<Presentation>()
                } 
            };

            _db.TeachingSessions.Add(ts.TeachingSession);

            TeachingSessions.Add(ts);

            return ts;
        }
        bool CanAddSessionExecute()
        {
            return true;
        }
        public ICommand AddSession { get { return new RelayCommand(AddSessionExecute, CanAddSessionExecute); } }


        void EmailDatesExecute()
        {
            EmailManager.sendDates(Presenters.Select(p=>p.Presenter));
            _db.SaveChanges();
        }
        bool CanEmailDatesExecute()
        {
            return !_db.RequiresSave();
        }
        public ICommand EmailDates { get { return new RelayCommand(EmailDatesExecute, CanEmailDatesExecute); } }

        #endregion
        #region EventManagers
        private void PicuAttachDates_DatesAccepted()
        {
            if (_selectedPicuAttachment == null) { return; }
            
            if (PicuAttachDates.StartDate.HasValue) { 
                _selectedPicuAttachment.StartDate = PicuAttachDates.StartDate.Value; 
            }
            _selectedPicuAttachment.FinishDate = PicuAttachDates.FinishDate;
        }

        private void FormDates_DatesAccepted()
        {
            if (!FormDates.StartDate.HasValue) { FormDates.StartDate = DateTime.MinValue; }
            _presenters = null;
            RaisePropertyChanged("Presenters");
        }

        public void OnWindowClosing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            e.Cancel = !CloseWindowDialog.CloseWithoutSaving(_db);
        }
        #endregion
        #region Interface Implemntations
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
