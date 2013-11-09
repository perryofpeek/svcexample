using System.Configuration;

namespace Application
{
    /// <summary>
    /// This is the app configuration section for managing the windows service information
    /// </summary>
    public class ServiceSection : ConfigurationSection
    {
        public const string SectionName = "Service";

        private const string NameAttribute = "Name";
        private const string DescrptionAttribute = "Description";

        [ConfigurationProperty(NameAttribute, IsRequired = true)]
        public string Name
        {
            get { return (string)this[NameAttribute]; }
            set { this[NameAttribute] = value; }
        }

        [ConfigurationProperty(DescrptionAttribute, IsRequired = true)]
        public string Description
        {
            get { return (string)this[DescrptionAttribute]; }
            set { this[DescrptionAttribute] = value; }
        }
    }
}