using Composition;
using ProjectTPA.Interfaces;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using System.Linq;

namespace ProjektTPA.Composition
{
    public class AppComposer
    {
        private AppSettings settings;

        [ImportMany(typeof(ITraceSource))]
        private IEnumerable<ITraceSource> Tracers { get; set; }

        [ImportMany(typeof(ISerializer))]
        private IEnumerable<ISerializer> Serializers { get; set; }

        public AppComposer()
        {
            settings = AppSettings.GetInstance();

            var databaseTracerDirectory = new DirectoryCatalog(settings.DatabaseTracerDll);
            var consoleTracerDirectory = new DirectoryCatalog(settings.ConsoleTracerDll);
            var xmlSerializationDirectory = new DirectoryCatalog(settings.XmlSerializationDll);
            var databaseSerializationDirectory = new DirectoryCatalog(settings.DatabaseSerializationDll);

            var catalog = new AggregateCatalog();
            catalog.Catalogs.Add(databaseTracerDirectory);
            catalog.Catalogs.Add(consoleTracerDirectory);
            catalog.Catalogs.Add(xmlSerializationDirectory);
            catalog.Catalogs.Add(databaseSerializationDirectory);

            var container = new CompositionContainer(catalog);
            container.ComposeParts(this);
        }

        public ITraceSource GetTracer()
        {
            string setup = settings.Tracer;
            return Tracers.ToList().Find(tracer => tracer.GetType().Name == setup);
        }

        public ISerializer GetSerializer()
        {
            string setup = settings.Serializer;
            return Serializers.ToList().Find(serializer => serializer.GetType().Name == setup);
        }
    }
}
