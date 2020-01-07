using System.Collections.Generic;
using System.Linq;

namespace UtilLib.FolderTree
{
    /// <summary>
    /// A class to hold a full copy of the enumerable that can
    /// result from a Linq grouping operation on FolderTreeItem objects
    /// </summary>
    /// <typeparam name="T">The type of the key of the group of FolderTreeItem objects</typeparam>
    public sealed class FolderTreeItemsGrouping<T> 
    {
        public static Dictionary<T, FolderTreeItemsGrouping<T>> CreateDictionary(IEnumerable<IGrouping<T, FolderTreeItem>> itemsGroupsList)
        {
            return 
                itemsGroupsList
                .ToDictionary(
                    itemsGroup => itemsGroup.Key, 
                    itemsGroup => new FolderTreeItemsGrouping<T>(itemsGroup)
                    );
        }

        public static Dictionary<T, FolderTreeItemsGrouping<T>> CreateDictionary(IEnumerable<IGrouping<T, FolderItem>> itemsGroupsList)
        {
            return 
                itemsGroupsList
                .ToDictionary(
                    itemsGroup => itemsGroup.Key, 
                    itemsGroup => new FolderTreeItemsGrouping<T>(itemsGroup)
                    );
        }

        public static Dictionary<T, FolderTreeItemsGrouping<T>> CreateDictionary(IEnumerable<IGrouping<T, FileItem>> itemsGroupsList)
        {
            return 
                itemsGroupsList
                .ToDictionary(
                    itemsGroup => itemsGroup.Key, 
                    itemsGroup => new FolderTreeItemsGrouping<T>(itemsGroup)
                    );
        }


        public static FolderTreeItemsGrouping<T> CreateCopy(FolderTreeItemsGrouping<T> itemsGroup)
        {
            return new FolderTreeItemsGrouping<T>(itemsGroup);
        }

        public static FolderTreeItemsGrouping<T> Create(IGrouping<T, FolderTreeItem> itemsGroup)
        {
            return new FolderTreeItemsGrouping<T>(itemsGroup);
        }

        public static FolderTreeItemsGrouping<T> Create(IGrouping<T, FolderItem> itemsGroup)
        {
            return new FolderTreeItemsGrouping<T>(itemsGroup);
        }

        public static FolderTreeItemsGrouping<T> Create(IGrouping<T, FileItem> itemsGroup)
        {
            return new FolderTreeItemsGrouping<T>(itemsGroup);
        }


        public T Key { get; private set; }

        private Dictionary<string, FileItem> _files;

        private Dictionary<string, FolderItem> _folders;

        private Dictionary<string, FolderItem> _parentFolders;


        public int FilesCount { get { return _files == null ? 0 : _files.Count; } }

        public int FoldersCount { get { return _folders == null ? 0 : _folders.Count; } }

        public int ParentFoldersCount { get { return _parentFolders == null ? 0 : _parentFolders.Count; } }

        public int ItemsCount { get { return FoldersCount + FilesCount; } }


        private FolderTreeItemsGrouping(FolderTreeItemsGrouping<T> itemsGroup)
        {
            Key = itemsGroup.Key;

            if (itemsGroup._files != null)
            {
                _files = new Dictionary<string, FileItem>();

                foreach (var pair in itemsGroup._files)
                    _files.Add(pair.Key, pair.Value);
            }

            if (itemsGroup._folders != null)
            {
                _folders = new Dictionary<string, FolderItem>();

                foreach (var pair in itemsGroup._folders)
                    _folders.Add(pair.Key, pair.Value);
            }

            if (itemsGroup._parentFolders == null) 
                return;

            _parentFolders = new Dictionary<string, FolderItem>();

            foreach (var pair in itemsGroup._parentFolders)
                _parentFolders.Add(pair.Key, pair.Value);
        }

        private FolderTreeItemsGrouping(IGrouping<T, FolderTreeItem> itemsGroup)
        {
            Key = itemsGroup.Key;

            foreach (var item in itemsGroup)
                AddItem(item);
        }

        private FolderTreeItemsGrouping(IGrouping<T, FolderItem> itemsGroup)
        {
            Key = itemsGroup.Key;

            foreach (var item in itemsGroup)
                AddFolder(item);
        }

        private FolderTreeItemsGrouping(IGrouping<T, FileItem> itemsGroup)
        {
            Key = itemsGroup.Key;

            foreach (var item in itemsGroup)
                AddFile(item);
        }


        private void AddItem(FolderTreeItem item)
        {
            if (item.IsFile)
            {
                if (_files == null)
                    _files = new Dictionary<string, FileItem>();

                _files.Add(item.ItemPath.ToLower(), (FileItem)item);
            }
            else
            {
                if (_folders == null)
                    _folders = new Dictionary<string, FolderItem>();

                _folders.Add(item.ItemPath.ToLower(), (FolderItem)item);
            }

            AddParentFolder(item.ParentFolder);
        }

        private void AddFile(FileItem item)
        {
            if (_files == null)
                _files = new Dictionary<string, FileItem>();

            _files.Add(item.ItemPath.ToLower(), item);

            AddParentFolder(item.ParentFolder);
        }

        private void AddFolder(FolderItem item)
        {
            if (_folders == null)
                _folders = new Dictionary<string, FolderItem>();

            _folders.Add(item.ItemPath.ToLower(), item);

            AddParentFolder(item.ParentFolder);
        }

        private void AddParentFolder(FolderItem parentFolder)
        {
            if (FolderItem.IsProper(parentFolder) == false)
                return;

            if (_parentFolders == null)
            {
                _parentFolders = 
                    new Dictionary<string, FolderItem>
                    {
                        {parentFolder.ItemPath.ToLower(), parentFolder}
                    };


                return;
            }

            if (_parentFolders.ContainsKey(parentFolder.ItemPath.ToLower()) == false)
                _parentFolders.Add(parentFolder.ItemPath.ToLower(), parentFolder);
        }


        public FolderTreeItemsGrouping<T> MergeItems<TU>(FolderTreeItemsGrouping<TU> grp)
        {
            if (grp._files != null)
                foreach (var pair in grp._files.Where(pair => _files.ContainsKey(pair.Key) == false))
                    AddFile(pair.Value);

            if (grp._folders == null) 
                return this;

            foreach (var pair in grp._folders.Where(pair => _folders.ContainsKey(pair.Key) == false))
                AddFolder(pair.Value);

            return this;
        }


        public IEnumerable<FolderItem> ParentFolders()
        {
            if (_parentFolders == null) 
                yield break;

            foreach (var pair in _parentFolders)
                yield return pair.Value;
        }

        public IEnumerable<FolderItem> Folders()
        {
            if (_folders == null) 
                yield break;

            foreach (var pair in _folders)
                yield return pair.Value;
        }

        public IEnumerable<FileItem> Files()
        {
            if (_files == null) 
                yield break;

            foreach (var pair in _files)
                yield return pair.Value;
        }

        public IEnumerable<FolderTreeItem> Items()
        {
            if (_folders != null)
                foreach (var pair in _folders)
                    yield return pair.Value;

            if (_files == null) 
                yield break;

            foreach (var pair in _files)
                yield return pair.Value;
        }


        public IEnumerable<string> ParentFolderPaths()
        {
            if (_parentFolders == null) 
                yield break;

            foreach (var pair in _parentFolders)
                yield return pair.Key;
        }

        public IEnumerable<string> FolderPaths()
        {
            if (_folders == null) 
                yield break;

            foreach (var pair in _folders)
                yield return pair.Key;
        }

        public IEnumerable<string> FilePaths()
        {
            if (_files == null) 
                yield break;

            foreach (var pair in _files)
                yield return pair.Key;
        }

        public IEnumerable<string> ItemPaths()
        {
            if (_folders != null)
                foreach (var pair in _folders)
                    yield return pair.Key;

            if (_files == null) 
                yield break;

            foreach (var pair in _files)
                yield return pair.Key;
        }
    }
}
