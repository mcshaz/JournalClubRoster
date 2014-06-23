using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace JournalClub
{
    public static class EmailManager
    {
        const string dateMessage = "A journal club date of {0} has been assigned to you. Please let me know if you will be unable to present.";
        public static void sendDates(IEnumerable<Presenter> presenters)
        {
            var now = DateTime.Now;
            foreach (var presenter in presenters)
            {
                if (!string.IsNullOrEmpty(presenter.WorkEmail))
                {
                    string dateString = string.Empty;
                    
                    foreach (var presentation in presenter.Presentations)
                    {
                        if (!presentation.EmailSent.HasValue && presentation.TeachingSession != null && presentation.TeachingSession.SessionDate > now)
                        {
                            dateString += presentation.TeachingSession.SessionDate.AddHours(15.5).ToString("f");
                            presentation.EmailSent = now;
                        }
                    }
                    if (dateString != string.Empty) { sendMail("Journal Presentation Date", string.Format(dateMessage, dateString), presenter.WorkEmail); }
                    
                }
            }
        }
        public static void sendMail(string subject, string message, string recipientAddress)
        {
            subject = Uri.EscapeDataString(subject);
            message= Uri.EscapeDataString(message);

            System.Diagnostics.Process proc = new System.Diagnostics.Process();
            proc.StartInfo.FileName = string.Format("mailto:{0}?subject={1}&body={2}", recipientAddress, subject, message);
            proc.Start();
        }
    }
}
