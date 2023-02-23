using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Com.RePower.Ocv.Project.ProjectBase.Controllers
{
    public class SettingManager<T> where T : class,new()
    {
        private static readonly Lazy<T> _instance =
           new Lazy<T>(() => new T());
        /// <summary>
        /// 单例静态实例
        /// </summary>
        public static T Instance { get { return _instance.Value; } }
    }
}
