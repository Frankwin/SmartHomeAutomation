using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using SmartHomeAutomation.Entities.Interfaces;

namespace SmartHomeAutomation.Entities.Models.Device
{
    [Table(nameof(Device), Schema = "Devices")]
    public class Device : ITrackable
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid DeviceId { get; set; }
        [Required]
        [MaxLength(100)]
        public string DeviceName { get; set; }
        
        [Required]
        public Guid DeviceTypeId { get; set; }
        [ForeignKey("DeviceTypeId")]
        public DeviceType DeviceType { get; set; }

        public Guid ManufacturerId { get; set; }
        public Manufacturer Manufacturer { get; set; }

        #region Tracking Properties
        public DateTime CreatedAt { get; set; }
        public string CreatedBy { get; set; }
        public DateTime LastUpdatedAt { get; set; }
        public string LastUpdatedBy { get; set; }
        public bool IsDeleted { get; set; }
        #endregion
    }
}
