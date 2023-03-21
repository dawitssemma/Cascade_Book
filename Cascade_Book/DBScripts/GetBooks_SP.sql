CREATE PROCEDURE GetBooks
AS
BEGIN
    SELECT Publisher, AuthorLastName, AuthorFirstName, Title, Price
    FROM Books
    ORDER BY Publisher, AuthorLastName, AuthorFirstName, Title
END
