using System;
using System.Collections.Generic;
using System.IO;

namespace NCToolsKidForJava.Utils
{
    public partial class ConfigUtil
    {
        public ConfigUtil()
        {
        }

        public Dictionary<String, String> readConfig()
        {
            Dictionary<String, String> config = new Dictionary<String, String>();
            String configFilePath = System.IO.Directory.GetCurrentDirectory() + "\\config.data";
            FileInfo conFile = new FileInfo(configFilePath);
            if (!conFile.Exists)
            {
                StreamWriter sw = new StreamWriter(configFilePath);
                sw.WriteLine("");
                sw.Close();

            }
            else
            {
                StreamReader sr = new StreamReader(configFilePath);
                String line;
                while ((line = sr.ReadLine()) != null)
                {
                    int splitIndex = line.Trim().IndexOf(" ");
                    if (splitIndex >= 0)
                    {
                        string key = line.Substring(0, splitIndex);
                        string value = line.Substring(splitIndex + 1);
                        config[key] = value;
                    }
                }
                sr.Close();
            }



            return config;
        }

        public void saveConfig(Dictionary<String, String> newConfig)
        {
            Dictionary<String, String> oldConfig = readConfig();
            foreach (string key in newConfig.Keys)
            {
                oldConfig[key] = newConfig[key];
            }
            String configFilePath = System.IO.Directory.GetCurrentDirectory() + "\\config.data";
            StreamWriter sw = new StreamWriter(configFilePath);
            foreach (string key in oldConfig.Keys)
            {
                sw.WriteLine(key+" "+oldConfig[key]);

            }
            sw.Close();

        }
        public void saveConfig(String key1,string value)
        {
            Dictionary<String, String> oldConfig = readConfig();
            oldConfig[key1] = value;
            String configFilePath = System.IO.Directory.GetCurrentDirectory() + "\\config.data";
            StreamWriter sw = new StreamWriter(configFilePath);
            foreach (string key in oldConfig.Keys)
            {
                sw.WriteLine(key + " " + oldConfig[key]);

            }
            sw.Close();

        }
    }
}
