using System;
using System.Collections.Generic;
using System.IO;
using FluentAssertions;
using Newtonsoft.Json;

namespace SampleTestFramework.Drivers
{

    public class EnvironmentConfig
    {
        private static readonly string DefaultEnvironment = @"dev";

        private static readonly string DefaultConfigRoot = @".\";
        
        private static Dictionary<string,string> config;

        static EnvironmentConfig()
        {
            var configRoot = Environment.GetEnvironmentVariable("QA_CONFIG_ROOT");
            if (configRoot == null) {
                configRoot = DefaultConfigRoot;
            }

            var envName = Environment.GetEnvironmentVariable("QA_ENV");
            if (envName == null) {
                envName = DefaultEnvironment;
            }

            var envFilePath = configRoot+envName+".json";
         
            File.Exists(envFilePath).Should().BeTrue("beacuse otherwise there is no environment configuration to setup run (envFilePath="+envFilePath+")");
            var envConfigRaw = (File.ReadAllText(envFilePath));
            config = JsonConvert.DeserializeObject<Dictionary<string,string>>(envConfigRaw);
        }

        public static string ItemOrDefault(string itemName, string defaultValue)
        {
            string itemValue = defaultValue;
            if (config.ContainsKey(itemName))
                itemValue = config[itemName];
            return itemValue;
        }

       public static string EnvVarOrDefault(string envVarName, string defaultValue)
        {
            string envVarValue = Environment.GetEnvironmentVariable(envVarName);
            if (envVarValue == null)
                envVarValue = defaultValue;
            return envVarValue;
        }

        public static string EnvVar(string envVarName)
        {
            string envVarValue = Environment.GetEnvironmentVariable(envVarName);
            envVarValue.Should().NotBeNull();
            return envVarValue;
        }

        public static string Item(string itemName)
        {
            string itemValue = null;
            if (config.ContainsKey(itemName))
                itemValue = config[itemName];
            itemValue.Should().NotBeNull();
            return itemValue;
        }

    }
}