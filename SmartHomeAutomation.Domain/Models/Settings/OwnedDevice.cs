using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using SmartHomeAutomation.Domain.Enums;
using SmartHomeAutomation.Domain.Interfaces;

namespace SmartHomeAutomation.Domain.Models.Settings
{
    [Table(nameof(OwnedDevice), Schema = "Settings")]
    public class OwnedDevice : ITrackable, IObjectWithState
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity), Column(Order = 0)]
        public Guid OwnedDeviceId { get; set; }
        [Column(Order = 1)]
        [ForeignKey("DeviceId")]
        public Guid DeviceId { get; set; }
        public static Device.Device Device { get; set; }
        [Column(Order = 2)]
        [ForeignKey("RoomId")]
        public Guid? RoomId { get; set; }
        public static Room Room { get; set; }
        [Column(Order = 3)]
        [Required]
        public string DeviceName { get; set; }
        [Column(Order = 4)]
        public string DeviceDescription { get; set; }
        [Column(Order = 5)]
        public Guid AccountId { get; set; }
        [ForeignKey("AccountId")]
        public static Account.Account Account { get; set; }

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
