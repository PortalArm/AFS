using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ActualFileStorage.BLL.DTOs
{
    public enum ObjectType { File, Folder }
    public abstract class ObjectBase
    {
        public ObjectType ObjectType { get; set; }
    }
}
