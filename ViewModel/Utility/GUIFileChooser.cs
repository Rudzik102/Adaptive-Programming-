using Interfaces;
using System.Windows.Forms;

namespace ProjectTPA.ViewModel
{
    public class GUIFileChooser : IFileChooser
    {
        public string GetPathToRead(string filter)
        {
            OpenFileDialog test = new OpenFileDialog()
            {
                Filter = filter
            };

            test.ShowDialog();

            if (test.FileName != null)
            {
                return test.FileName;
            }

            return null;
        }

        public string GetPathToSave(string filter)
        {
            SaveFileDialog dialog = new SaveFileDialog()
            {
                Filter = filter
            };

            dialog.ShowDialog();

            if (dialog.FileName != null)
            {
                return dialog.FileName;
            }

            return null;
        }
    }
}
