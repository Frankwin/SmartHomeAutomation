using System.Collections.Generic;

namespace SmartHomeAutomation.Services
{
    public class PageResult
    {
        public int TotalCount { get; set; }
        public double TotalPages { get; set; }
        public List<dynamic> Collection { get; set; }
    }
}
