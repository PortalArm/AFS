using ActualFileStorage.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ActualFileStorage.BLL.FileHandlers
{
    // from server
    public interface IFileDownload
    {
        byte[] DownloadFile(User user, File file);
        //Не совсем работает так, как хотелось бы
        //System.IO.Stream DownloadFileAsStream(User user, File file);
    }
}
