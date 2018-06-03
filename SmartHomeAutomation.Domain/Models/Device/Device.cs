using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using SmartHomeAutomation.Domain.Enums;
using SmartHomeAutomation.Domain.Interfaces;

namespace SmartHomeAutomation.Domain.Models.Device
{
    [Table(nameof(Device), Schema = "Devices")]
    public class Device : ITrackable, IObjectWithState
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity), Column(Order = 0)]
        public Guid DeviceId { get; set; }
        [Required, MaxLength(100), Column(Order = 1)]
        public string DeviceName { get; set; }
        
        [Required, Column(Order = 2)]
        [ForeignKey("DeviceTypeId")]
        public Guid DeviceTypeId { get; set; }
        
        //public DeviceType DeviceType { get; set; }

        [Column(Order = 3)]
        [ForeignKey("ManufacturerId")]
        public Guid ManufacturerId { get; set; }
        
        //public Manufacturer Manufacturer { get; set; }

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
