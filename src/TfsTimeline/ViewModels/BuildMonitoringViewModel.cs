using System.Collections.Generic;
using System.Linq;

namespace TfsTimeline.ViewModels
{
    public class BuildMonitoringViewModel
    {
        public List<string> BuildDefinitions{ get; set; }

        public string BuildMonitoringUrl { get; set; }

        public string BuildDefinitionsCommaSeparated
        {
            get
            {
                return string.Join(",", BuildDefinitions.Select(x=> "'" + x + "'"));
            }
        }
    }
}