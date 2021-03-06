﻿using Microsoft.EntityFrameworkCore;
using SmartHomeAutomation.Domain.Enums;
using SmartHomeAutomation.Domain.Interfaces;

namespace SmartHomeAutomation.Services.Helpers
{
    public static class StateHelper
    {
        public static EntityState ConvertState(ObjectState state)
        {
            switch (state)
            {
                case ObjectState.Added:
                    return EntityState.Added;
                case ObjectState.Modified:
                    return EntityState.Modified;
                case ObjectState.Deleted:
                    return EntityState.Deleted;
                case ObjectState.Detached:
                    return EntityState.Detached;
                                default:
                    return EntityState.Unchanged;
            }
        }

        public static void ApplyStateChanges(this DbContext context)
        {
            foreach (var entry in context.ChangeTracker.Entries<IObjectWithState>())
            {
                var stateInfo = entry.Entity;
                entry.State = ConvertState(stateInfo.ObjectState);
            }
        }
    }
}
