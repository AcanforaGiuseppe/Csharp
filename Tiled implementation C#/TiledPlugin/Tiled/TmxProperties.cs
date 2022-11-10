using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TiledPlugin
{
    public class TmxProperties
    {
        private Dictionary<string, object> props;

        public TmxProperties()
        {
            props = new Dictionary<string, object>();
        }

        public void SetString(string name, string value)
        {
            props[name] = value;
        }

        public void SetBool(string name, bool value)
        {
            props[name] = value;
        }

        public string GetString(string name)
        {
            if (!Has(name))
                return null;

            return (string)props[name];
        }

        public bool GetBool(string name)
        {
            return (bool)props[name];
        }

        public bool Has(string name)
        {
            return props.ContainsKey(name);
        }

    }
}