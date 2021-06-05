using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using System.Windows;

namespace Ringen.Resources
{
    public class RingenResourceManager
    {
        public static RingenResourceManager _instance;

        private Dictionary<string, object> resources = new Dictionary<string, object>();


        public static RingenResourceManager Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new RingenResourceManager();
                return _instance;
            }
        }

        public static void AddResource(string baseName, Assembly assembly)
        {
            ResourceDictionary resourceDictionary = new ResourceDictionary();
            Uri source = new Uri("/" + assembly.GetName().Name + ";component/" + baseName, UriKind.RelativeOrAbsolute);
            resourceDictionary.Source = source;

            foreach(DictionaryEntry entry in resourceDictionary)
            {
                Instance.resources.Add(entry.Key.ToString(), entry.Value);
            }
        }

        public static T GetResource<T>(string key)
        {
            if (Instance.resources.ContainsKey(key))
                return (T)Instance.resources[key];
            else
                throw new Exception("Key not found");
        }
    }
}
