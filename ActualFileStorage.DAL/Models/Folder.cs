using System;

namespace ActualFileStorage.DAL.Models
{
    public class Folder
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public FileAccess Visibility { get; set; }
        public DateTime CreationTime { get; set; }

        public virtual User User { get; set; }
    }


}
