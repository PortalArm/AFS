create procedure getAllUserFolders(
@userId as int)
as
with cte_folders as (
select Folders.Id, Folders.Name, Folders.CreationTime, Folders.ParentFolder_Id from Folders inner join Users on Users.FolderId = Folders.Id and Users.Id = @userId and Folders.ParentFolder_Id is null
union all
select a.Id, a.Name, a.CreationTime, a.ParentFolder_Id from Folders a inner join cte_folders b on b.Id = a.ParentFolder_Id)
select cte_folders.Id from cte_folders;

getAllUserFolders 3