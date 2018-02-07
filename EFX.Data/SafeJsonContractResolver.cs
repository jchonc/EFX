using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EFX.Data
{
    public class SafeJsonContractResolver: DefaultContractResolver
    {
        private readonly string[] _forbiddenAttrNames;

        public SafeJsonContractResolver()
        {
            _forbiddenAttrNames = null;
        }

        public SafeJsonContractResolver(string[] forbiddenAttrNames)
        {
            _forbiddenAttrNames = forbiddenAttrNames;
        }

        protected override IList<JsonProperty> CreateProperties(Type type, MemberSerialization memberSerialization)
        {
            IList<JsonProperty> properties = base.CreateProperties(type, memberSerialization);
            if (_forbiddenAttrNames != null && _forbiddenAttrNames.Count() > 0)
            {
                properties = properties.Where(p => !_forbiddenAttrNames.Contains(p.PropertyName)).ToList();
            }
            return properties;
        }
    }
}
