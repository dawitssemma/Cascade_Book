using Cascade_Book.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace Cascade_Book.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BooksController : ControllerBase
    {
        private readonly BookContext _context;

        public BooksController(BookContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Book>>> GetBooks()
        {
            try
            {
                var books = await _context.Books
                                .OrderBy(b => b.Publisher)
                                .ThenBy(b => b.AuthorLastName)
                                .ThenBy(b => b.AuthorFirstName)
                                .ThenBy(b => b.Title)
                                .ToListAsync();

                return Ok(books);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost]
        public IActionResult AddBook(Book book)
        {
            try
            {
                _context.Books.Add(book);
                _context.SaveChanges();

                return Ok(book);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet]
        public IActionResult GetBooks_UsingSP()
        {
            try
            {
                var sqlCommand = new SqlCommand("GetBooksSorted", new SqlConnection());
                sqlCommand.CommandType = CommandType.StoredProcedure;

                sqlCommand.Connection.Open();
                var reader = sqlCommand.ExecuteReader();

                var books = new List<Book>();
                while (reader.Read())
                {
                    var book = new Book
                    {
                        Publisher = reader.GetString(0),
                        AuthorLastName = reader.GetString(1),
                        AuthorFirstName = reader.GetString(2),
                        Title = reader.GetString(3),
                        Price = reader.GetDecimal(4)
                    };
                    books.Add(book);
                }

                sqlCommand.Connection.Close();

                return Ok(books);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }


        [HttpPost]
        public IActionResult AddBook_UsingSP(Book book)
        {
            try
            {
                var publisherParam = new SqlParameter("@Publisher", book.Publisher);
                var titleParam = new SqlParameter("@Title", book.Title);
                var authorLastNameParam = new SqlParameter("@AuthorLastName", book.AuthorLastName);
                var authorFirstNameParam = new SqlParameter("@AuthorFirstName", book.AuthorFirstName);
                var priceParam = new SqlParameter("@Price", book.Price);

                var sqlCommand = new SqlCommand("AddBook", new SqlConnection());
                sqlCommand.CommandType = CommandType.StoredProcedure;
                sqlCommand.Parameters.AddRange(new[] { publisherParam, titleParam, authorLastNameParam, authorFirstNameParam, priceParam });

                // Execute the SqlCommand to add the Book object to the Book table in the database
                sqlCommand.Connection.Open();
                sqlCommand.ExecuteNonQuery();
                sqlCommand.Connection.Close();

                return Ok(book);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }


        [HttpGet]
        [Route("TotalPrice")]
        public IActionResult GetTotalPrice()
        {
            try
            {
                decimal totalPrice = _context.Books.Sum(b => b.Price);
                return Ok(totalPrice);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        public void SaveBooks(List<Book> books)
        {
            // Convert the list of books to a DataTable
            DataTable table = new DataTable();
            table.Columns.Add("Publisher", typeof(string));
            table.Columns.Add("Title", typeof(string));
            table.Columns.Add("AuthorLastName", typeof(string));
            table.Columns.Add("AuthorFirstName", typeof(string));
            table.Columns.Add("Price", typeof(decimal));
            foreach (var book in books)
            {
                table.Rows.Add(book.Publisher, book.Title, book.AuthorLastName, book.AuthorFirstName, book.Price);
            }

            // Open a new SQL connection
            using (SqlConnection connection = new SqlConnection("connectionString"))
            {
                connection.Open();

                // Create a new SQL transaction
                using (SqlTransaction transaction = connection.BeginTransaction())
                {
                    try
                    {
                        // Create a new SQL command and set the transaction and connection
                        using (SqlCommand command = new SqlCommand())
                        {
                            command.Connection = connection;
                            command.Transaction = transaction;

                            command.CommandText = "INSERT INTO Books (Publisher, Title, AuthorLastName, AuthorFirstName, Price) SELECT * FROM @Books";
                            command.Parameters.AddWithValue("@Books", table);

                            command.ExecuteNonQuery();
                            transaction.Commit();
                        }
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        throw ex;
                    }
                }
            }

        }        
    }
}