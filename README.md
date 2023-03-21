# Cascade_Book

**Cascade Book API**
This is a solution for a REST API built using ASP.NET MVC that manages a collection of books. The API allows for sorting books by publisher, author (last name, first name), and title, as well as adding new entries to the book table.

**Data Definition**
The Book class has the following properties:
•	Publisher (string): the publisher of the book
•	Title (string): the title of the book
•	AuthorLastName (string): the last name of the author of the book
•	AuthorFirstName (string): the first name of the author of the book
•	Price (decimal): the price of the book
•	PublicationYear (string): the year the book got published
•	Edition (string): the edition of the book

**Project Structure:**
The project includes the following components:
BooksController.cs: contains the implementation of the REST API endpoints.
Book.cs: defines the Book object with its attributes.
BookContext.cs: defines the Book database context with its tables.
DBScripts folder: contains SQL scripts for creating the Book table and stored procedures.

**Table Design**
The Book data can be stored in a single table with the following fields and datatypes:

Field Name	    Data Type
Id	            int (PK)
Publisher	      nvarchar
Title	          nvarchar
AuthorLastName	nvarchar
AuthorFirstName	nvarchar
Price	          decimal

**API Endpoints**
The following API endpoints are available:

GET /api/books
Returns a sorted list of books by Publisher, Author (last name, first name), then title.
POST /api/books
Adds a new book entry to the database.
GET /api/books/ GetBooks_UsingSP
Adds a new book entry to the database using a stored procedure.
GET /api/books/AddBook_UsingSP
Adds a new book entry to the database using a stored procedure.
GET /api/books/totalPrice
Returns the total price of all books in the database.

**Stored Procedures**
The following stored procedures are used:
GetBooks
Returns a sorted list of books by Publisher, Author (last name, first name), then title.
AddBook
Adds a new book entry to the database.

**Test Data**
INSERT INTO Books (Publisher, Title, AuthorLastName, AuthorFirstName, Price)
VALUES
  ('PublisherA', 'Title1', 'AuthorLastName1', 'AuthorFirstName1', 10.99),
  ('PublisherB', 'Title2', 'AuthorLastName2', 'AuthorFirstName2', 20.00),
  ('PublisherC', 'Title3', 'AuthorLastName3', 'AuthorFirstName3', 30.00),
  ('PublisherD', 'Title4', 'AuthorLastName4', 'AuthorFirstName4', 40.00),
  ('PublisherE', 'Title5', 'AuthorLastName5', 'AuthorFirstName5', 50.10);


