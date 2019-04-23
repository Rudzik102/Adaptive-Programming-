using ProjectTPA.ViewModel;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using System.Configuration;
using System.IO;
using System.Reflection;
using System.Windows;

namespace ProjectTPA.View
{
    /// <summary>
    /// Logika interakcji dla klasy App.xaml
    /// </summary>
    public partial class App : Application
    {
        /*
        private void On_Startup(object sender, StartupEventArgs e)
        {
            MasterViewModel viewModel = new MasterViewModel();
            Compose(viewModel);
        }

        public void Compose(object obj)
        {
            NameValueCollection plugins = (NameValueCollection)ConfigurationManager.GetSection("plugins");
            string[] pluginsCatalogs = plugins.AllKeys;
            List<DirectoryCatalog> directoryCatalogs = new List<DirectoryCatalog>();
            foreach (string pluginsCatalog in pluginsCatalogs)
            {
                if (Directory.Exists(pluginsCatalog))
                    directoryCatalogs.Add(new DirectoryCatalog(pluginsCatalog));
            }

            AggregateCatalog catalog = new AggregateCatalog(directoryCatalogs);
            CompositionContainer container = new CompositionContainer(catalog);

            try
            {
                container.ComposeParts(obj);
            }
            catch (CompositionException compositionException)
            {
                Console.WriteLine(compositionException.ToString());
            }
            catch (Exception exception) when (exception is ReflectionTypeLoadException)
            {
                ReflectionTypeLoadException typeLoadException = (ReflectionTypeLoadException)exception;
                Exception[] loaderExceptions = typeLoadException.LoaderExceptions;

                throw;
            }
        }
        */
    }
}
