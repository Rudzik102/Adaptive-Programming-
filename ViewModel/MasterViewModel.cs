using ProjectTPA.Utility;
using ProjectTPA.ViewModel.Module;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Input;
using ProjectTPA.Model;
using ProjectTPA.Interfaces;
using System.ComponentModel.Composition;
using ProjektTPA.Composition;
using Interfaces;

namespace ProjectTPA.ViewModel
{
    [Export]
    public class MasterViewModel : BaseViewModel
    {
        // Private Properties
        private ObservableCollection<TreeViewItem> _tree { get; set; } = new ObservableCollection<TreeViewItem>();
        private string _filepath { get; set; }

        public IFileChooser FileChoose {get;set;}
        public IPathLoader PathLoader { get; set; }
        public ISerializer Serializer { get; set; }
        public ITraceSource TraceSource { get; set; }

        public Reflector reflector { get; set; } = new Reflector();
        
        //View Properties
        public ObservableCollection<TreeViewItem> Tree
        {
            get { return _tree; }
        }

        public string FilePath
        {
            get { return _filepath; }
            set { _filepath = value; NotifyPropertyChanged("FilePath"); }
        }

        // Commands
        public ICommand BrowseCommand { get; private set; }
        public ICommand LoadCommand { get; private set; }
        public ICommand SaveCommand { get; private set; }
        public ICommand AdaptCommand { get; private set; }

        // Constructor
        public MasterViewModel()
        {
            Compose();
            PathLoader = new XamlFileChooser();
            FileChoose = new GUIFileChooser();
            
            // Init commands
            BrowseCommand = new DelegateCommand(BrowsePath);
            LoadCommand = new DelegateCommand(LoadFile);
            SaveCommand = new DelegateCommand(SaveFile);
            AdaptCommand = new DelegateCommand(Adapt);
        }

        private void Compose()
        {
            AppComposer applicationComposer = new AppComposer();
            TraceSource = applicationComposer.GetTracer();
            Serializer = applicationComposer.GetSerializer();
        }

        // Methods
        public void BrowsePath()
        {
            FilePath = FileChoose.GetPathToRead("Dynamic Library File(*.dll)|*.dll");
            TraceSource.Trace("Opened file picking window");
        }

        public void LoadFile()
        {
            _tree = new ObservableCollection<TreeViewItem>();

            //if (FilePath.Contains(".dll"))
            {
                //DatabaseSerializer xmlSerialization = new DatabaseSerializer();
                //reflector.AssemblyModel = xmlSerialization.Deserialize("");
                reflector.AssemblyModel = new AssemblyMetadata(PathLoader.Load(FilePath));
                LoadTree();
            }
        }

        public void LoadTree()
        {
            _tree = new ObservableCollection<TreeViewItem>();

            List<string> parentType = new List<string>();
            List<NamespaceMetadata> testLibraryName = reflector.AssemblyModel.NamespaceModels;
            List<TypeMetadata> testLibraryTypes = new List<TypeMetadata>();
            List<TreeViewItem> items = new List<TreeViewItem>();

            foreach (NamespaceMetadata oNamespace in testLibraryName)
            {
                TreeViewItem name = new TreeViewItem()
                {
                    Name = oNamespace.Name
                };

                testLibraryTypes = oNamespace.Types;

                foreach (TypeMetadata oType in testLibraryTypes)
                {
                    TreeViewItem imd = new TreeViewItem()
                    {
                        Name = oType.Name
                    };

                    TreeViewItem test = new TreeViewItem()
                    {
                        Name = oType.Name,
                        Children = new ObservableCollection<TreeViewItem>(ViewType.GetChildren(oType, imd, parentType))
                    };

                    items.Add(test);
                    parentType.Clear();
                }

                name.Children = new ObservableCollection<TreeViewItem>(items);
                _tree.Add(name);
            }

            NotifyPropertyChanged("Tree");
            TraceSource.Trace("File");
        }

        public void SaveFile()
        {
            if (Serializer is XmlSerializer.XmlSerializer)
            {
                string path = FileChoose.GetPathToSave("XML File(*.xml) | *.xml");
                Serializer.Serialize(reflector.AssemblyModel, path);
                TraceSource.Trace("Saved XML file");
            }
            else
            {
                Serializer.Serialize(reflector.AssemblyModel, "");
            }
        }

        public void Adapt()
        {
            if (Serializer is XmlSerializer.XmlSerializer)
            {
                FilePath = FileChoose.GetPathToRead("XML File(*.xml) | *.xml");
                reflector.AssemblyModel = Serializer.Deserialize(FilePath);
                LoadTree();
                TraceSource.Trace("Saved XML file");
            }
            else
            {
                reflector.AssemblyModel = Serializer.Deserialize("");
                LoadTree();
            }
        }
    }
}
