namespace UtilLib.FolderTree
{
    public sealed class FileItem : FolderTreeItem
    {
        public override int ItemLevel { get { return ParentFolder.ItemLevel + 1; } }


        internal FileItem(FolderItem parentFolder, string fileName)
            : base(parentFolder, fileName)
        {
        }
    }
}
