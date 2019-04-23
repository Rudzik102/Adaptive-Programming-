using System;
using System.IO;
using ProjectTPA.ViewModel;
using ProjectTPA.Database;

namespace ProjectTPA.ConsoleApp
{
    class Program
    {
        public static MasterViewModel ViewModel { get; set; }

        static void Main(string[] args)
        {
            ViewModel = new MasterViewModel()
            {
                FileChoose = new CLFileChooser()
            };
            MainMenu("");
        }

        public static void MainMenu(string message)
        {
            Console.Clear();
            Console.WriteLine(message);
            Console.WriteLine("Avaible options: B\b\browse for choosing the dll, L\\l\\load for presenting the data, S\\s\\Save for serialization, A\\a\\adapt for showind the serialized files" +
                              "To exit the program type E/e/exit and confirm with 'Enter'");

            string choose = Console.ReadLine();
            switch (choose)
            {
                case "B":
                case "b":
                case "Browse":
                    {
                        ViewModel.BrowsePath();
                        MainMenu("Returning to main menu...");
                        break;
                    }

                case "L":
                case "l":
                case "Load":
                    {
                        ViewModel.LoadFile();
                        Expand();
                        MainMenu("Returning to main menu...");
                        break;
                    }
                case "S":
                case "s":
                case "Save":
                    {
                        ViewModel.SaveFile();
                        MainMenu("Returning to main menu...");
                        break;
                    }
                case "A":
                case "a":
                case "Adapt":
                    {
                        ViewModel.Adapt();
                        Expand();
                        MainMenu("Returning to main menu...");
                        break;
                    }
                case "E":
                case "e":
                case "Exit":
                    {
                        Environment.Exit(0);
                        break;
                    }
                default:
                    {
                        MainMenu("Wrong Option!\n");
                        break;
                    }
            }

        }

        public static void Expand()
        {
            Console.WriteLine("Wyświetlam namespace");
            foreach(TreeViewItem item in ViewModel.Tree)
            {
                Console.WriteLine(item.Name);
                foreach(TreeViewItem items in item.Children)
                {
                    Console.WriteLine(Indent(2)+items.Name);
                    foreach(TreeViewItem itemss in items.Children)
                    {
                        Console.WriteLine(Indent(4)+itemss.Name);
                    }
                }
            }

            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();

            MainMenu("Returning to main menu...");
        }

        public static string Indent(int count)
        {
            return "".PadLeft(count);
        }
    }
}
