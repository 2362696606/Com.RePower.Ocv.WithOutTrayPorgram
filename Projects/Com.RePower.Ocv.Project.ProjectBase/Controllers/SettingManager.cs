﻿using Com.RePower.Ocv.Model.DataBaseContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Com.RePower.Ocv.Project.ProjectBase.Controllers
{
    public class SettingManager<T> where T : class
    {
        private static readonly Lazy<T> _instance = new Lazy<T>(() =>
        {
            var ctors = typeof(T).GetConstructors(
                BindingFlags.Instance
                | BindingFlags.NonPublic
                | BindingFlags.Public);
            if (ctors.Count() != 1)
                throw new InvalidOperationException(String.Format("Type {0} must have exactly one constructor.", typeof(T)));
            var ctor = ctors.SingleOrDefault(c => !c.GetParameters().Any() && c.IsPrivate);
            if (ctor == null)
                throw new InvalidOperationException(String.Format("The constructor for {0} must be private and take no parameters.", typeof(T)));
            return (T)ctor.Invoke(null);
        }, true);


        public static T Instance
        {
            get
            {
                return _instance.Value;
            }
        }
    }
}
