using Interfaces;
using System;

namespace ProjectTPA.ConsoleApp
{
    public class CLFileChooser : IFileChooser
    {
        public string GetPathToRead(string filter)
        {
            Console.WriteLine("Type file path to read:");
            return Console.ReadLine();
        }

        public string GetPathToSave(string filter)
        {
            Console.WriteLine("Type file path to save:");
            return Console.ReadLine();
        }
    }
}
