namespace CMS.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("tx_Reminder")]
    public partial class Reminder
    {
        [Key]
        public int ReminderId { get; set; }

        [StringLength(200)]
        public string Title { get; set; }

        public string Description { get; set; }

        public DateTime ReminderTime { get; set; }

        [StringLength(128)]
        public string CreatedBy { get; set; }

        public DateTime CreatedOn { get; set; }

        public bool IsRead { get; set; }

        public bool IsActive { get; set; }

        public virtual AspNetUser User { get; set; }
    }
}
