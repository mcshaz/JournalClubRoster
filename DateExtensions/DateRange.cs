using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DateExtensions
{
    public class DateRange : IEquatable<DateRange>, ICloneable
    {
        private DateTime _start;
        private DateTime _finish;
        public DateTime Start { 
            get { return _start; }
            set 
            { 
                if (_finish != DateTime.MinValue && value > _finish) { throw new ArgumentOutOfRangeException("Attempted to assign a Start Date which is after Finish Date"); }
                _start = value;
            }
        }
        public DateTime Finish {
            get { return _finish; }
            set
            {
                if (value < _start) { throw new ArgumentOutOfRangeException("Attempted to assign a Finish Date which is before Start Date"); }
                _finish = value;
            }
        }
        public bool IsInRange(DateTime date)
        {
            if (_start == DateTime.MinValue || _finish == DateTime.MinValue) { throw new Exception("All DateRange properties not set"); }
            return _start < date && _finish > date;
        }
        public bool InRange(DateTime? date, bool IncludeNull = false)
        {
            if (date.HasValue) { return IsInRange(date.Value); }
            return IncludeNull;
        }
        #region ICloneable
        public Object Clone()
        {
            return new DateRange { Start = _start, Finish = _finish };
        }
        #endregion
        #region equality
        public override bool Equals(object obj)
        {
            return this.Equals(obj as DateRange);
        }

        public bool Equals(DateRange d)
        {
            // If parameter is null, return false. 
            if (Object.ReferenceEquals(d, null))
            {
                return false;
            }

            // Optimization for a common success case. 
            if (Object.ReferenceEquals(this, d))
            {
                return true;
            }

            // If run-time types are not exactly the same, return false. 
            if (this.GetType() != d.GetType())
            {
                return false;
            }

            // Return true if the fields match. 
            // Note that the base class is not invoked because it is 
            // System.Object, which defines Equals as reference equality. 
            return (Start == d.Start) && (Finish == d.Finish);
        }

        public override int GetHashCode()
        {
            return Start.GetHashCode() * 0x00010000 + Finish.GetHashCode();
        }

        public static bool operator ==(DateRange lhs, DateRange rhs)
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

        public static bool operator !=(DateRange lhs, DateRange rhs)
        {
            return !(lhs == rhs);
        }
#endregion
    }
    public static class DateRangeExtensions
    {
        public static IEnumerable<DateRange> IntersectDates(this IEnumerable<DateRange> dateRangeList, DateRange range)
        {
            var returnVar = new List<DateRange>();
            foreach (var d in dateRangeList)
            {
                switch (d.OverlapsWith(range))
                {
                    case DateOverlap.EalierOverlapping:
                        returnVar.Add(new DateRange { Start = range.Start, Finish = d.Finish });
                        break;
                    case DateOverlap.LaterOverLapping:
                        returnVar.Add(new DateRange { Start = d.Start, Finish = range.Finish });
                        break;
                    default:
                        returnVar.Add((DateRange)d.Clone());
                        break;
                }
            }
            return returnVar;
        }
        public enum DateOverlap { EntirelySurrounded, EntirelySurrounding, EalierOverlapping, LaterOverLapping, NotOverlapping, ExactlyEqual }
        public static DateOverlap OverlapsWith(this DateRange primaryRange, DateRange comparisonRange)
        {
            if (comparisonRange == primaryRange) { return DateOverlap.ExactlyEqual; }
            if (comparisonRange.Start > primaryRange.Finish || comparisonRange.Finish < primaryRange.Start) { return DateOverlap.NotOverlapping; }
            if (comparisonRange.Start < primaryRange.Start)
            {
                if (comparisonRange.Finish > primaryRange.Finish) { return DateOverlap.EntirelySurrounded; }
                return DateOverlap.EalierOverlapping;
            }
            if (comparisonRange.Finish < primaryRange.Finish) { return DateOverlap.EntirelySurrounding; }
            return DateOverlap.LaterOverLapping;
        }
    }
}
