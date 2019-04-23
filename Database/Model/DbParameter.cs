using ProjectTPA.DatabaseHelper;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectTPA.Database
{
    public class DbParameter
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public DbType Type { get; set; }

        public virtual ICollection<DbMethod> MethodParameters { get; set; }
        public virtual ICollection<DbType> TypeFields { get; set; }

        public DbParameter()
        {
            MethodParameters = new HashSet<DbMethod>();
            TypeFields = new HashSet<DbType>();
        }

        public DbParameter(ParameterBase parameterBase)
        {
            Name = parameterBase.Name;
            Type = DbType.GetOrAdd(parameterBase.TypeMetadata);
        }
    }
}
