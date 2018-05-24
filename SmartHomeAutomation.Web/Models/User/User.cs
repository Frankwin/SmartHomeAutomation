using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using SmartHomeAutomation.Web.Models.Accounts;

namespace SmartHomeAutomation.Web.Models.Users
{
    [Table(nameof(User), Schema = "Users")]
    public class User
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid UserId { get; set; }
        [MaxLength(30)]
        [Required]
        public string UserName { get; set; }
        [Required]
        public string Password { get; set; }
        [MaxLength(100)]
        [Required]
        public string EmailAddress { get; set; }
        [Required]
        public string CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; } = new DateTime();
        public string UpdatedBy { get; set; }
        public DateTime? UpdatedOn { get; set; }
        [MaxLength(1)]
        public string Status { get; set; } = "A";

        public Guid AccountId { get; set; }
        public Account Account { get; set; }
    }
}
