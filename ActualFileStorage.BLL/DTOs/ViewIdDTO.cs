using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ActualFileStorage.BLL.DTOs
{
    public class ViewIdDTO
    {
        public int? UserId { get; set; }
        public int? FolderId { get; set; }
        public bool IsFile { get; set; }
        public bool Exists { get; set; }
        public int? FileId { get; set; }

        //public FileInfoDTO File { get; set; }
    }
}
