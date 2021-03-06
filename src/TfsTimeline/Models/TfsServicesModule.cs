﻿using System.Configuration;
using System.Net;
using Microsoft.TeamFoundation.Build.Client;
using Microsoft.TeamFoundation.Client;
using Microsoft.TeamFoundation.TestManagement.Client;
using Microsoft.TeamFoundation.VersionControl.Client;

using Ninject.Modules;

namespace TfsTimeline.Models
{
    public class TfsServicesModule : NinjectModule
    {
        public override void Load()
        {
            var teamFoundationServerUrl = ConfigurationManager.AppSettings["TeamFoundationServerUrl"];
            var tfsUser = ConfigurationManager.AppSettings["TFS_User"];
            var tfsPassword = ConfigurationManager.AppSettings["TFS_Password"];
            var tfs = new TfsTeamProjectCollection(TfsTeamProjectCollection.GetFullyQualifiedUriForName(teamFoundationServerUrl), new NetworkCredential(tfsUser, tfsPassword));

            Bind<IBuildServer>().ToMethod(ctx => tfs.GetService<IBuildServer>());
            Bind<VersionControlServer>().ToMethod(ctx => tfs.GetService<VersionControlServer>());
            Bind<ITestManagementService>().ToMethod(ctx => tfs.GetService<ITestManagementService>());

            Bind<IBuildService>().To<Tfs2010BuildService>().InSingletonScope();
        }
    }
}
