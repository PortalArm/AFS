create procedure getAllUserFolders(
@userId as int)
as
with cte_folders as (
select Folders.Id, Folders.Name, Folders.CreationTime, Folders.ParentFolder_Id from Folders inner join Users on Users.FolderId = Folders.Id and Users.Id = @userId and Folders.ParentFolder_Id is null
union all
select a.Id, a.Name, a.CreationTime, a.ParentFolder_Id from Folders a inner join cte_folders b on b.Id = a.ParentFolder_Id)
select cte_folders.Id from cte_folders;
go

create function getDescendantFoldersIds(
@basefolderid as int)
returns @output table(Id int)
as
begin
with cte_folders as (
select folders.id, folders.name, folders.parentfolder_id from folders where folders.id = @basefolderid
union all
select a.id, a.name, a.parentfolder_id from folders a inner join cte_folders b on a.parentfolder_id = b.id)
insert @output
select id from cte_folders;
return
end;
go

create function getDescendantFilesIds(
@basefolderid as int)
returns @output table(Id int)
as
begin
insert @output
select Files.Id from Files where  Files.Folder_Id in (select * from getDescendantFoldersIds(@basefolderid))
return
end;
go

--nicer
create function getDescendantFilesIds(
@basefolderid as int)
returns table
as
return
select * from Files where  Files.Folder_Id in (select * from getDescendantFoldersIds(@basefolderid))
--от @folderId до корневой папки
--;with cte_folders as (
--select Folders.Id, Folders.Name, Folders.ParentFolder_Id from Folders where Folders.Id = @folderId
--union all
--select a.Id, a.Name, a.ParentFolder_Id from Folders a inner join cte_folders b on b.ParentFolder_Id = a.Id)
--select * from cte_folders;
--getAllUserFolders 3

--от @folderId и далее по иерархии
--;with cte_folders as (
--select Folders.Id, Folders.Name, Folders.ParentFolder_Id from Folders where Folders.Id = @folderId
--union all
--select a.Id, a.Name, a.ParentFolder_Id from Folders a inner join cte_folders b on a.ParentFolder_Id = b.Id)
--select * from cte_folders;


create function getFoldersIdsToRoot(
@basefolderid as int)
returns @output table(Id int, Name varchar(max), Visibility int)
as
begin
with cte_folders as (
select Folders.Id, Folders.Name, Folders.Visibility, Folders.ParentFolder_Id from Folders where Folders.Id = @basefolderid
union all
select a.Id, a.Name, a.Visibility, a.ParentFolder_Id from Folders a inner join cte_folders b on b.ParentFolder_Id = a.Id)
insert @output
select Id, Name, Visibility from cte_folders;
return
end;
go



--нахождение Id хоз€ина папки
create function getFolderOwnerId(
@basefolderid as int)
returns int
as
begin
declare @userName varchar(max);
with cte_folders as (
select RowNum = 1, Folders.Name, Folders.ParentFolder_Id from Folders where Folders.Id = @basefolderid
union all
select RowNum + 1 as RowNum, a.Name, a.ParentFolder_Id  from Folders a inner join cte_folders b  on b.ParentFolder_Id = a.Id)
select top 1 @userName = Name from cte_folders order by RowNum desc
return (select Id from Users where Login = @userName)
end;
go