using System;

namespace ActualFileStorage.DAL.Models
{
    public class User
    {
        public int Id { get; set; } // req 
        public string Login { get; set; } // req 
        public string FirstName { get; set; } // req 
        public string SecondName { get; set; }
        public DateTime BirthDate { get; set; }

        public string Salt { get; set; }
        public string PassHash { get; set; } //  => Hash(Operation(Password, Salt))
        public string Email { get; set; }

        //public int FolderId { get; set; }

        //public int FolderId { get; set; } // req 


        public virtual Folder Folder { get; set; }
        //public LoginInfo LoginInfo { get; set; }
    }

    //public class File
    //{
    //    public int FileId { get; set; } // req, ident

    //    public int CreatorId { get; set; }
    //    public User Creator { get; set; } // req, root => admin?

    //    public string Name { get; set; } // req
    //    public string Extension { get; set; } // opt
    //    public FileType Type { get; set; } // req
    //    public FileAccess Access { get; set; } // req
    //    public DateTime CreationTime { get; set; } // auto-gen(?) req | в CUOW объединить создание записи и загрузку на сервер
    //    public int ParentId { get; set; }
    //    public File Parent { get; set; }  // better be indexed, req
    //    public ICollection<File> Children { get; set; }
    //    public ICollection<User> Users { get; set; } // hz

    //}

}
