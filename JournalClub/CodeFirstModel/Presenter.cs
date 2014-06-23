namespace JournalClub
{
    using MicroMvvm;
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Collections.Specialized;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    public class Presenter
    {
        [Key]
        public System.Guid Id { get; set; }
        [StringLength(100)]
        [DataType(DataType.EmailAddress)]
        public string PersonalEmail { get; set; }
        [StringLength(100)]
        [DataType(DataType.EmailAddress)]
        public string WorkEmail { get; set; }
        public bool IsRegistrar { get; set; }
        [StringLength(100)]
        public string FullName {get; set;}
    
        public virtual ICollection<PicuAttachment> PicuAttachments { get; set; }
        public virtual ICollection<Presentation> Presentations { get; set; }
    }

}
