using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace UtilLib.FolderTree
{
    public sealed class FolderItem : FolderTreeItem
    {
        public static bool IsProper(FolderItem folder)
        {
            return 
                ReferenceEquals(folder, null) == false 
                && folder.IsMultiRootParentFolder == false;
        }

        public static FolderItem CreateRoot(string folderPath)
        {
            return new FolderItem(folderPath);
        }

        public static FolderItem CreateMultiRoot(IEnumerable<string> itemsPaths)
        {
            //Create a list of accepted files and folders
            var acceptedFolders = new List<string>();
            var acceptedFiles = new List<string>();

            var itemsPathsArray = itemsPaths.ToArray();

            //Find all existing folder paths in the input items_paths list
            var foldersPaths =
                itemsPathsArray
                .Where(Directory.Exists)
                .OrderBy(itemPath => itemPath);

            //Find all existing file paths in the input items_paths list
            var filesPaths =
                itemsPathsArray
                .Where(File.Exists)
                .OrderBy(itemPath => itemPath);

            //Reject any folder that is a sub-folder of any already accepted folder
            var foldersList =
                foldersPaths.Where(
                    folderPath =>
                        acceptedFolders.Exists(
                            acceptedFolder => 
                                folderPath
                                .ToLower()
                                .StartsWith(acceptedFolder.ToLower())
                            ) == false
                    );

            acceptedFolders.AddRange(foldersList);

            //Reject any file that is a sub-file of any already accepted folder
            //Reject any file that is already accepted
            var filePathsList =
                filesPaths
                .Where(
                    filePath =>
                        acceptedFolders
                        .Exists(
                            acceptedFolder => 
                                filePath
                                .ToLower()
                                .StartsWith(acceptedFolder.ToLower())
                            ) == false
                    )
                    .Where(
                        filePath =>
                            acceptedFiles.Exists(
                                acceptedFile =>
                                    String
                                    .Equals(filePath, acceptedFile, StringComparison.InvariantCultureIgnoreCase)
                                ) == false
                        );

            acceptedFiles.AddRange(filePathsList);

            //When there is only one accepted folder and no accepted files just create a normal root folder item
            if (acceptedFolders.Count == 1 && Directory.Exists(acceptedFolders[0]))
                return new FolderItem(acceptedFolders[0]);

            //Create a multi-rooted folder item and fill it with the accepted root folders and root files
            var multirootFolder = new FolderItem();

            if (acceptedFiles.Count > 0)
            {
                multirootFolder._files = new Dictionary<string, FileItem>();

                foreach (var filePath in acceptedFiles)
                    multirootFolder._files.Add(filePath, new FileItem(multirootFolder, filePath));
            }

            if (acceptedFolders.Count <= 0) 
                return multirootFolder;

            multirootFolder._folders = new Dictionary<string, FolderItem>();

            foreach (var folderPath in acceptedFolders)
                multirootFolder._folders.Add(folderPath, new FolderItem(multirootFolder, folderPath));

            return multirootFolder;
        }


        private readonly int _level;

        private Dictionary<string, FileItem> _files;

        private Dictionary<string, FolderItem> _folders;


        public bool HasFiles { get { return (_files != null && _files.Count > 0); } }

        public bool HasFolders { get { return (_folders != null && _folders.Count > 0); } }

        public bool HasItems { get { return HasFiles || HasFolders; } }

        public int FilesCount { get { return (_files == null ? 0 : _files.Count); } }

        public int FoldersCount { get { return (_folders == null ? 0 : _folders.Count); } }

        public int ItemsCount { get { return FilesCount + FoldersCount; } }


        public override int ItemLevel { get { return _level; } }

        /// <summary>
        /// Create a non-root folder item
        /// </summary>
        /// <param name="parentFolder"></param>
        /// <param name="folderName"></param>
        internal FolderItem(FolderItem parentFolder, string folderName)
            : base(parentFolder, folderName)
        {
            _level = parentFolder._level + 1;
        }

        /// <summary>
        /// Create a root folder item having no parent multi-root folder item
        /// </summary>
        /// <param name="folderName"></param>
        private FolderItem(string folderName)
            : base(null, folderName)
        {
            _level = 0;
        }

        /// <summary>
        /// Create a multi-root folder item
        /// </summary>
        private FolderItem()
            : base(null, String.Empty)
        {
            _level = -1;
        }


        public bool ContainsFile(string fileName)
        {
            return 
                _files != null 
                && _files.ContainsKey(fileName.ToLower());
        }

        public bool ContainsFolder(string folderName)
        {
            return 
                _folders != null 
                && _folders.ContainsKey(folderName.ToLower());
        }

        public bool ContainsItem(string itemName)
        {
            return ContainsFolder(itemName) || ContainsFile(itemName);
        }


        public bool TryGetFile(string fileName, out FileItem fileItem)
        {
            if (_files != null)
                return _files.TryGetValue(fileName.ToLower(), out fileItem);

            fileItem = null;

            return false;
        }

        public bool TryGetFolder(string folderName, out FolderItem folderItem)
        {
            if (_folders != null)
                return _folders.TryGetValue(folderName.ToLower(), out folderItem);

            folderItem = null;

            return false;
        }

        public bool TryGetItem(string itemName, out FolderTreeItem treeItem)
        {
            FolderItem folderItem;

            if (TryGetFolder(itemName, out folderItem))
            {
                treeItem = folderItem;

                return true;
            }

            FileItem fileItem;

            if (TryGetFile(itemName, out fileItem))
            {
                treeItem = fileItem;

                return true;
            }

            treeItem = null;

            return false;
        }


        public FileItem AddChildFile(string fileName)
        {
            if (IsMultiRootParentFolder)
                throw new InvalidOperationException("Cannot modify child files or folders for a multi-root parent folder");

            var fileNameKey = fileName.ToLower();

            if (_folders != null && _folders.ContainsKey(fileNameKey))
                throw new ArgumentException("A child folder with the same name already exists");

            var fileItem = new FileItem(this, fileName);

            if (_files == null)
                _files = new Dictionary<string, FileItem>();

            _files.Add(fileNameKey, fileItem);

            return fileItem;
        }

        public FolderItem AddChildFolder(string folderName)
        {
            if (IsMultiRootParentFolder)
                throw new InvalidOperationException("Cannot modify child files or folders for a multi-root parent folder");

            var folderNameKey = folderName.ToLower();

            if (_files.ContainsKey(folderNameKey))
                throw new ArgumentException("A child file with the same name already exists");

            var folderItem = new FolderItem(this, folderName);

            if (_folders == null)
                _folders = new Dictionary<string, FolderItem>();

            _folders.Add(folderNameKey, folderItem);

            return folderItem;
        }


        public void AddChildFiles(IEnumerable<string> fileNames, Func<FolderItem, string, bool> acceptFile = null)
        {
            if (IsMultiRootParentFolder)
                throw new InvalidOperationException("Cannot modify child files or folders for a multi-root parent folder");

            if (acceptFile == null)
                foreach (var fileName in fileNames)
                    AddChildFile(fileName);

            else
                foreach (var fileName in fileNames.Where(fileName => acceptFile(this, fileName)))
                    AddChildFile(fileName);
        }

        public void AddChildFolders(IEnumerable<string> folderNames, Func<FolderItem, string, bool> acceptFolder = null)
        {
            if (IsMultiRootParentFolder)
                throw new InvalidOperationException("Cannot modify child files or folders for a multi-root parent folder");

            if (acceptFolder == null)
                foreach (var folderName in folderNames)
                    AddChildFolder(folderName);

            else
                foreach (var folderName in folderNames.Where(folderName => acceptFolder(this, folderName)))
                    AddChildFolder(folderName);
        }

        public void AddChildFilesFromDisk(Func<FolderItem, string, bool> acceptFile = null)
        {
            AddChildFilesFromDisk("*.*", acceptFile);
        }

        public void AddChildFilesFromDisk(string searchPattern, Func<FolderItem, string, bool> acceptFile = null)
        {
            if (IsMultiRootParentFolder)
                throw new InvalidOperationException("Cannot modify child files or folders for a multi-root parent folder");

            _files = null;
            _folders = null;

            var fileNames = ReadChildFileNamesFromDisk(searchPattern, acceptFile);

            if (fileNames.Count <= 0) 
                return;

            _files = new Dictionary<string, FileItem>();

            foreach (var fileName in fileNames)
                _files.Add(fileName.ToLower(), new FileItem(this, fileName));
        }

        public void AddChildFoldersFromDisk(Func<FolderItem, string, bool> acceptFolder = null)
        {
            AddChildFoldersFromDisk("*.*", acceptFolder);
        }

        public void AddChildFoldersFromDisk(string searchPattern, Func<FolderItem, string, bool> acceptFolder = null)
        {
            if (IsMultiRootParentFolder)
                throw new InvalidOperationException("Cannot modify child files or folders for a multi-root parent folder");

            _files = null;
            _folders = null;

            var folderNames = ReadChildFolderNamesFromDisk(searchPattern, acceptFolder);

            if (folderNames.Count <= 0) 
                return;

            _folders = new Dictionary<string, FolderItem>();

            foreach (var folderName in folderNames)
                _folders.Add(folderName.ToLower(), new FolderItem(this, folderName));
        }

        public void AddChildItemsFromDisk()
        {
            AddChildItemsFromDisk("*.*", "*.*", null, null);
        }

        public void AddChildItemsFromDisk(string itemsSearchPattern)
        {
            AddChildItemsFromDisk(itemsSearchPattern, itemsSearchPattern, null, null);
        }

        public void AddChildItemsFromDisk(string foldersSearchPattern, string filesSearchPattern)
        {
            AddChildItemsFromDisk(foldersSearchPattern, filesSearchPattern, null, null);
        }

        public void AddChildItemsFromDisk(string itemsSearchPattern, Func<FolderItem, string, bool> acceptFolder, Func<FolderItem, string, bool> acceptFile)
        {
            AddChildItemsFromDisk(itemsSearchPattern, itemsSearchPattern, acceptFolder, acceptFile);
        }

        public void AddChildItemsFromDisk(string foldersSearchPattern, string filesSearchPattern, Func<FolderItem, string, bool> acceptItem)
        {
            AddChildItemsFromDisk(foldersSearchPattern, filesSearchPattern, acceptItem, acceptItem);
        }

        public void AddChildItemsFromDisk(string foldersSearchPattern, string filesSearchPattern, Func<FolderItem, string, bool> acceptFolder, Func<FolderItem, string, bool> acceptFile)
        {
            if (IsMultiRootParentFolder)
                throw new InvalidOperationException("Cannot modify child files or folders for a multi-root parent folder");

            _files = null;
            _folders = null;

            var folderNames = ReadChildFolderNamesFromDisk(foldersSearchPattern, acceptFolder);

            if (folderNames.Count > 0)
            {
                _folders = new Dictionary<string, FolderItem>();

                foreach (var folderName in folderNames)
                    _folders.Add(folderName.ToLower(), new FolderItem(this, folderName));
            }

            var fileNames = ReadChildFileNamesFromDisk(filesSearchPattern, acceptFile);

            if (fileNames.Count <= 0) 
                return;

            _files = new Dictionary<string, FileItem>();

            foreach (var fileName in fileNames)
                _files.Add(fileName.ToLower(), new FileItem(this, fileName));
        }


        public void AddDescendantFoldersFromDisk(Func<FolderItem, string, bool> acceptFolder = null)
        {
            AddDescendantFoldersFromDisk("*.*", acceptFolder);
        }

        public void AddDescendantFoldersFromDisk(string searchPattern, Func<FolderItem, string, bool> acceptFolder = null)
        {
            var foldersStack = new Stack<FolderItem>();

            foldersStack.Push(this);

            while (foldersStack.Count > 0)
            {
                var folder = foldersStack.Pop();

                if (folder.IsMultiRootParentFolder == false)
                    folder.AddChildFoldersFromDisk(searchPattern, acceptFolder);

                if (!folder.HasFolders) 
                    continue;

                foreach (var pair in folder._folders)
                    foldersStack.Push(pair.Value);
            }
        }

        public void AddDescendantItemsFromDisk()
        {
            AddDescendantItemsFromDisk("*.*", "*.*", null, null);
        }

        public void AddDescendantItemsFromDisk(string itemsSearchPattern)
        {
            AddDescendantItemsFromDisk(itemsSearchPattern, itemsSearchPattern, null, null);
        }

        public void AddDescendantItemsFromDisk(string foldersSearchPattern, string filesSearchPattern)
        {
            AddDescendantItemsFromDisk(foldersSearchPattern, filesSearchPattern, null, null);
        }

        public void AddDescendantItemsFromDisk(string itemsSearchPattern, Func<FolderItem, string, bool> acceptFolder, Func<FolderItem, string, bool> acceptFile)
        {
            AddDescendantItemsFromDisk(itemsSearchPattern, itemsSearchPattern, acceptFolder, acceptFile);
        }

        public void AddDescendantItemsFromDisk(string foldersSearchPattern, string filesSearchPattern, Func<FolderItem, string, bool> acceptItem)
        {
            AddDescendantItemsFromDisk(foldersSearchPattern, filesSearchPattern, acceptItem, acceptItem);
        }

        public void AddDescendantItemsFromDisk(string foldersSearchPattern, string filesSearchPattern, Func<FolderItem, string, bool> acceptFolder, Func<FolderItem, string, bool> acceptFile)
        {
            var foldersStack = new Stack<FolderItem>();

            foldersStack.Push(this);

            while (foldersStack.Count > 0)
            {
                var folder = foldersStack.Pop();

                if (folder.IsMultiRootParentFolder == false)
                    folder.AddChildItemsFromDisk(foldersSearchPattern, filesSearchPattern, acceptFolder, acceptFile);

                if (!folder.HasFolders) 
                    continue;

                foreach (var pair in folder._folders)
                    foldersStack.Push(pair.Value);
            }
        }


        public List<string> ReadChildFileNamesFromDisk(Func<FolderItem, string, bool> acceptFile = null)
        {
            return ReadChildFileNamesFromDisk("*.*", acceptFile);
        }

        public List<string> ReadChildFileNamesFromDisk(string searchPattern, Func<FolderItem, string, bool> acceptFile = null)
        {
            if (IsMultiRootParentFolder)
                throw new InvalidOperationException("Cannot read child files or folders from disk for a multi-root parent folder");

            var filesList = new List<string>();

            try
            {
                var filePaths = Directory.GetFiles(ItemPath, searchPattern, SearchOption.TopDirectoryOnly);

                if (acceptFile == null)
                    filesList
                        .AddRange(filePaths
                            .Select(Path.GetFileName));

                else
                    filesList
                        .AddRange(
                            filePaths
                                .Select(Path.GetFileName)
                                .Where(fileName => acceptFile(this, fileName))
                        );
            }
            catch
            {
                // ignored
            }

            return filesList;
        }

        public List<string> ReadChildFolderNamesFromDisk(Func<FolderItem, string, bool> acceptFolder = null)
        {
            return ReadChildFolderNamesFromDisk("*.*", acceptFolder);
        }

        public List<string> ReadChildFolderNamesFromDisk(string searchPattern, Func<FolderItem, string, bool> acceptFolder = null)
        {
            if (IsMultiRootParentFolder)
                throw new InvalidOperationException("Cannot read child files or folders from disk for a multi-root parent folder");

            var foldersList = new List<string>();

            try
            {
                var folderPaths = Directory.GetDirectories(ItemPath, searchPattern, SearchOption.TopDirectoryOnly);

                if (acceptFolder == null)
                    foldersList
                    .AddRange(folderPaths.Select(Path.GetFileName));

                else
                    foldersList
                        .AddRange(
                            folderPaths
                            .Select(Path.GetFileName)
                            .Where(folderName => acceptFolder(this, folderName))
                            );
            }
            catch
            {
                // ignored
            }

            return foldersList;
        }


        public void ClearFiles()
        {
            if (IsMultiRootParentFolder)
                throw new InvalidOperationException("Cannot modify child files or folders for a multi-root parent folder");

            _files = null;
        }

        public void ClearFolders()
        {
            if (IsMultiRootParentFolder)
                throw new InvalidOperationException("Cannot modify child files or folders for a multi-root parent folder");

            _folders = null;
        }

        public void ClearItems()
        {
            if (IsMultiRootParentFolder)
                throw new InvalidOperationException("Cannot modify child files or folders for a multi-root parent folder");

            _folders = null;
            _files = null;
        }


        public IEnumerable<FileItem> ChildFiles()
        {
            if (_files == null) 
                yield break;

            foreach (var pair in _files)
                yield return pair.Value;
        }

        public IEnumerable<FileItem> DescendantFiles()
        {
            var folderStack = new Stack<FolderItem>();

            folderStack.Push(this);

            while (folderStack.Count > 0)
            {
                var folder = folderStack.Pop();

                if (folder.HasFiles)
                    foreach (var pair in folder._files)
                        yield return pair.Value;

                if (!folder.HasFolders) 
                    continue;

                foreach (var pair in folder._folders)
                    folderStack.Push(pair.Value);
            }
        }

        public IEnumerable<FileItem> DescendantFiles(Func<FolderItem, bool> visitFolder)
        {
            var folderStack = new Stack<FolderItem>();

            if (visitFolder(this))
                folderStack.Push(this);

            while (folderStack.Count > 0)
            {
                var folder = folderStack.Pop();

                if (folder.HasFiles)
                    foreach (var pair in folder._files)
                        yield return pair.Value;

                if (!folder.HasFolders) 
                    continue;

                foreach (var pair in folder._folders.Where(pair => visitFolder(pair.Value)))
                    folderStack.Push(pair.Value);
            }
        }


        public IEnumerable<FolderItem> ChildFolders()
        {
            if (_folders == null) 
                yield break;

            foreach (var pair in _folders)
                yield return pair.Value;
        }

        public IEnumerable<FolderItem> DescendantFolders()
        {
            if (_folders == null) 
                yield break;

            var folderStack = new Stack<FolderItem>();

            foreach (var pair in _folders)
                folderStack.Push(pair.Value);

            while (folderStack.Count > 0)
            {
                var folder = folderStack.Pop();

                yield return folder;

                if (!folder.HasFolders) 
                    continue;

                foreach (var pair in folder._folders)
                    folderStack.Push(pair.Value);
            }
        }

        public IEnumerable<FolderItem> DescendantFolders(Func<FolderItem, bool> visitFolder)
        {
            if (_folders == null) 
                yield break;

            var folderStack = new Stack<FolderItem>();

            foreach (var pair in _folders.Where(pair => visitFolder(pair.Value)))
                folderStack.Push(pair.Value);

            while (folderStack.Count > 0)
            {
                var folder = folderStack.Pop();

                yield return folder;

                if (!folder.HasFolders) 
                    continue;

                foreach (var pair in folder._folders.Where(pair => visitFolder(pair.Value)))
                    folderStack.Push(pair.Value);
            }
        }

        public IEnumerable<FolderItem> DownTreeFolders()
        {
            if (_folders != null)
            {
                var folderStack = new Stack<FolderItem>();

                folderStack.Push(this);

                while (folderStack.Count > 0)
                {
                    var folder = folderStack.Pop();

                    yield return folder;

                    if (!folder.HasFolders) 
                        continue;

                    foreach (var pair in folder._folders)
                        folderStack.Push(pair.Value);
                }
            }
            else if (IsMultiRootParentFolder == false)
                yield return this;
        }

        public IEnumerable<FolderItem> DownTreeFolders(Func<FolderItem, bool> visitFolder)
        {
            if (_folders != null)
            {
                var folderStack = new Stack<FolderItem>();

                if (visitFolder(this))
                    folderStack.Push(this);

                while (folderStack.Count > 0)
                {
                    var folder = folderStack.Pop();

                    yield return folder;

                    if (!folder.HasFolders) 
                        continue;

                    foreach (var pair in folder._folders.Where(pair => visitFolder(pair.Value)))
                        folderStack.Push(pair.Value);
                }
            }
            else if (IsMultiRootParentFolder == false && visitFolder(this))
                yield return this;
        }

        public IEnumerable<FolderItem> AncestorFolders()
        {
            for (
                var folder = ParentFolder;
                IsProper(folder);
                folder = folder.ParentFolder
                )
                yield return folder;
        }

        public IEnumerable<FolderItem> AncestorFolders(Func<FolderItem, bool> visitFolder)
        {
            for (
                var folder = ParentFolder;
                IsProper(folder) && visitFolder(folder);
                folder = folder.ParentFolder
                )
                yield return folder;
        }

        public IEnumerable<FolderItem> UpTreeFolders()
        {
            for (
                var folder = this;
                IsProper(folder); 
                folder = folder.ParentFolder
                )
                yield return folder;
        }

        public IEnumerable<FolderItem> UpTreeFolders(Func<FolderItem, bool> visitFolder)
        {
            for (
                var folder = this;
                IsProper(folder) && visitFolder(folder);
                folder = folder.ParentFolder
                )
                yield return folder;
        }

        public IEnumerable<FolderItem> UpDownTreeFolders()
        {
            for (
                var folder = this;
                IsProper(folder);
                folder = folder.ParentFolder
                )
                yield return folder;

            if (_folders == null) 
                yield break;

            var folderStack = new Stack<FolderItem>();

            foreach (var pair in _folders)
                folderStack.Push(pair.Value);

            while (folderStack.Count > 0)
            {
                var folder = folderStack.Pop();

                yield return folder;

                if (!folder.HasFolders) 
                    continue;

                foreach (var pair in folder._folders)
                    folderStack.Push(pair.Value);
            }
        }

        public IEnumerable<FolderItem> UpDownTreeFolders(Func<FolderItem, bool> visitFolder)
        {
            for (
                var folder = this;
                IsProper(folder) && visitFolder(folder);
                folder = folder.ParentFolder
                )
                yield return folder;

            if (_folders == null) 
                yield break;

            var folderStack = new Stack<FolderItem>();

            foreach (var pair in _folders.Where(pair => visitFolder(pair.Value)))
                folderStack.Push(pair.Value);

            while (folderStack.Count > 0)
            {
                var folder = folderStack.Pop();

                yield return folder;

                if (!folder.HasFolders) 
                    continue;

                foreach (var pair in folder._folders.Where(pair => visitFolder(pair.Value)))
                    folderStack.Push(pair.Value);
            }
        }


        public IEnumerable<FolderTreeItem> ChildItems()
        {
            if (_folders != null)
                foreach (var pair in _folders)
                    yield return pair.Value;

            if (_files == null) 
                yield break;

            foreach (var pair in _files)
                yield return pair.Value;
        }

        public IEnumerable<FolderTreeItem> DescendantItems()
        {
            var folderStack = new Stack<FolderItem>();

            if (_files != null)
                foreach (var pair in _files)
                    yield return pair.Value;

            if (_folders != null)
                foreach (var pair in _folders)
                    folderStack.Push(pair.Value);

            while (folderStack.Count > 0)
            {
                var folder = folderStack.Pop();

                yield return folder;

                if (folder.HasFiles)
                    foreach (var pair in folder._files)
                        yield return pair.Value;

                if (!folder.HasFolders) 
                    continue;

                foreach (var pair in folder._folders)
                    folderStack.Push(pair.Value);
            }
        }

        public IEnumerable<FolderTreeItem> DescendantItems(Func<FolderItem, bool> visitFolder)
        {
            var folderStack = new Stack<FolderItem>();

            if (_files != null)
                foreach (var pair in _files)
                    yield return pair.Value;

            if (_folders != null)
                foreach (var pair in _folders.Where(pair => visitFolder(pair.Value)))
                    folderStack.Push(pair.Value);

            while (folderStack.Count > 0)
            {
                var folder = folderStack.Pop();

                yield return folder;

                if (folder.HasFiles)
                    foreach (var pair in folder._files)
                        yield return pair.Value;

                if (!folder.HasFolders) 
                    continue;

                foreach (var pair in folder._folders.Where(pair => visitFolder(pair.Value)))
                    folderStack.Push(pair.Value);
            }
        }

        public IEnumerable<FolderTreeItem> DownTreeItems()
        {
            var folderStack = new Stack<FolderItem>();

            folderStack.Push(this);

            while (folderStack.Count > 0)
            {
                var folder = folderStack.Pop();

                if (folder.IsMultiRootParentFolder == false)
                    yield return folder;

                if (folder.HasFiles)
                    foreach (var pair in folder._files)
                        yield return pair.Value;

                if (!folder.HasFolders) 
                    continue;

                foreach (var pair in folder._folders)
                    folderStack.Push(pair.Value);
            }
        }

        public IEnumerable<FolderTreeItem> DownTreeItems(Func<FolderItem, bool> visitFolder)
        {
            var folderStack = new Stack<FolderItem>();

            if (visitFolder(this))
                folderStack.Push(this);

            while (folderStack.Count > 0)
            {
                var folder = folderStack.Pop();

                if (folder.IsMultiRootParentFolder == false)
                    yield return folder;

                if (folder.HasFiles)
                    foreach (var pair in folder._files)
                        yield return pair.Value;

                if (!folder.HasFolders) 
                    continue;

                foreach (var pair in folder._folders.Where(pair => visitFolder(pair.Value)))
                    folderStack.Push(pair.Value);
            }
        }

        public IEnumerable<FolderTreeItem> UpDownTreeItems()
        {
            for (
                var folder = this;
                IsProper(folder);
                folder = folder.ParentFolder
                )
                yield return folder;

            var folderStack = new Stack<FolderItem>();

            if (_files != null)
                foreach (var pair in _files)
                    yield return pair.Value;

            if (_folders != null)
                foreach (var pair in _folders)
                    folderStack.Push(pair.Value);

            while (folderStack.Count > 0)
            {
                var folder = folderStack.Pop();

                yield return folder;

                if (folder.HasFiles)
                    foreach (var pair in folder._files)
                        yield return pair.Value;

                if (!folder.HasFolders) 
                    continue;

                foreach (var pair in folder._folders)
                    folderStack.Push(pair.Value);
            }
        }

        public IEnumerable<FolderTreeItem> UpDownTreeItems(Func<FolderTreeItem, bool> visitFolder)
        {
            for (
                var folder = this;
                IsProper(folder) && visitFolder(folder);
                folder = folder.ParentFolder
                )
                yield return folder;

            var folderStack = new Stack<FolderItem>();

            if (_files != null)
                foreach (var pair in _files)
                    yield return pair.Value;

            if (_folders != null)
                foreach (var pair in _folders.Where(pair => visitFolder(pair.Value)))
                    folderStack.Push(pair.Value);

            while (folderStack.Count > 0)
            {
                var folder = folderStack.Pop();

                yield return folder;

                if (folder.HasFiles)
                    foreach (var pair in folder._files)
                        yield return pair.Value;

                if (!folder.HasFolders) 
                    continue;

                foreach (var pair in folder._folders.Where(pair => visitFolder(pair.Value)))
                    folderStack.Push(pair.Value);
            }
        }
    }
}
