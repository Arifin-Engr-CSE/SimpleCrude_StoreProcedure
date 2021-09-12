
Create proc StudentAddOrEdit
@Id int,
@Name varchar(20),
@Gender varchar(20),
@Address varchar(20)

AS
IF(@Id=0)
insert into tbl_student([Name],Gender,[Address])
values(@Name,@Gender,@Address)

ELSE
UPDATE tbl_student
SET
[Name]=@Name,
Gender=@Gender,
[Address]=@Address WHERE Id=@Id