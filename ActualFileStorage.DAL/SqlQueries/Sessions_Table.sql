create table Sessions(
	Id int primary key,
	UserId int,
	ExpireTime datetime not null,
	CreationTime datetime not null,
	SpecVal nvarchar(64) not null
	CONSTRAINT FK_User_Id_CASCADEDELETE
    FOREIGN KEY (UserId)
    REFERENCES Users(Id)
    ON DELETE CASCADE
)