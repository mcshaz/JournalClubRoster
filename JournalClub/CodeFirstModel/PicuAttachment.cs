namespace JournalClub
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;
    public class PicuAttachment
    {
        [Key]
        public Guid Id { get; set; }
        public Guid PresenterId { get; set; }
        public DateTime StartDate {get; set;}
        public DateTime? FinishDate {get; set;}
        [ForeignKey("PresenterId")]
        public virtual Presenter Presenter { get; set; }
    }

    public static class PicuAttachmentExtensions
    {
        public static PicuAttachment AdjustedOverlappingDates(this Presenter presenter, DateExtensions.DateRange dates)
        {
            var overlappingDates = presenter.PicuAttachments.FirstOrDefault(a => a.StartDate < dates.Finish || a.FinishDate > dates.Finish);
            if (overlappingDates == null) { return null; } // Id = Guid.NewGuid()
            if (overlappingDates.StartDate > dates.Start) { overlappingDates.StartDate = dates.Start; }
            if (overlappingDates.FinishDate < dates.Finish) { overlappingDates.FinishDate = dates.Finish; }
            return overlappingDates;
        }
    }
}
