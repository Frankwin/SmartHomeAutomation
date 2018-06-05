using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using SmartHomeAutomation.Domain.Enums;
using SmartHomeAutomation.Domain.Interfaces;

namespace SmartHomeAutomation.Domain.Models.Settings
{
    [Table(nameof(DeviceSetting), Schema = "Settings")]
    public class DeviceSetting : ITrackable, IObjectWithState
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity), Column(Order = 0)]
        public Guid DeviceSettingId { get; set; }
        [Column(Order = 1)]
        public Guid OwnedDeviceId { get; set; }
        public static OwnedDevice OwnedDevice { get; set; }
        [Column(Order = 2)]
        [Required]
        public string DeviceSettingName { get; set; }
        [Column(Order = 3)]
        [Required]
        public string DeviceSettingValue { get; set; }
        
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
