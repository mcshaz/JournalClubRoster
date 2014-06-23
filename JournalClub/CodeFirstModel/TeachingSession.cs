namespace JournalClub
{
    using MicroMvvm;
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.ComponentModel.DataAnnotations;
    public class TeachingSession
    {
        [Key]
        public Guid Id { get; set; }
        public DateTime SessionDate { get; set; }
    
        public virtual ICollection<Presentation> Presentations { get; set; }
    }
}
