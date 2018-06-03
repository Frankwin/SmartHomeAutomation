using System;
using System.Security.Principal;
using SmartHomeAutomation.Domain.Interfaces;

namespace SmartHomeAutomation.Services.Helpers
{
    public static class AuditTracking
    {
        public static void SetUpdateInfo(this ITrackable trackable, IPrincipal userPrincipal)
        {
            trackable.LastUpdatedBy = userPrincipal.Identity.Name;
            trackable.LastUpdatedAt = DateTime.Now;
        }

        public static void SetInsertInfo(this ITrackable trackable, IPrincipal userPrincipal)
        {
            trackable.CreatedBy = userPrincipal.Identity.Name;
            trackable.CreatedAt = DateTime.Now;
            trackable.LastUpdatedBy = userPrincipal.Identity.Name;
            trackable.LastUpdatedAt = DateTime.Now;
        }

        public static void SetDeleteInfo(this ITrackable trackable, IPrincipal userPrincipal)
        {
            trackable.LastUpdatedBy = userPrincipal.Identity.Name;
            trackable.LastUpdatedAt = DateTime.Now;
            trackable.IsDeleted = true;
        }
    }
}
