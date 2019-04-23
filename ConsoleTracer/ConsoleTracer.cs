using ProjectTPA.Interfaces;
using System;
using System.ComponentModel.Composition;

namespace ProjectTPA.Utility
{
    [Export(typeof(ITraceSource))]
    public class ConsoleTracer : ITraceSource
    {
        public void Trace(string source)
        {
            string date = "[" + DateTime.Now.ToString(@"MM\/dd\/yyyy h\:mm tt") + "] ";
            Console.WriteLine(date + source);
        }
    }
}
