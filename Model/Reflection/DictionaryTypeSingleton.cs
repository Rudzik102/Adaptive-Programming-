using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectTPA.Model
{
    public sealed class DictionaryTypeSingleton
    {

        public static DictionaryTypeSingleton Instance { get; } = new DictionaryTypeSingleton();

        private readonly Dictionary<string, TypeMetadata> _data = new Dictionary<string, TypeMetadata>();
        private DictionaryTypeSingleton()
        {
        }

        public void Add(string name, TypeMetadata type)
        {
            _data.Add(name, type);
        }

        public bool ContainsKey(string name)
        {
            return _data.ContainsKey(name);
        }

        public TypeMetadata Get(string key)
        {
            _data.TryGetValue(key, out TypeMetadata value);
            return value;
        }
    }
}
