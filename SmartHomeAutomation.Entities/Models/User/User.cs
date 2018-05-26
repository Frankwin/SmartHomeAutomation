using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SmartHomeAutomation.Entities.Models.User
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
        
        public Guid AccountId { get; set; }
        public Account.Account Account { get; set; }

        #region Tracking Properties
        public DateTime CreatedAt { get; set; }
        public string CreatedBy { get; set; }
        public DateTime LastUpdatedAt { get; set; }
        public string LastUpdatedBy { get; set; }
        public bool IsDeleted { get; set; }
        #endregion
    }
}
