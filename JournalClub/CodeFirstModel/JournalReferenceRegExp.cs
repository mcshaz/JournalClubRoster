namespace JournalClub
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    public class JournalReferenceRegExp
    {
        [Key]
        public int Id { get; set; }
        [StringLength(255)]
        public string Regex { get; set; }
        [StringLength(100)]
        public string ExpName { get; set; }
        public bool SingleLine { get; set; }
    }
}
