using LibrarySystem.Data.Entities;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibrarySystem.Data.Repository
{
	public class BookRepository
	{
		public IEnumerable<Book> GetBooks()
		{
			string sql = "Select * from Book";

			using SqlConnection connection = new SqlConnection(Helper.ConnectionString);
			connection.Open();

			using SqlCommand sqlCommand = new SqlCommand(sql, connection);

			using SqlDataReader reader = sqlCommand.ExecuteReader();

			while (reader.Read())
			{
				Book book = new Book()
				{
					ID = reader.GetInt32(0),

					Title = reader["Title"].ToString()!,
					Year = reader["Year"] as int?
				};

				yield return book;
			}
		}

		public Book? GetBookByID(int id)
		{
			string sql = "Select * from Book where ID = @id";

			using SqlConnection connection = new SqlConnection(Helper.ConnectionString);
			connection.Open();

			using SqlCommand sqlCommand = new SqlCommand(sql, connection);
			sqlCommand.Parameters.AddWithValue("@id", id);

			using SqlDataReader reader = sqlCommand.ExecuteReader();

			if (reader.Read())
			{
				Book book = new Book()
				{
					ID = reader.GetInt32(0),

					Title = reader["Title"].ToString()!,
					Year = reader["Year"] as int?
				};

				return book;
			}

			return null;
		}

		public Book? GetBookByTitle(string title)
		{
			string sql = "Select * from Book where Title = @title";

			using SqlConnection connection = new SqlConnection(Helper.ConnectionString);
			connection.Open();

			using SqlCommand sqlCommand = new SqlCommand(sql, connection);
			sqlCommand.Parameters.AddWithValue("@title", title);

			using SqlDataReader reader = sqlCommand.ExecuteReader();

			if (reader.Read())
			{
				Book book = new Book()
				{
					ID = reader.GetInt32(0),

					Title = reader["Title"].ToString()!,
					Year = reader["Year"] as int?
				};

				return book;
			}

			return null;
		}

		public void Add(Book book)
		{
			using SqlConnection connection = new SqlConnection(Helper.ConnectionString);
			connection.Open();

			if (book.Author?.ID == 0)
			{
				// create author
				string addAuthorSQL = "Insert into Author ([Name], Surname) values (@name, @surname)";

				using SqlCommand sqlCommand = new SqlCommand(addAuthorSQL, connection);
				sqlCommand.Parameters.AddWithValue("@name", book.Author?.Name);
				sqlCommand.Parameters.AddWithValue("@surname", book.Author?.Surname ?? SqlString.Null);

				sqlCommand.ExecuteNonQuery();

				// get author id
				string getAuthorIDSQL = "Select @@Identity";
				using SqlCommand sqlCommand2 = new SqlCommand(getAuthorIDSQL, connection);

				int authorID = Convert.ToInt32(sqlCommand2.ExecuteScalar());

				book.AuthorID = authorID;
			}

			string insertBookSQL = "Insert into Book (Title, Year, AuthorID) values (@title, @year, @author)";
			using SqlCommand commmand3 = new SqlCommand(insertBookSQL, connection);
			commmand3.Parameters.AddWithValue("@title", book.Title);
			commmand3.Parameters.AddWithValue("@year", book.Year ?? SqlInt32.Null);
			commmand3.Parameters.AddWithValue("@author", book.AuthorID);

			commmand3.ExecuteNonQuery();
		}

		public void Delete(Book book)
		{
			string sql = "Delete from Book where ID = @id";

			using SqlConnection connection = new SqlConnection(Helper.ConnectionString);
			connection.Open();

			using SqlCommand sqlCommand = new SqlCommand(sql, connection);
			sqlCommand.Parameters.AddWithValue("@id", book.ID);

			sqlCommand.ExecuteNonQuery();
		}

		public void Update(Book book)
		{
			string sql = "Update Book set Title = @title, Year = @year where ID = @id";

			using SqlConnection connection = new SqlConnection(Helper.ConnectionString);
			connection.Open();

			using SqlCommand sqlCommand = new SqlCommand(sql, connection);
			sqlCommand.Parameters.AddWithValue("@id", book.ID);
			sqlCommand.Parameters.AddWithValue("@title", book.Title);
			sqlCommand.Parameters.AddWithValue("@year", book.Year ?? SqlInt32.Null);

			sqlCommand.ExecuteNonQuery();
		}
	}
}