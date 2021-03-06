﻿using ActualFileStorage.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ActualFileStorage.DAL.Repositories
{
    public interface IFileRepository : IRepository<File>
    {
        IEnumerable<File> GetAllFilesInFolder(int folderId);
        File GetFileByLink(string link);
    }
}
