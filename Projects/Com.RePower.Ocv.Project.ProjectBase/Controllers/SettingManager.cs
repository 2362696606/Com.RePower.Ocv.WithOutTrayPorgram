using System.Reflection;

namespace Com.RePower.Ocv.Project.ProjectBase.Controllers
{
    public class SettingManager<T> where T : class
    {
        private static readonly Lazy<T> LazyInstance = new Lazy<T>(() =>
        {
            var cors = typeof(T).GetConstructors(
                    BindingFlags.Instance
                    | BindingFlags.NonPublic
                    | BindingFlags.Public);
            if (cors.Count() != 1)
                throw new InvalidOperationException($"Type {typeof(T)} must have exactly one constructor.");
            var ctor = cors.SingleOrDefault(c => !c.GetParameters().Any() && c.IsPrivate);
            if (ctor == null)
                throw new InvalidOperationException(
                    $"The constructor for {typeof(T)} must be private and take no parameters.");
            return (T)ctor.Invoke(null);
        }, true);

        public static T Instance => LazyInstance.Value;
    }
}