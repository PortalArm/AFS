using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ActualFileStorage.BLL.DTOs
{
    public class ObjectsDTO
    {
        public int ParentFolderId { get; set; }
        public IEnumerable<FileDTO> Files { get; set; }
        public IEnumerable<FolderDTO> Folders { get; set; }
        public IEnumerable<HistoryItemDTO> History { get; set; }
        public FolderInfoDTO Parent { get; set; }
    }
}
