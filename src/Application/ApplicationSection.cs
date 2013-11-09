using System.Configuration;

namespace Application
{
    public class ApplicationSection : ConfigurationSection
    {
        public const string SectionName = "Application";

        private const string PortAttribute = "Port";

        [ConfigurationProperty("Port", IsRequired = true)]
        public int Port
        {
            get { return (int)this["Port"]; }
            set { this["Port"] = value; }
        }      
    }
}