using ActualFileStorage.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ActualFileStorage.BLL.DTOs
{
    public class FileInfoDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public long Size { get; set; }
        public string Extension { get; set; }
        //public int FolderId { get; set; }
        public FileAccess Visibility { get; set; }
        public DateTime CreationTime { get; set; }
        public string ShortLink { get; set; }
    }
}
