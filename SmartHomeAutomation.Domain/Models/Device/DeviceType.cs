using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using SmartHomeAutomation.Domain.Enums;
using SmartHomeAutomation.Domain.Interfaces;

namespace SmartHomeAutomation.Domain.Models.Device
{
    [Table(nameof(DeviceType), Schema = "Devices")]
    public class DeviceType : ITrackable, IObjectWithState
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity), Column(Order = 0)]
        public Guid DeviceTypeId { get; set; }
        [Required, Column(Order = 1)]
        public string DeviceTypeName { get; set; }

        [Required, Column(Order = 2)]
        public Guid DeviceCategoryId { get; set; }
        [ForeignKey("DeviceCategoryId")]
        public static DeviceCategory DeviceCategory { get; set; }

        public ICollection<Device> Devices { get; set; }

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

        [NotMapped]
        public ObjectState ObjectState { get; set; }
    }
}
