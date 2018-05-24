using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SmartHomeAutomation.Web.Models.Devices
{
    [Table(nameof(Device), Schema = "Devices")]
    public class Device
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
