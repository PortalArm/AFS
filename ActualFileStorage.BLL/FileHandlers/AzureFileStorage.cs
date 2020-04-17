using ActualFileStorage.DAL.Models;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ActualFileStorage.BLL.FileHandlers
{
    public class AzureFileStorage : IStorage
    {
        private static string storageConnectionString = new string(Convert.FromBase64String("RGVmYXVsdEVuZHBvaW50c1Byb3RvY29sPWh0dHBzO0FjY291bnROYW1lPWZzZmlsZXN0b3JhZ2UxO0FjY291bnRLZXk9OWoyZzkzWFlhYzB2ZS9ndHhrOXBkSU0vTVBIYlUvd0ZGSGlFYTlRdTY3YXZoV2Q0U3hheVpITE05aG5nYUhvQlVkb1l4NUF2Smt4dVpaTlJVT2EycEE9PTtFbmRwb2ludFN1ZmZpeD1jb3JlLndpbmRvd3MubmV0").Select(v => (char)v).ToArray());
        private static string _blobName = "web-storage";
        private CloudStorageAccount _account;
        private CloudBlobClient _serviceClient;
        private CloudBlobContainer _fileContainer;
        public AzureFileStorage()
        {
            _account = CloudStorageAccount.Parse(storageConnectionString);
            _serviceClient = _account.CreateCloudBlobClient();

            _fileContainer = _serviceClient.GetContainerReference(_blobName);
            _fileContainer.CreateIfNotExistsAsync().Wait();
        }

        public void DeleteFile(User user, DAL.Models.File file)
        {
            var directory = _fileContainer.GetDirectoryReference(user.Folder.Name);
            var blob = directory.GetBlockBlobReference(file.Hash);
            blob.DeleteIfExists();
        }

        public void DeleteFiles(User user, IEnumerable<DAL.Models.File> files)
        {
            var directory = _fileContainer.GetDirectoryReference(user.Folder.Name);
            foreach (var file in files)
                directory.GetBlockBlobReference(file.Hash).DeleteIfExists();
        }

        public byte[] DownloadFile(User user, DAL.Models.File file)
        {
            var directory = _fileContainer.GetDirectoryReference(user.Folder.Name);
            var blob = directory.GetBlockBlobReference(file.Hash); blob.FetchAttributes();
            var len = blob.Properties.Length;
            byte[] output = new byte[len];
            blob.DownloadToByteArray(output, 0);
            return output;
        }

        //public string GetDownloadLink(User user, DAL.Models.File file)
        //{
        //    var directory = _fileContainer.GetDirectoryReference(user.Folder.Name);
        //    var blob = directory.GetBlockBlobReference(file.Hash); blob.FetchAttributes();
        //    var len = blob.Properties.Length;
        //    byte[] output = new byte[len];
        //    blob.DownloadToByteArray(output, 0);
        //    return output;
        //}


        //public Stream DownloadFileAsStream(User user, DAL.Models.File file)
        //{
        //    var directory = _fileContainer.GetDirectoryReference(user.Folder.Name);
        //    var blob = directory.GetBlockBlobReference(file.Hash);
        //    MemoryStream stream = new MemoryStream();
        //    blob.DownloadToStream(stream);
        //    return stream;
        //}

        public void UploadFile(User user, DAL.Models.File file, byte[] data)
        {
            var directory = _fileContainer.GetDirectoryReference(user.Folder.Name);
            var blob = directory.GetBlockBlobReference(file.Hash);
            blob.UploadFromByteArray(data, 0, data.Length);
            //CloudBlockBlob blob = _fileContainer.GetBlockBlobReference(file.Hash);
            // blob.UploadTextAsync("Hello, World!").Wait();
        }

        public void UploadFile(User user, DAL.Models.File file, Stream data)
        {
            var directory = _fileContainer.GetDirectoryReference(user.Folder.Name);
            var blob = directory.GetBlockBlobReference(file.Hash);
            blob.UploadFromStream(data);
        }
    }
}
