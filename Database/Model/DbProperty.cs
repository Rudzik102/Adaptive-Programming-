using ProjectTPA.DatabaseHelper;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectTPA.Database
{
    public class DbProperty
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public DbType Type { get; set; }

        public virtual ICollection<DbType> TypeProperties { get; set; }

        public DbProperty()
        {
            TypeProperties = new HashSet<DbType>();
        }

        public DbProperty(PropertyBase propertyBase)
        {
            Name = propertyBase.Name ?? "default";
            Type = DbType.GetOrAdd(propertyBase.TypeMetadata);
        }
    }
}
