using Composition;
using ProjectTPA.DatabaseHelper;
using ProjectTPA.Interfaces;
using ProjectTPA.Model;
using System;
using System.ComponentModel.Composition;
using System.Data.Entity;
using System.Linq;

namespace ProjectTPA.Database.Serialize
{
    [Export(typeof(ISerializer))]
    public class DatabaseSerializer : ISerializer
    {
        private DBaseContext context;

        [ImportingConstructor]
        public DatabaseSerializer()
        {
            this.context = new DBaseContext(AppSettings.GetInstance().DatabaseConnectionString);
        }

        public AssemblyMetadata Deserialize(string path)
        {
            //using (DBaseContext context = new DBaseContext(AppSettings.GetInstance().DatabaseConnectionString))
            {
                context.Assembly.Load();
                context.Namespace.Load();
                context.Type.Load();
                context.Method.Load();
                context.Property.Load();
                context.Parameter.Load();
                AssemblyBase  assemblyBase = DatabaseMapper.Convert(context.Assembly.OrderByDescending(c => c.Id).FirstOrDefault() ?? throw new Exception());
                if (assemblyBase == null)
                {
                    throw new Exception();
                }

                return new AssemblyMetadata(assemblyBase);
            }
        }

        public void Serialize(AssemblyMetadata _object, string path)
        {
            //using (DBaseContext context = new DBaseContext(AppSettings.GetInstance().DatabaseConnectionString))
            {
                System.Data.Entity.Database.SetInitializer(new DropCreateDatabaseAlways<DBaseContext>());
                AssemblyBase assemblyBase = Mapper.Convert(_object);
                DbAssembly dbAssembly = new DbAssembly(assemblyBase);

                context.Assembly.Add(dbAssembly);
                context.SaveChanges();
            }
        }
    }
}
