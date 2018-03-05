namespace CMS.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("tm_EmailTemplate")]
    public partial class EmailTemplate
    {
        [Key]
        public int EmailTemplateId { get; set; }

        [StringLength(100)]
        public string TemplateName { get; set; }
        public string TemplateHtml { get; set; }

        public string BodyContent { get; set; }

        [StringLength(200)]
        public string Subject { get; set; }

        [StringLength(200)]
        public string From { get; set; }
    }
}
