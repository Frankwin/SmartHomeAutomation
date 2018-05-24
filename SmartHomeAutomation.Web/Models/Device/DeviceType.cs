using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;

namespace SmartHomeAutomation.Web.Models.Devices
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

        [Required]
        public string CreatedBy { get; set; } = "System Generated";
        [Required]
        public DateTime CreatedOn { get; set; } = new DateTime();
        public string UpdatedBy { get; set; } = "System Generated";
        public DateTime UpdatedOn { get; set; }
        [MaxLength(1)]
        [Required]
        public string Status { get; set; } = "A";
    }
}
