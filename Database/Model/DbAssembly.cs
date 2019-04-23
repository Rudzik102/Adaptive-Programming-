using ProjectTPA.DatabaseHelper;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectTPA.Database
{
    public class DbAssembly
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<DbNamespace> Namespaces { get; set; }

        public DbAssembly() { }

        public DbAssembly(AssemblyBase assemblyBase)
        {
            Name = assemblyBase.Name;
            Namespaces = assemblyBase.Namespaces?.Select(x => new DbNamespace(x)).ToList();
        }
    }
}
