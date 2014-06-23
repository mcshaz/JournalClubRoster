namespace JournalClub
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    
    public class Article
    {
        [Key]
        public int Id { get; set; }
        [StringLength(255)]
        public string Title { get; set; }
        [StringLength(255)]
        public string Journal { get; set; }
        [Range(1995, 2050)]
        public short YearPublished { get; set; }
        [StringLength(255)]
        public string Authors { get; set; }

        public int ArticleTypeId { get; set; }
        [StringLength(255)]
        public string PowerPointLocation { get; set; }
        public Nullable<Guid> PresentationId { get; set; }
        [StringLength(255)]
        public string ArticleLocation { get; set; }
        [StringLength(100)]
        public string Reference { get; set; }
        public Nullable<int> PMID { get; set; }

        [ForeignKey("ArticleTypeId")]
        public virtual ArticleType ArticleType { get; set; }
        [ForeignKey("PresentationId")]
        public virtual Presentation Presentation { get; set; }
    }
}
