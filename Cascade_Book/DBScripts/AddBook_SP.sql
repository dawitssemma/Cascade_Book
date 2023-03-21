CREATE PROCEDURE AddBook
    @Publisher NVARCHAR(100),
    @Title NVARCHAR(100),
    @AuthorLastName NVARCHAR(50),
    @AuthorFirstName NVARCHAR(50),
    @Price DECIMAL(18,2)
AS
BEGIN
    INSERT INTO Books (Publisher, Title, AuthorLastName, AuthorFirstName, Price)
    VALUES (@Publisher, @Title, @AuthorLastName, @AuthorFirstName, @Price)

    SELECT SCOPE_IDENTITY() AS Id
END
