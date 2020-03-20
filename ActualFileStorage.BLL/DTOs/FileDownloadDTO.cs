using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ActualFileStorage.BLL.DTOs
{
    public class FileDownloadDTO
    {
        public string FileName { get; set; }
        public byte[] Data { get; set; }
        public bool IsPermitted { get; set; }
    }
}
