using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ActualFileStorage.BLL.DTOs
{
    public class FileUploadDTO
    {
        public string FileName { get; set; }
        public Stream Data { get; set; }
    }
}
