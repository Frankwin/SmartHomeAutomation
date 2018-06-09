using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using SmartHomeAutomation.Domain.Enums;
using SmartHomeAutomation.Domain.Interfaces;
using SmartHomeAutomation.Domain.Models.SettingsModels;

namespace SmartHomeAutomation.Domain.Models.DeviceModels
{
    [Table(nameof(Device), Schema = "Devices")]
    public class Device : ITrackable, IObjectWithState
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity), Column(Order = 0)]
        public Guid DeviceId { get; set; }
        [Required, MaxLength(100), Column(Order = 1)]
        public string DeviceName { get; set; }
        
        [Required, Column(Order = 2)]
        public Guid DeviceTypeId { get; set; }

        [ForeignKey("DeviceTypeId")]
        public DeviceType DeviceType { get; set; }

        public List<OwnedDevice> OwnedDevice { get; set; }

        [Column(Order = 3)]
        public Guid ManufacturerId { get; set; }

        [ForeignKey("ManufacturerId")]
        public Manufacturer Manufacturer { get; set; }

        #region Tracking Properties
        [Column(Order = 4)]
        public DateTime CreatedAt { get; set; }
        [Column(Order = 5)]
        public string CreatedBy { get; set; }
        [Column(Order = 6)]
        public DateTime LastUpdatedAt { get; set; }
        [Column(Order = 7)]
        public string LastUpdatedBy { get; set; }
        [Column(Order = 8)]
        public bool IsDeleted { get; set; }
        #endregion

        [NotMapped]
        public ObjectState ObjectState { get; set; }
    }
}
