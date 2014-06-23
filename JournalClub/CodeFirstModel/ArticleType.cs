namespace JournalClub
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    public class ArticleType
    {
        [Key]
        public int Id { get; set; }
        [StringLength(50)]
        public string Description { get; set; }

        public virtual ICollection<Article> Articles { get; set; }
    }
}
