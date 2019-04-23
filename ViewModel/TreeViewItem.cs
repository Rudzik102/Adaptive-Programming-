using System.Collections.ObjectModel;

namespace ProjectTPA.ViewModel
{
    public class TreeViewItem
    {
        public ObservableCollection<TreeViewItem> Children { get; set; }

        public string Name { get; set; }

        public TreeViewItem()
        {
            Children = new ObservableCollection<TreeViewItem>();
            Name = "Root";
        }
    }
}
