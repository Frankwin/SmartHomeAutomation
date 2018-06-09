using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using SmartHomeAutomation.Domain.Enums;
using SmartHomeAutomation.Domain.Interfaces;

namespace SmartHomeAutomation.Domain.Models.DeviceModels
{
    [Table(nameof(DeviceCategory), Schema = "Devices")]
    public class DeviceCategory : ITrackable, IObjectWithState
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity), Column(Order = 0)]
        public Guid DeviceCategoryId { get; set; }
        [Required, Column(Order = 1)]
        public string DeviceCategoryName { get; set; }

        public ICollection<DeviceType> DeviceTypes { get; set; }
        
        #region Tracking Properties
        [Column(Order = 2)]
        public DateTime CreatedAt { get; set; }
        [Column(Order = 3)]
        public string CreatedBy { get; set; }
        [Column(Order = 4)]
        public DateTime LastUpdatedAt { get; set; }
        [Column(Order = 5)]
        public string LastUpdatedBy { get; set; }
        [Column(Order = 6)]
        public bool IsDeleted { get; set; }
        #endregion

        [NotMapped]
        public ObjectState ObjectState { get; set; }
    }
}
