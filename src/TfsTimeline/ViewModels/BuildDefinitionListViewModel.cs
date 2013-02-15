using System.Collections.Generic;

namespace TfsTimeline.ViewModels
{
    public class BuildDefinitionListViewModel
    {
        public string Title { get; set; }

        public List<BuildDefinitionViewModel> BuildDefinitionList { get; set; }
    }
}