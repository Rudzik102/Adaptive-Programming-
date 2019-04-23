using ProjectTPA.DatabaseHelper;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectTPA.Database
{
    public class DbNamespace
    {
        [Key]
        public int Id { set; get; }
        public string Name { get; set; }
        public ICollection<DbType> Types { get; set; }

        public DbNamespace() { }

        public DbNamespace(NamespaceBase namespaceBase)
        {
            Name = namespaceBase.Name;
            Types = namespaceBase.Types?.Select(x => DbType.GetOrAdd(x)).ToList();
        }
    }
}
