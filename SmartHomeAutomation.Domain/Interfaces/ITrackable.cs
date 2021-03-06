﻿using System;

namespace SmartHomeAutomation.Domain.Interfaces
{
    public interface ITrackable
    {
        DateTime CreatedAt { get; set; }
        string CreatedBy { get; set; }
        DateTime LastUpdatedAt { get; set; }
        string LastUpdatedBy { get; set; }
        bool IsDeleted { get; set; }
    }
}
