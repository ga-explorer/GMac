using System.IO;

namespace UtilLib.FolderTree
{
    /// <summary>
    /// This class represents a folder or file in a tree of folders
    /// </summary>
    public abstract class FolderTreeItem
    {
        /// <summary>
        /// The parent folder of this item
        /// </summary>
        public FolderItem ParentFolder { get; private set; }

        /// <summary>
        /// The name of the item including the extension
        /// </summary>
        public string ItemName { get; private set; }


        /// <summary>
        /// The extension of the item
        /// </summary>
        public string ItemExtension
        {
            get
            {
                var fileExt = Path.GetExtension(ItemName);

                if (fileExt == null || fileExt.Length <= 1)
                    return "";

                return fileExt.Substring(1);
            }
        }

        /// <summary>
        /// The name of the item excluding the extension
        /// </summary>
        public string ItemNameWithoutExtension
        {
            get
            {
                var itemName = ItemName;
                var fileExt = Path.GetExtension(itemName);

                return 
                    string
                    .IsNullOrEmpty(fileExt) 
                    ? itemName 
                    : itemName.Substring(0, itemName.Length - fileExt.Length);
            }
        }

        /// <summary>
        /// The full path of the item including the extension
        /// </summary>
        public string ItemPath 
        { 
            get 
            { 
                return (ItemLevel <= 0) ? ItemName : Path.Combine(ParentFolderPath, ItemName); 
            } 
        }

        /// <summary>
        /// The full path of the item excluding the extension
        /// </summary>
        public string ItemPathWithoutExtension { get { return Path.Combine(ParentFolderPath, ItemNameWithoutExtension); } }

        /// <summary>
        /// The full path of the parent folder
        /// </summary>
        public string ParentFolderPath { get { return FolderItem.IsProper(ParentFolder) ? ParentFolder.ItemPath : ""; } }

        /// <summary>
        /// True if this item has a proper parent folder (equivalent to having a level of 1 or more)
        /// </summary>
        public bool HasParentFolder { get { return FolderItem.IsProper(ParentFolder); } }

        /// <summary>
        /// True if the item is a file
        /// </summary>
        public bool IsFile { get { return this is FileItem; } }

        /// <summary>
        /// True if the item is a folder (proper or not)
        /// </summary>
        public bool IsFolder { get { return this is FolderItem; } }

        /// <summary>
        /// True if the item is a file with level 0. This can only happen if the parent folder is
        /// a multi-root folder item
        /// </summary>
        public bool IsRootFile { get { return (this is FileItem) && (ItemLevel == 0); } }

        /// <summary>
        /// True is the item is a folder with level 0
        /// </summary>
        public bool IsRootFolder { get { return (this is FolderItem) && (ItemLevel == 0); } }

        /// <summary>
        /// True if the item has level 0
        /// </summary>
        public bool IsRootItem { get { return (ItemLevel == 0); } }

        /// <summary>
        /// True if the item is a file with level 1 or more
        /// </summary>
        public bool IsNonRootFile { get { return (this is FileItem) && (ItemLevel > 0); } }

        /// <summary>
        /// True if the item is a folder with level 1 or more
        /// </summary>
        public bool IsNonRootFolder { get { return (this is FolderItem) && (ItemLevel > 0); } }

        /// <summary>
        /// True if the item is level 1 or more
        /// </summary>
        public bool IsNonRootItem { get { return (ItemLevel > 0); } }

        /// <summary>
        /// True if this item is a multi-root parent folder. A multi-root parent folder has level -1
        /// </summary>
        public bool IsMultiRootParentFolder { get { return (this is FolderItem) && (ItemLevel == -1); } }


        /// <summary>
        /// The level of this item in the folders tree. Root items have level 0.
        /// </summary>
        public abstract int ItemLevel { get; }


        protected FolderTreeItem(FolderItem parentFolder, string itemName)
        {
            ParentFolder = parentFolder;
            ItemName = itemName;
        }


        public override string ToString()
        {
            return ItemPath;
        }
    }
}
