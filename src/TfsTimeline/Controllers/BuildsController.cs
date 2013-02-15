using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

using TfsTimeline.Models;
using TfsTimeline.ViewModels;

namespace TfsTimeline.Controllers
{
    public class BuildsController : Controller
    {
        private readonly IBuildService buildService;

        public BuildsController(IBuildService buildService)
        {
            this.buildService = buildService;
        }

        public ActionResult ProjectNamesView()
        {
            var projectNames = buildService.GetProjects();

            var viewModel = new LinkListViewModel
            {
                Title = "Projects on TFS",
                Links = projectNames.ToDictionary(
                    projectName => Url.Action("BuildNamesView", new { projectName }),
                    projectName => projectName)
            };

            return View("LinkListView", viewModel);
        }
        
        public ActionResult BuildNamesView(string projectName)
        {
            var buildNames = buildService.GetBuildDefinitions(projectName);

            var viewModel = new BuildDefinitionListViewModel
            {
                Title = string.Format("Builds for project {0}", projectName),
            };

            viewModel.BuildDefinitionList = new List<BuildDefinitionViewModel>();

            foreach (var buildName in buildNames)
            {
                viewModel.BuildDefinitionList.Add(new BuildDefinitionViewModel()
                    {
                        BuildName = buildName,
                        Link = Url.Action("BuildTimelineView", new { projectName, buildName })
                    });
            }

            return View("BuildDefinitionsView", viewModel);
        }

        public ActionResult BuildTimelineView(string projectName, string buildName)
        {
            return View(new LatestBuildsViewModel { ProjectName = projectName, BuildName = buildName });
        }

        [HttpPost]
        public ActionResult MonitorBuildsView(string projectName, string [] builds)
        {
            var selectedBuilds = builds.ToList().Where(x => x != "false").ToList();

            var buildMonioringViewModel = new BuildMonitoringViewModel
                {
                    BuildDefinitions = selectedBuilds,
                    BuildMonitoringUrl = this.Url.Action("LastBuildResults", "BuildsApi")
                };

            return View(buildMonioringViewModel);
        }
    }
}
