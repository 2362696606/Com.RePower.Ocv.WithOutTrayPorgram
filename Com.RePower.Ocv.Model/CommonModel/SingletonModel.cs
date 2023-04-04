using System.Reflection;

namespace Com.RePower.Ocv.Model.CommonModel
{
    /// <summary>
    /// 单例模型
    /// </summary>
    public class SingletonModel<T> where T : class
    {
        private static readonly Lazy<T> _instance = new(() =>
        {
            var ctors = typeof(T).GetConstructors(
                BindingFlags.Instance
                | BindingFlags.NonPublic
                | BindingFlags.Public);
            if (ctors.Count() != 1)
                throw new InvalidOperationException($"Type {typeof(T)} must have exactly one constructor.");
            var ctor = ctors.SingleOrDefault(c => !c.GetParameters().Any() && c.IsPrivate);
            if (ctor == null)
                throw new InvalidOperationException(
                    $"The constructor for {typeof(T)} must be private and take no parameters.");
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