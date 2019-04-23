using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectTPA.DatabaseHelper
{
    public class MethodBase
    {
        public string Name;
        public IEnumerable<TypeBase> GenericArguments;
        public Tuple<AccessLevelEnum, AbstractEnum, StaticEnum, VirtualEnum> Modifiers;
        public TypeBase ReturnType;
        public bool Extension;
        public IEnumerable<ParameterBase> Parameters;
    }
}
