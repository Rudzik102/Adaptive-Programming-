namespace Composition
{
    public class AppSettings
    {
        private const string DEFAULT_TRACER = "ConsoleTracer";
        private const string DEFAULT_SERIALIZER = "XmlSerializer";
        private static AppSettings settings;

        public string Tracer { get; private set; }
        public string Serializer { get; private set; }
        public string ConsoleTracerDll { get; private set; }
        public string DatabaseTracerDll { get; private set; }
        public string XmlSerializationDll { get; private set; }
        public string DatabaseSerializationDll { get; private set; }
        public string DatabaseConnectionString { get; private set; }

        private AppSettings()
        {
            Tracer = Properties.Settings.Default.ITraceSource ?? DEFAULT_TRACER;
            Serializer = Properties.Settings.Default.ISerializer ?? DEFAULT_SERIALIZER;
            ConsoleTracerDll = Properties.Settings.Default.ConsoleTracerDll;
            DatabaseTracerDll = Properties.Settings.Default.DatabaseTracerDll;
            XmlSerializationDll = Properties.Settings.Default.XmlSerializerDll;
            DatabaseSerializationDll = Properties.Settings.Default.DatabaseSerializerDll;
            DatabaseConnectionString = Properties.Settings.Default.DatabaseConnectionString;
        }

        public static AppSettings GetInstance()
        {
            if (settings == null)
            {
                settings = new AppSettings();
            }

            return settings;
        }
    }
}
