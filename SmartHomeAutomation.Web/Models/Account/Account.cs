using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SmartHomeAutomation.Web.Models.Accounts
{
    [Table(nameof(Account), Schema="Accounts")]
    public class Account
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid AccountId { get; set; }
        [MaxLength(50)]
        [Required]
        public string AccountName { get; set; }
        [Required]
        public string CreatedBy { get; set; }
        [Required]
        public DateTime CreatedOn { get; set; } = new DateTime();
        public string UpdatedBy { get; set; }
        public DateTime UpdatedOn { get; set; }
        [MaxLength(1)]
        [Required]
        public string Status { get; set; } = "A";
    }
}
