create proc StudentDeleteByID

@Id int
as

Delete from tbl_student where Id=@Id
