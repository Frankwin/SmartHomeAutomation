using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using SmartHomeAutomation.Domain.Enums;
using SmartHomeAutomation.Domain.Interfaces;
using SmartHomeAutomation.Domain.Models.AccountModels;
using SmartHomeAutomation.Domain.Models.DeviceModels;

namespace SmartHomeAutomation.Domain.Models.SettingsModels
{
    [Table(nameof(OwnedDevice), Schema = "Settings")]
    public class OwnedDevice : ITrackable, IObjectWithState
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity), Column(Order = 0)]
        public Guid OwnedDeviceId { get; set; }
        [Column(Order = 1)]
        
        public Guid? DeviceId { get; set; }
        [ForeignKey("DeviceId")]
        public Device Device { get; set; }
        [Column(Order = 2)]
        public Guid? RoomId { get; set; }
        [ForeignKey("RoomId")]
        public Room Room { get; set; }
        [Column(Order = 3)]
        [Required]
        public string DeviceName { get; set; }
        [Column(Order = 4)]
        public string DeviceDescription { get; set; }
        [Column(Order = 5)]
        public Guid AccountId { get; set; }
        [ForeignKey("AccountId")]
        public Account Account { get; set; }
        public List<DeviceSetting> DeviceSettings { get; set; }

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
