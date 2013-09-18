using System;
using System.Configuration;

namespace NancyBoilerplate.Core
{
    public class Config
    {
        /// <summary>
        /// Returns the connection string for the environment based on the 
        /// environment machine name.
        /// </summary>
        public static string ConnectionString
        {
            get { return ConfigurationManager.ConnectionStrings[Environment.MachineName].ConnectionString; }
        }
    }
}