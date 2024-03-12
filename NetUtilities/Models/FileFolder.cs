using System.Collections.Generic;

namespace NetUtilities.Models
{
    public class FileFolder
    {
        public string Name { get; private set; }
        public string Path { get; private set; }
        public bool IsFolder { get; private set; }
        public List<FileFolder> Children { get; private set; }

        public FileFolder(string name, string path, bool isFolder, List<FileFolder> folders = default)
        {
            Name = name;
            Path = path;
            IsFolder = isFolder;
            Children = folders ?? new List<FileFolder>();
        }
    }
}
