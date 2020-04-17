using ActualFileStorage.DAL.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ActualFileStorage.PL.Models
{
    public class FolderInfoViewModel
    {
        public int Id { get; set; }
        [Display(ResourceType = typeof(Localization.InfoRes), Name = "FolderName")]
        public string Name { get; set; }
        [Display(ResourceType = typeof(Localization.InfoRes), Name = "Visibility")]
        public FileAccess Visibility { get; set; }
        [Display(ResourceType = typeof(Localization.InfoRes), Name = "ShortLink")]
        public string ShortLink { get; set; }
        public bool ReadOnlyLink { get; set; }
    }
}