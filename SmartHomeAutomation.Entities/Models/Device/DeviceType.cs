using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SmartHomeAutomation.Entities.Models.Device
{
    [Table(nameof(DeviceType), Schema = "Devices")]
    public class DeviceType
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid DeviceTypeId { get; set; }
        [Required]
        public string DeviceTypeName { get; set; }

        [Required]
        public Guid DeviceCategoryId { get; set; }
        [ForeignKey("DeviceCategory")]
        public static DeviceCategory DeviceCategory { get; set; }

        public ICollection<Device> Devices { get; set; }

        #region Tracking Properties
        public DateTime CreatedAt { get; set; }
        public string CreatedBy { get; set; }
        public DateTime LastUpdatedAt { get; set; }
        public string LastUpdatedBy { get; set; }
        public bool IsDeleted { get; set; }
        #endregion
    }
}
