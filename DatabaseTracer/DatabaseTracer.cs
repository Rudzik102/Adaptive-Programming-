using Composition;
using ProjectTPA.Database;
using ProjectTPA.Interfaces;
using System.ComponentModel.Composition;

namespace DatabaseLogger
{
    [Export(typeof(ITraceSource))]
    public class DatabaseTracer : ITraceSource
    {
        public void Trace(string source)
        {
            using (DBaseContext context = new DBaseContext(AppSettings.GetInstance().DatabaseConnectionString))
            {
                context.Log.Add(new DbLog(source));
                context.SaveChanges();
            }
        }
    }
}
