﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WdS.ElioPlus.Lib.Services.HangfireJobs
{
    public class ApplicationPreload : System.Web.Hosting.IProcessHostPreloadClient
    {
        public void Preload(string[] parameters)
        {
            HangfireBootstrapper.Instance.Start();
        }
    }
}