using System.Configuration;

namespace Application
{
    /// <summary>
    /// This is the app configuration section for managing application configuration settings as required. 
    /// </summary>
    public class ApplicationSection : ConfigurationSection
    {
        public const string SectionName = "Application";

        private const string PortAttribute = "Port";

        [ConfigurationProperty(PortAttribute, IsRequired = true)]
        public int Port
        {
            get { return (int)this[PortAttribute]; }
            set { this[PortAttribute] = value; }
        }      
    }
}