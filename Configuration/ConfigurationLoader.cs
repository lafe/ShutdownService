using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;
using lafe.Logging.Interface;
using lafe.ShutdownService.Configuration.Interface;

namespace lafe.ShutdownService.Configuration
{
    public class ConfigurationLoader : IConfigurationLoader
    {
        public ILog Logger { get; private set; }

        public ConfigurationLoader(ILog logger)
        {
            Logger = logger;
        }

        public Configuration LoadConfig()
        {
            try
            {

                Logger.Trace(LogNumbers.LoadingConfig, "Loading Configuration");
                var file = GetFile();
                Logger.Trace(LogNumbers.UsingConfigurationFile, string.Format("Using configuration file {0}", file));

                var config = LoadConfiguration(file);
                return config;
            }
            catch (Exception ex)
            {
                Logger.Fatal(LogNumbers.LoadingConfigException, ex, string.Format("While loading the Configuration, an error occured: {0}", ex));
                throw;
            }

        }

        protected virtual Configuration LoadConfiguration(string file)
        {
            Logger.Trace(LogNumbers.CreatingXmlReader, string.Format("Creating XML Reader for file {0}", file));
            var xmlReader = XmlReader.Create(file);
            Logger.Trace(LogNumbers.CreatingSerializer, "Creating XML Deserializer for configuration file ");
            var serializer = new XmlSerializer(typeof (Configuration));
            Logger.Trace(LogNumbers.CanDeserialize, "Checking if configuration file can be deserialized");
            var canDeserialize = serializer.CanDeserialize(xmlReader);
            if (!canDeserialize)
            {
                Logger.Fatal(LogNumbers.CannotDeserialize, string.Format("Configuration file \"{0}\" is not a valid configuration file.", file));
                throw new NotSupportedException("Invalid configuration file");
            }

            Logger.Trace(LogNumbers.DeserializingFile, "Deserializing configuration file");
            var config = (Configuration)serializer.Deserialize(xmlReader);
            Logger.Info(LogNumbers.ConfigurationLoaded, "Configuration file successfully loaded");
            return config;
        }

        /// <summary>
        /// Searches for the Configuration file and returns the first match
        /// </summary>
        /// <returns>Searches for the configuration file</returns>
        private string GetFile()
        {
            foreach (var possibleLocation in GetPossibleConfigurationLocations())
            {
                Logger.Trace(LogNumbers.SearchingConfigurationFile, string.Format("Searching configuration file at \"{0}\"", possibleLocation));
                if (File.Exists(possibleLocation))
                {
                    Logger.Trace(LogNumbers.FoundConfiguration, string.Format("Found configuration file \"{0}\"", possibleLocation));
                    return possibleLocation;
                }
                else
                {
                    Logger.Trace(LogNumbers.ConfigurationNotFoundAtLocation, string.Format("Configuration file not found at \"{0}\"", possibleLocation));
                }
            }
            throw new FileNotFoundException(string.Format("Configuration file could not be found"));
        }

        /// <summary>
        /// Generating all possible configuration locations
        /// </summary>
        private IEnumerable<string> GetPossibleConfigurationLocations()
        {
            var possibleLocations = new[] { "Configuration.xml" };
            foreach (var directory in GetDirectories())
            {
                Logger.Trace(LogNumbers.GeneratingPath, string.Format("Investigating directory \"{0}\"", directory));
                foreach (var possibleLocation in possibleLocations)
                {
                    var location = Path.Combine(directory, possibleLocation);
                    yield return location;
                }
            }
        }

        /// <summary>
        /// Gets the possible directories, where the file could be located
        /// </summary>
        private IEnumerable<string> GetDirectories()
        {
            yield return Directory.GetCurrentDirectory(); 
            yield return AppDomain.CurrentDomain.BaseDirectory;   
        }
    }
}
