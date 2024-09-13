using BooksWebAPI.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using System.Data.SqlClient;
using static System.Reflection.Metadata.BlobBuilder;

namespace BooksWebAPI
{
    public class DBConnection
    {
        public List<Book> GetAllBooks()
        {
            List<Book> books = new List<Book>();

            var cmd = GetSqlCommand();

            cmd.CommandText = "SELECT * FROM Books";

            var reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                var book = new Book()
                {
                    Id = int.Parse(reader["Id"].ToString()),
                    Title = reader["Title"].ToString(),
                    Pages = int.Parse(reader["Pages"].ToString()),
                };

                books.Add(book);
            }

            return books;
        }

        public Book GetBookById(int id)
        {
            var cmd = GetSqlCommand();
            cmd.CommandText = "SELECT * FROM Books WHERE Id = @id";
            cmd.Parameters.AddWithValue("id", id);

            var reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                var book = new Book()
                {
                    Id = int.Parse(reader["Id"].ToString()),
                    Title = reader["Title"].ToString(),
                    Pages = int.Parse(reader["Pages"].ToString()),
                };

                return book;
            }

            return null;
        }

        public void SaveBook(Book book)
        {
            var cmd = GetSqlCommand();

            cmd.CommandText = "INSERT INTO Books (Title, Pages) VALUES (@title, @pages)";

            cmd.Parameters.AddWithValue("title", book.Title);
            cmd.Parameters.AddWithValue("pages", book.Pages);

            cmd.ExecuteNonQuery();
        }

        public void UpdateBook(int id, Book book)
        {
            var cmd = GetSqlCommand();
            cmd.CommandText = "UPDATE Books SET Title = @title, Pages = @pages WHERE Id = @id";

            cmd.Parameters.AddWithValue("title", book.Title);
            cmd.Parameters.AddWithValue("pages", book.Pages);
            cmd.Parameters.AddWithValue("id", id);

            cmd.ExecuteNonQuery();
        }

        public void DeleteBookById(int id)
        {
            var cmd = GetSqlCommand();
            cmd.CommandText = "DELETE FROM Books WHERE Id = @id";

            cmd.Parameters.AddWithValue("id", id);

            cmd.ExecuteNonQuery();
        }

        private SqlCommand GetSqlCommand()
        {
            string connectionString = "Data Source=localhost;Initial Catalog=BooksDB;Integrated Security=True;Encrypt=False";
            SqlConnection conn = new SqlConnection(connectionString);
            conn.Open();

            SqlCommand cmd = conn.CreateCommand();

            cmd.CommandType = System.Data.CommandType.Text;

            return cmd;
        }

        public List <Loan> GetAllLoans()
        {
            List<Loan> loans = new List<Loan>();

            var cmd = GetSqlCommand();

            cmd.CommandText = "SELECT * FROM Loan2";

            var reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                var loan = new Loan()
                {
                    Id = int.Parse(reader["Id"].ToString()),
                    BookId = int.Parse(reader["BookId"].ToString()),
                    StartDate = reader.GetDateTime(reader.GetOrdinal("StartDate")),
                };
                loans.Add(loan);
            }

            return loans;
        }

        public Loan GetLoanById(int id)
        {
            var cmd = GetSqlCommand();
            cmd.CommandText = "SELECT * FROM Loan2 WHERE Id = @id";
            cmd.Parameters.AddWithValue("id", id);

            var reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                var loan = new Loan()
                {
                    Id = int.Parse(reader["Id"].ToString()),
                    BookId = int.Parse(reader["BookId"].ToString()),
                    StartDate = reader.GetDateTime(reader.GetOrdinal("StartDate")),
                };

                return loan;
            }

            return null;
        }

        public void SaveLoan(Loan loan)
        {
            var cmd = GetSqlCommand();

            cmd.CommandText = "INSERT INTO Loan2 (BookId, StartDate) VALUES (@bookId, @startDate)";

            cmd.Parameters.AddWithValue("bookId", loan.BookId);
            cmd.Parameters.AddWithValue("startDate", loan.StartDate);

            cmd.ExecuteNonQuery();
        }

        public void UpdateLoan(int id, Loan loan)
        {
            var cmd = GetSqlCommand();
            cmd.CommandText = "UPDATE Loan2 SET BookId = @bookId, StartDate = @startDate WHERE Id = @id";

            cmd.Parameters.AddWithValue("bookId", loan.BookId);
            cmd.Parameters.AddWithValue("startDate", loan.StartDate);
            cmd.Parameters.AddWithValue("id", id);

            cmd.ExecuteNonQuery();
        }

        public void DeleteLoanById(int id)
        {
            var cmd = GetSqlCommand();
            cmd.CommandText = "DELETE FROM Loan2 WHERE ID = @id";

            cmd.Parameters.AddWithValue("id", id);

            cmd.ExecuteNonQuery();
        }

        // Method to check if the BookId exists in the Books table
        public bool Bookexists(int bookId)
        {
            var cmd = GetSqlCommand();
            cmd.CommandText = "SELECT COUNT(1) FROM Books WHERE Id = @id";

            cmd.Parameters.AddWithValue("id", bookId);

            var count = Convert.ToInt32(cmd.ExecuteScalar());
            return count > 0;
        }
    }
}
