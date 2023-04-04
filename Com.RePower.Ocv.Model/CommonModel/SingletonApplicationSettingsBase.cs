using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Com.RePower.Ocv.Model.CommonModel
{
    public class SingletonApplicationSettingsBase<T>:ApplicationSettingsBase where T:ApplicationSettingsBase, new()
    {
        protected static T DefaultInstance = ((T)(global::System.Configuration.ApplicationSettingsBase.Synchronized(new T())));

        public static T Default
        {
            get
            {
                return DefaultInstance;
            }
        }
    }
}
