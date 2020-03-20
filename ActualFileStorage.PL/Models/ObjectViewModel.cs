using ActualFileStorage.BLL.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ActualFileStorage.PL.Models
{
    public class ObjectsViewModel
    {
        public int ParentFolderId { get; set; }
        public IEnumerable<FileViewModel> Files { get; set; }
        public IEnumerable<FolderViewModel> Folders { get; set; }
        public IEnumerable<HistoryItemViewModel> History { get; set; }
    }
}