using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ActualFileStorage.BLL.DTOs
{
    public class FileDTO : ObjectBase
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Extension { get; set; }

    }
}
