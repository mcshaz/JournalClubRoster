using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace DateExtensions
{
    public class StaffAttachment : IEquatable<StaffAttachment>
    {
        private int _termNumber;
        private int _termsPerYear;
        public DateRange Dates {get;set;}
        public int TermNumber 
        {
            get {return _termNumber;} 
            set 
            {
                if (TermsPerYear!=0 && TermsPerYear<value) { throw new ArgumentOutOfRangeException("Must be less than or equal to TermsPerYear"); }
                _termNumber = value;
            }
        }
        public int TermsPerYear 
        {
            get
            {
                return _termsPerYear;
            }
            set
            {
                if (value <0 || value >8 ) { throw new ArgumentOutOfRangeException("Must be between 0 & 8"); }
                if (TermNumber != 0 && value < TermNumber) {throw new ArgumentOutOfRangeException("Must be greater than or equal to the TermNumber");}
                _termsPerYear = value;
            }
        }
        [Range(2000, 2050)]
        public int YearCommencing {get;set; }

        #region equality
        public override bool Equals(object obj)
        {
            return this.Equals(obj as StaffAttachment);
        }

        public bool Equals(StaffAttachment s)
        {
            // If parameter is null, return false. 
            if (Object.ReferenceEquals(s, null))
            {
                return false;
            }

            // Optimization for a common success case. 
            if (Object.ReferenceEquals(this, s))
            {
                return true;
            }

            // If run-time types are not exactly the same, return false. 
            if (this.GetType() != s.GetType())
            {
                return false;
            }

            // Return true if the fields match. 
            // Note that the base class is not invoked because it is 
            // System.Object, which defines Equals as reference equality. 
            return (s.TermNumber==TermNumber && s.TermsPerYear == TermsPerYear && s.YearCommencing == YearCommencing);
        }

        public override int GetHashCode()
        {
            return TermNumber * 0x00010000 + TermsPerYear + YearCommencing * 0x01000000;
        }

        public static bool operator ==(StaffAttachment lhs, StaffAttachment rhs)
        {
            // Check for null on left side. 
            if (Object.ReferenceEquals(lhs, null))
            {
                if (Object.ReferenceEquals(rhs, null))
                {
                    // null == null = true. 
                    return true;
                }

                // Only the left side is null. 
                return false;
            }
            // Equals handles case of null on right side. 
            return lhs.Equals(rhs);
        }

        public static bool operator !=(StaffAttachment lhs, StaffAttachment rhs)
        {
            return !(lhs == rhs);
        }
        #endregion
    }
    internal class StaffAttachmentDetails : StaffAttachment
    {
        [Range(1, 12)]
        internal int StartingMonth { get; set; }
        [Range(1, 5)]
        internal int MondayOfMonth { get; set; }
    }
    public static class TermCalculations
    {

        public static StaffAttachment GetAttachment(DateTime dateWithinTerm, int termsPerYear = 2, int startingMonth = 12, int mondayOfMonth = 2)
        {
            var returnVar = new StaffAttachmentDetails
            {
                TermsPerYear = termsPerYear,
                StartingMonth = startingMonth,
                MondayOfMonth = mondayOfMonth
            };
            if (WeekDayUtilities.NthWeekdayOfMonth(dateWithinTerm.Year, startingMonth, mondayOfMonth) > dateWithinTerm)
            {
                returnVar.YearCommencing = dateWithinTerm.Year - 1;
            }
            else
            {
                returnVar.YearCommencing = dateWithinTerm.Year;
            }
            var termDiv = CalculateTermDivisions(returnVar);

            returnVar.TermNumber = (int)((double)((dateWithinTerm - termDiv.YearCommence).Days) / termDiv.DaysPerTerm) + 1;
            returnVar.Dates = CalculateTerm(termDiv.YearCommence, termDiv.DaysPerTerm, returnVar.TermNumber);
            return returnVar;

        }
        /// <param name="termNumber">starting at term 1</param>
        public static StaffAttachment GetAttachment(int startingYear, int termNumber, int termsPerYear = 2, int startingMonth = 12, int mondayOfMonth = 2)
        {
            var returnVar = new StaffAttachmentDetails
            {
                TermNumber = termNumber,
                TermsPerYear = termsPerYear,
                StartingMonth = startingMonth,
                MondayOfMonth = mondayOfMonth,
                YearCommencing = startingYear
            };
            var termDiv = CalculateTermDivisions(returnVar);

            returnVar.Dates = CalculateTerm(termDiv.YearCommence, termDiv.DaysPerTerm, returnVar.TermNumber);
            return returnVar;
        }
        private class TermDivisions
        {
            public DateTime YearCommence { get; set; }
            public Double DaysPerTerm { get; set; }
        }
        private static TermDivisions CalculateTermDivisions(StaffAttachmentDetails startingYear)
        {

            var yearDates = new DateRange
            {
                Start = WeekDayUtilities.NthWeekdayOfMonth(startingYear.YearCommencing, startingYear.StartingMonth, startingYear.MondayOfMonth),
                Finish = WeekDayUtilities.NthWeekdayOfMonth(startingYear.YearCommencing + 1, startingYear.StartingMonth, startingYear.MondayOfMonth)
            };
            return new TermDivisions
            {
                YearCommence = yearDates.Start,
                DaysPerTerm = (double)(yearDates.Finish - yearDates.Start).Days / startingYear.TermsPerYear
            };
        }
        private static DateRange CalculateTerm(DateTime yearCommence, double daysPerTerm, int termNumber)
        {
            return new DateRange
            {
                Start = yearCommence.AddDays(Math.Floor(daysPerTerm * (termNumber - 1))),
                Finish = yearCommence.AddDays(Math.Floor(daysPerTerm * (termNumber)) - 1)
            };
        }
    }
}
