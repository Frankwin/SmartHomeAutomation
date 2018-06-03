using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using SmartHomeAutomation.Domain.Enums;
using SmartHomeAutomation.Domain.Interfaces;

namespace SmartHomeAutomation.Domain.Models.User
{
    [Table(nameof(User), Schema = "Users")]
    public class User: ITrackable, IObjectWithState
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity), Column(Order = 0)]
        public Guid UserId { get; set; }
        [MaxLength(30), Required, Column(Order = 1)]
        public string UserName { get; set; }
        [Required, Column(Order = 2)]
        public string Password { get; set; }
        [MaxLength(100), Required, Column(Order = 3)]
        public string EmailAddress { get; set; }

        [Column(Order = 4)]
        public Guid AccountId { get; set; }
        [ForeignKey("AccountId")]
        public Account.Account Account { get; set; }

        #region Tracking Properties
        [Column(Order = 5)]
        public DateTime CreatedAt { get; set; }
        [Column(Order = 6)]
        public string CreatedBy { get; set; }
        [Column(Order = 7)]
        public DateTime LastUpdatedAt { get; set; }
        [Column(Order = 8)]
        public string LastUpdatedBy { get; set; }
        [Column(Order = 9)]
        public bool IsDeleted { get; set; }
        #endregion

        [NotMapped]
        public ObjectState ObjectState { get; set; }
    }
}
