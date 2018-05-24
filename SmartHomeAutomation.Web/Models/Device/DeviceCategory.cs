using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;

namespace SmartHomeAutomation.Web.Models.Devices
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
