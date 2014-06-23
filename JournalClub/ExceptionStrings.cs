using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JournalClub
{
    public static class ExceptionStrings
    {
        public static string CreateDetailString(this Exception ex)
        {
            var sb = new StringBuilder();
            var nextEx = ex;
            sb.AppendLine("Exception thrown by application:");

            do
            {
                sb.AppendLine(new string('-', 20));
                sb.AppendLine(nextEx.ToString());
                nextEx = ex.InnerException;
            } while (nextEx != null);
            return sb.ToString();
        }
    }
}
