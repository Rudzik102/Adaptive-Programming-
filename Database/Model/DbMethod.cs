using ProjectTPA.DatabaseHelper;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectTPA.Database
{
    public class DbMethod
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public bool Extension { get; set; }
        public DbType ReturnType { get; set; }
        public ICollection<DbType> GenericArguments { get; set; }
        public ICollection<DbParameter> Parameters { get; set; }
        public AccessLevelEnum AccessLevel { get; set; }
        public AbstractEnum Abstract { get; set; }
        public StaticEnum Static { get; set; }
        public VirtualEnum Virtual { get; set; }

        public virtual ICollection<DbType> TypeConstructors { get; set; }
        public virtual ICollection<DbType> TypeMethods { get; set; }

        public DbMethod()
        {
            GenericArguments = new List<DbType>();
            Parameters = new List<DbParameter>();
            TypeConstructors = new HashSet<DbType>();
            TypeMethods = new HashSet<DbType>();
        }

        public DbMethod(MethodBase methodBase)
        {
            Name = methodBase.Name;
            Extension = methodBase.Extension;
            ReturnType = DbType.GetOrAdd(methodBase.ReturnType);
            GenericArguments = methodBase.GenericArguments?.Select(x => DbType.GetOrAdd(x)).ToList();
            Parameters = methodBase.Parameters?.Select(x => new DbParameter(x)).ToList();
            AccessLevel = methodBase.Modifiers.Item1;
            Abstract = methodBase.Modifiers.Item2;
            Static = methodBase.Modifiers.Item3;
            Virtual = methodBase.Modifiers.Item4;
        }
    }
}
