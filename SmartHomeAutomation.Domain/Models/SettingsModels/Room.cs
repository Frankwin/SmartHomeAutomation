using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using SmartHomeAutomation.Domain.Enums;
using SmartHomeAutomation.Domain.Interfaces;
using SmartHomeAutomation.Domain.Models.AccountModels;

namespace SmartHomeAutomation.Domain.Models.SettingsModels
{
    [Table(nameof(Room), Schema = "Settings")]
    public class Room : ITrackable, IObjectWithState
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity), Column(Order = 0)]
        public Guid RoomId { get; set; }
        [MaxLength(50), Column(Order = 1)]
        public string RoomName { get; set; }
        [MaxLength(200), Column(Order = 2)]
        public string RoomDescription { get; set; }
        [Column(Order = 3)]
        public Guid AccountId { get; set; }
        [ForeignKey("AccountId")]
        public Account Account { get; set; }
        public ICollection<OwnedDevice> LinkedDevices { get; set; }

        [NotMapped]
        public ObjectState ObjectState { get; set; }

        #region Tracking Properties
        [Column(Order = 3)]
        public DateTime CreatedAt { get; set; }
        [Column(Order = 4)]
        public string CreatedBy { get; set; }
        [Column(Order = 5)]
        public DateTime LastUpdatedAt { get; set; }
        [Column(Order = 6)]
        public string LastUpdatedBy { get; set; }
        [Column(Order = 7)]
        public bool IsDeleted { get; set; }
        #endregion
    }
}
