using DateExtensions;
using MicroMvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Input;

namespace JournalClub.ViewModels
{
    class TermDatesViewModel : ObservableObject
    {
        #region members
        StaffAttachment _termYear;
        DateTime? _startDate;
        DateTime? _finishDate;
        public bool DatesChanged { get; set; }
        const int monthsAdvance=2;
        #endregion
        #region constructors
        public TermDatesViewModel()
            : this(TermCalculations.GetAttachment(DateTime.Now.AddMonths(monthsAdvance)))
        {
        }
        public TermDatesViewModel(DateTime startDate, DateTime? finishDate) : this(GetTermYear(startDate, finishDate))
        { 
        }
        public TermDatesViewModel(StaffAttachment termYear)
        {
            _termYear = termYear;
            _startDate = _termYear.Dates.Start;
            _finishDate = _termYear.Dates.Finish;
        }
        #endregion
        #region Methods
        public void SetTermYear(DateTime? startDate, DateTime? finishDate)
        {
            TermYear = GetTermYear(startDate, finishDate);
            SetNewDates();
        }
        private static StaffAttachment GetTermYear(DateTime? startDate, DateTime? finishDate)
        {
            if (startDate.HasValue && finishDate.HasValue)
            {
                StaffAttachment returnVar = TermCalculations.GetAttachment(startDate.Value, (int)(365 / (double)(finishDate.Value - startDate.Value).Days));
                if (returnVar.TermNumber >1 && (returnVar.Dates.Finish - startDate.Value) < (startDate.Value - returnVar.Dates.Start))
                {
                    returnVar.TermNumber -= 1;
                }
                return returnVar;
            }
            else
            {
                return TermCalculations.GetAttachment(DateTime.Now.AddMonths(monthsAdvance));
            }
        }
        #endregion
        #region properties
        public StaffAttachment TermYear
        {
            get
            {
                return _termYear;
            }
            set
            {
                if (_termYear != value)
                {
                    _termYear = value;
                    RaisePropertyChanged("TermYear");
                }
            }
        }
        public DateTime? StartDate
        {
            get
            {
                return _startDate;
            }
            set
            {
                if (_startDate != value)
                {
                    _startDate = value;
                    DatesChanged = true;
                    RaisePropertyChanged("StartDate");
                }
            }
        }
        public DateTime? FinishDate
        {
            get
            {
                return _finishDate;
            }
            set
            {
                if (_finishDate != value)
                {
                    _finishDate = value;
                    DatesChanged = true;
                    RaisePropertyChanged("FinishDate");
                }
            }
        }
        public int StartingYear
        {
            get
            {
                return _termYear.YearCommencing;
            }
            set
            {
                if (_termYear.YearCommencing != value)
                {
                    TermYear = TermCalculations.GetAttachment(value, TermNumber, TermsPerYear);
                    SetNewDates();
                    RaisePropertyChanged("StartingYear");
                }
            }
        }
        public int TermNumber
        {
            get
            {
                return _termYear.TermNumber;
            }
            set
            {
                if (_termYear.TermNumber != value)
                {
                    TermYear = TermCalculations.GetAttachment(StartingYear, value, TermsPerYear);
                    SetNewDates();
                    RaisePropertyChanged("TermNumber");
                }
            }
        }
        public int TermsPerYear
        {
            get
            {
                return _termYear.TermsPerYear;
            }
            set
            {
                if (_termYear.TermsPerYear != value)
                {
                    TermYear = TermCalculations.GetAttachment(StartingYear, TermNumber, value);
                    SetNewDates();
                    RaisePropertyChanged("TermsPerYear");
                }
            }
        }
        private void SetNewDates()
        {
            StartDate = _termYear.Dates.Start;
            FinishDate = _termYear.Dates.Finish;
        }
        #endregion
        #region Commands

        public Action ApplyTermExecute { get; set; }
        private void ApplyTermAndDisable()
        {
            if (ApplyTermExecute != null) { ApplyTermExecute(); }
            DatesChanged = false;
        }

        bool CanApplyTermExecute()
        {
            return DatesChanged;
        }
        public ICommand ApplyTerm 
        { 
            get 
            {
                return new RelayCommand(ApplyTermAndDisable, CanApplyTermExecute); 
            } 
        }

        #endregion

    }
}
