using ActualFileStorage.DAL.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ActualFileStorage.PL.Models
{
    public class FileInfoViewModel
    {
        public int Id { get; set; }
        [Display(ResourceType = typeof(Localization.InfoRes),Name = "FileName")]
        public string FullName { get; set; }
        [Display(ResourceType = typeof(Localization.InfoRes),Name = "FileSize")]
        public string Size { get; set; }
        [Display(ResourceType = typeof(Localization.InfoRes), Name = "Visibility")]
        public FileAccess Visibility { get; set; }
        [Display(ResourceType = typeof(Localization.InfoRes),Name = "CreationDate")]
        public DateTime CreationTime { get; set; }
        [Display(ResourceType = typeof(Localization.InfoRes),Name = "ShortLink")]
        public string ShortLink { get; set; }
        public bool ReadOnlyLink { get; set; }
    }
}