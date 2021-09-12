create proc StudentViewByID

@Id int
as

select *from tbl_student where Id=@Id
