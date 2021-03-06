﻿using ActualFileStorage.DAL.Models;

namespace ActualFileStorage.BLL.FileHandlers
{
    // to server
    public interface IFileUpload
    {
        void UploadFile(User user, File file, byte[] data);
        void UploadFile(User user, File file, System.IO.Stream data);
    }
}
