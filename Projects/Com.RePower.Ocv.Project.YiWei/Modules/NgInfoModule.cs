﻿using Autofac;
using Com.RePower.Ocv.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Com.RePower.Ocv.Project.YiWei.Modules
{
    public class NgInfoModule:Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<NgInfo>()
                .AsSelf()
                .SingleInstance();
        }
    }
}
