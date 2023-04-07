using System.Configuration;

namespace Com.RePower.Ocv.Model.CommonModel
{
    public abstract class SingletonApplicationSettingsBase<T> : ApplicationSettingsBase where T : ApplicationSettingsBase, new()
    {
        public static T Default { get; } = ((T)(global::System.Configuration.ApplicationSettingsBase.Synchronized(new T())));
    }
}