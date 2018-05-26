using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SmartHomeAutomation.Entities.Models.Device
{
    [Table(nameof(DeviceCategory), Schema = "Devices")]
    public class DeviceCategory
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid DeviceCategoryId { get; set; }
        [Required]
        public string DeviceCategoryName { get; set; }

        public ICollection<DeviceType> DeviceTypes { get; set; }

        #region Tracking Properties
        public DateTime CreatedAt { get; set; }
        public string CreatedBy { get; set; }
        public DateTime LastUpdatedAt { get; set; }
        public string LastUpdatedBy { get; set; }
        public bool IsDeleted { get; set; }
        #endregion
    }
}
