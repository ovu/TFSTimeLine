﻿using System.Collections.Generic;

namespace TfsTimeline.ViewModels
{
    public class LinkListViewModel
    {
        public string Title { get; set; }

        public IDictionary<string, string> Links { get; set; }
    }
}