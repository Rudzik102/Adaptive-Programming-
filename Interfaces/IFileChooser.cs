namespace Interfaces
{
    public interface IFileChooser
    {
        string GetPathToRead(string filter);
        string GetPathToSave(string filter);
    }
}
