using ActualFileStorage.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ActualFileStorage.BLL.FileHandlers
{
    // from server
    public interface IFileRoutine : IFileUpload, IFileDownload
    {
    }
}
