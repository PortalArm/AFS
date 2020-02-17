﻿using ActualFileStorage.DAL.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ActualFileStorage.BLL.FileHandlers
{
    class LocalServerStorage : IFileRoutine
    {
        private string _root;
        public LocalServerStorage(string root)
        {
            _root = root;
        }
        public byte[] DownloadFile(User user, DAL.Models.File file)
        {
            string infile = Path.Combine(_root, user.Folder.Name, file.Hash);
            if (!System.IO.File.Exists(infile))
                throw new FileNotFoundException();
            return System.IO.File.ReadAllBytes(infile);
        }

        public void UploadFile(User user, DAL.Models.File file, byte[] data)
        {
            string outfile = Path.Combine(_root, user.Folder.Name, file.Hash);
#if !DEBUG
            if (System.IO.File.Exists(outfile))
                throw new IOException();
#endif

            System.IO.File.WriteAllBytes(outfile, data);
            //using (StreamWriter s = new StreamWriter(Path.Combine(_root, user.Folder.Name)))
            //using (BinaryWriter bw = new BinaryWriter(s.BaseStream))
            //{
            //    //bw.Write()
            //}
        }
    }
}