using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ActualFileStorage.DAL.Models
{
    public class ShortFolder
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public FileAccess Visibility { get; set; }
    }
}
