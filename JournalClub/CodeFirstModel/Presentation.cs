namespace JournalClub
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    public class Presentation 
    {
        [Key]
        public Guid Id { get; set; }
        public System.Guid PresenterId { get; set; }
        public Nullable<Guid> TeachingSessionId { get; set; }
        public DateTime? EmailSent { get; set; }
        public virtual ICollection<Article> Articles { get; set; }
        [ForeignKey("PresenterId")]
        public virtual Presenter Presenter { get; set; }
        [ForeignKey("TeachingSessionId")]
        public virtual TeachingSession TeachingSession {get; set;}
    }
}
