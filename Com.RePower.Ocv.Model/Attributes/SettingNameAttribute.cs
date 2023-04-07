namespace Com.RePower.Ocv.Model.Attributes
{
    [System.AttributeUsage(AttributeTargets.Field | AttributeTargets.Property, Inherited = true, AllowMultiple = false)]
    public sealed class SettingNameAttribute : Attribute
    {
        // See the attribute guidelines at
        //  http://go.microsoft.com/fwlink/?LinkId=85236
        private readonly string _name;

        // This is a positional argument
        public SettingNameAttribute(string positionalString)
        {
            this._name = positionalString;
        }

        public string Name
        {
            get { return _name; }
        }
    }
}