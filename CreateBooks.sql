
-- Id				int	Primary key
-- Publisher		nvarchar(100)	
-- Title			nvarchar(100)	
-- AuthorLastName	nvarchar(50)	
-- AuthorFirstName	nvarchar(50)	
-- Price			decimal(18,2)	

CREATE TABLE Books (
    Id INT NOT NULL IDENTITY(1,1) PRIMARY KEY,
    Publisher NVARCHAR(100) NOT NULL,
    Title NVARCHAR(100) NOT NULL,
    AuthorLastName NVARCHAR(50) NOT NULL,
    AuthorFirstName NVARCHAR(50) NOT NULL,
    Price DECIMAL(18,2) NOT NULL
);
