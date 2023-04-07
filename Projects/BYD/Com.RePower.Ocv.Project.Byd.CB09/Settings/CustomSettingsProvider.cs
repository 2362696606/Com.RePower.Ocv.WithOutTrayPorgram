using System.Collections;
using System.Collections.Specialized;
using System.Configuration;
using System.Diagnostics;
using System.Globalization;
using System.Xml;

namespace Com.RePower.Ocv.Project.Byd.CB09.Settings;

//[
//     PermissionSet(SecurityAction.LinkDemand, Name = "FullTrust"),
//     PermissionSet(SecurityAction.InheritanceDemand, Name = "FullTrust")
//]
public class CustomSettingsProvider : SettingsProvider
{
    private const string UserSettingsGroupName = "userSettings";
    private string _applicationName = string.Empty;
    public override string ApplicationName { get => _applicationName; set => _applicationName = value; }

    public override void Initialize(string name, NameValueCollection values)
    {
        if (String.IsNullOrEmpty(name))
        {
            name = "CustomProvider";
        }

        base.Initialize(name, values);
    }

    private Configuration? _configuration;

    private void Open()
    {
        var fileMap = new ExeConfigurationFileMap
        {
            ExeConfigFilename = $"{_applicationName}.exe.config",
            RoamingUserConfigFilename = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\" + _applicationName + "\\Settings\\user.config"
        };
        _configuration = ConfigurationManager.OpenMappedExeConfiguration(fileMap, ConfigurationUserLevel.PerUserRoaming);
    }

    //[
    // FileIOPermission(SecurityAction.Assert, AllFiles = FileIOPermissionAccess.PathDiscovery | FileIOPermissionAccess.Read),
    // PermissionSet(SecurityAction.LinkDemand, Name = "FullTrust"),
    // PermissionSet(SecurityAction.InheritanceDemand, Name = "FullTrust")
    //]
    public override SettingsPropertyValueCollection GetPropertyValues(SettingsContext context, SettingsPropertyCollection collection)
    {
        Open();
        var sectionName = GetSectionName(context);
        Trace.Assert(!string.IsNullOrEmpty(sectionName), $"Failed to get {context}");
        if (!string.IsNullOrEmpty(sectionName))
        {
            var settings = ReadSettingsFromFile(sectionName);
            var values = new SettingsPropertyValueCollection();
            foreach (SettingsProperty settingProperty in collection)
            {
                var value = new SettingsPropertyValue(settingProperty);
                if (settings.Contains(settingProperty.Name))
                {
                    if (settings[settingProperty.Name] is { } tempSetting)
                    {
                        var ss = (StoredSetting)tempSetting;
                        var valueString = ss.XmlNode.InnerXml;
                        if (ss.SerializeAs == SettingsSerializeAs.String)
                        {
                            valueString = _escaper.Unescape(valueString);
                        }

                        value.SerializedValue = valueString;
                    }
                    else
                    {
                        throw new ArgumentNullException($"{settingProperty.Name}对应项为null");
                    }
                }
                else if (settingProperty.DefaultValue != null)
                {
                    value.SerializedValue = settingProperty.DefaultValue;
                }

                value.IsDirty = false;
                values.Add(value);
            }

            return values;
        }
        else
        {
            throw new ArgumentNullException($"Failed to get {context}");
        }
    }

    // ReSharper disable once IdentifierTypo
    private readonly XmlEscaper _escaper = new();

    private IDictionary ReadSettingsFromFile(string sectionName)
    {
        IDictionary settings = new Hashtable();
        var sectionGroup = _configuration?.GetSectionGroup(UserSettingsGroupName);
        //var section = sectionGroup?.Sections[sectionName] as ClientSettingsSection;
        if (sectionGroup?.Sections[sectionName] is ClientSettingsSection section)
        {
            foreach (SettingElement setting in section.Settings)
            {
                settings[setting.Name] = new StoredSetting(setting.SerializeAs, setting.Value.ValueXml);
            }
        }

        return settings;
    }

    private string GetSectionName(SettingsContext context)
    {
        var groupName = (string?)context["GroupName"];
        var key = (string?)context["SettingsKey"];

        Trace.Assert(groupName != null, "SettingsContext did not have a GroupName!");

        if (!string.IsNullOrEmpty(groupName))
        {
            string sectionName = groupName;

            if (!String.IsNullOrEmpty(key))
            {
                sectionName = string.Format(CultureInfo.InvariantCulture, "{0}.{1}", sectionName, key);
            }
            return XmlConvert.EncodeLocalName(sectionName);
        }
        else
        {
            throw new ArgumentNullException($"{nameof(groupName)} is null");
        }
    }

    public override void SetPropertyValues(SettingsContext context, SettingsPropertyValueCollection collection)
    {
        string sectionName = GetSectionName(context);
        IDictionary userSettings = new Hashtable();
        foreach (SettingsPropertyValue value in collection)
        {
            SettingsProperty setting = value.Property;
            if (value.IsDirty)
            {
                StoredSetting ss = new StoredSetting(setting.SerializeAs, SerializeToXmlElement(setting, value));
                userSettings[setting.Name] = ss;
            }
        }
        WriteSettings(sectionName, userSettings);
    }

    private void WriteSettings(string sectionName, IDictionary newSettings)
    {
        Open();
        var section = GetConfigSection(sectionName);

        if (section != null)
        {
            SettingElementCollection sec = section.Settings;
            foreach (DictionaryEntry entry in newSettings)
            {
                SettingElement se = sec.Get((string)entry.Key);

                if (se == null)
                {
                    se = new SettingElement
                    {
                        Name = (string)entry.Key
                    };
                    sec.Add(se);
                }

                //StoredSetting ss = (StoredSetting)entry.Value;
                if (entry.Value is StoredSetting ss)
                {
                    se.SerializeAs = ss.SerializeAs;
                    se.Value.ValueXml = ss.XmlNode;
                }
            }

            try
            {
                _configuration?.Save();
            }
            catch (ConfigurationErrorsException ex)
            {
                // We wrap this in an exception with our error message and throw again.
                throw new ConfigurationErrorsException($"Save file to {_configuration?.FilePath} failed", ex);
            }
        }
        else
        {
            throw new ConfigurationErrorsException($"Can not find the section {section} in the setting file");
        }
    }

    private ClientSettingsSection? GetConfigSection(string sectionName)
    {
        var config = _configuration;
        var fullSectionName = UserSettingsGroupName + "/" + sectionName;
        ClientSettingsSection? section = null;

        if (config != null)
        {
            section = config.GetSection(fullSectionName) as ClientSettingsSection;

            if (section == null)
            {
                // Looks like the section isn't declared - let's declare it and try again.
                DeclareSection(sectionName);
                section = config.GetSection(fullSectionName) as ClientSettingsSection;
            }
        }

        return section;
    }

    // Declares the section handler of a given section in its section group, if a declaration isn't already
    // present.
    private void DeclareSection(string sectionName)
    {
        var config = _configuration;
        var settingsGroup = config?.GetSectionGroup(UserSettingsGroupName);

        if (settingsGroup == null)
        {
            //Declare settings group
            ConfigurationSectionGroup group = new UserSettingsGroup();
            config?.SectionGroups.Add(UserSettingsGroupName, group);
        }

        settingsGroup = config?.GetSectionGroup(UserSettingsGroupName);

        Trace.Assert(settingsGroup != null, "Failed to declare settings group");

        if (settingsGroup != null)
        {
            ConfigurationSection section = settingsGroup.Sections[sectionName];
            if (section == null)
            {
                section = new ClientSettingsSection();
                section.SectionInformation.AllowExeDefinition = ConfigurationAllowExeDefinition.MachineToLocalUser;
                section.SectionInformation.RequirePermission = false;
                settingsGroup.Sections.Add(sectionName, section);
            }
        }
    }

    private XmlNode SerializeToXmlElement(SettingsProperty setting, SettingsPropertyValue value)
    {
        XmlDocument doc = new XmlDocument();
        XmlElement valueXml = doc.CreateElement("value");

        string? serializedValue = value.SerializedValue as string;

        if (serializedValue == null && setting.SerializeAs == SettingsSerializeAs.Binary)
        {
            // SettingsPropertyValue returns a byte[] in the binary serialization case. We need to
            // encode this - we use base64 since SettingsPropertyValue understands it and we won't have
            // to special case while deserializing.
            if (value.SerializedValue is byte[] buf)
            {
                serializedValue = Convert.ToBase64String(buf);
            }
        }

        serializedValue ??= String.Empty;

        // We need to escape string serialized values
        if (setting.SerializeAs == SettingsSerializeAs.String)
        {
            serializedValue = _escaper.Escape(serializedValue);
        }

        valueXml.InnerXml = serializedValue;

        // Hack to remove the XmlDeclaration that the XmlSerializer adds.
        XmlNode? unwanted = null;
        foreach (XmlNode child in valueXml.ChildNodes)
        {
            if (child.NodeType == XmlNodeType.XmlDeclaration)
            {
                unwanted = child;
                break;
            }
        }
        if (unwanted != null)
        {
            valueXml.RemoveChild(unwanted);
        }

        return valueXml;
    }

    // ReSharper disable once IdentifierTypo
    private class XmlEscaper
    {
        private readonly XmlElement _temp;

        // ReSharper disable once IdentifierTypo
        internal XmlEscaper()
        {
            var doc = new XmlDocument();
            _temp = doc.CreateElement("temp");
        }

        internal string Escape(string xmlString)
        {
            if (String.IsNullOrEmpty(xmlString))
            {
                return xmlString;
            }

            _temp.InnerText = xmlString;
            return _temp.InnerXml;
        }

        // ReSharper disable once IdentifierTypo
        internal string Unescape(string escapedString)
        {
            if (String.IsNullOrEmpty(escapedString))
            {
                return escapedString;
            }

            _temp.InnerXml = escapedString;
            return _temp.InnerText;
        }
    }
}

internal class StoredSetting
{
    public StoredSetting(SettingsSerializeAs serializeAs, XmlNode xmlNode)
    {
        this.SerializeAs = serializeAs;
        this.XmlNode = xmlNode;
    }

    internal SettingsSerializeAs SerializeAs;
    internal XmlNode XmlNode;
}