using System.Collections.Generic;

namespace Mission_PrincessRescue
{
    public class TmxProperties
    {
        private Dictionary<string, object> props;

        public TmxProperties()
        {
            props = new Dictionary<string, object>();
        }

        public void Set<Type>(string key, Type property)
        {
            if (Has(key))
                return;

            props.Add(key, property);
        }

        public Type Get<Type>(string key)
        {
            if (Has(key))
                return (Type)props[key];

            return default;
        }

        public bool Has(string name)
        {
            return props.ContainsKey(name);
        }

    }
}