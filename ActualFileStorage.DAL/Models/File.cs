using System;
using System.Collections.Generic;

namespace ActualFileStorage.DAL.Models
{

    public class File
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Hash { get; set; }
        public long Size { get; set; }
        public string Ext { get; set; }
        //public int FolderId { get; set; }
        public FileAccess Visibility { get; set; }
        public DateTime CreationTime { get; set; }
        public string ShortLink { get; set; }

        public virtual Folder Folder { get; set; }
    }

}
