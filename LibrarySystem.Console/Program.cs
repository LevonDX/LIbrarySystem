using LibrarySystem.Data.Entities;
using LibrarySystem.Data.Repository;

namespace LibrarySystem.ConsoleUI
{
	internal class Program
	{
		static void Main(string[] args)
		{
			BookRepository bookRepository = new BookRepository();

			var books = bookRepository.GetBooks();

			foreach (Book book in books)
			{
				Console.WriteLine(book);
			}

			Author author = new Author()
			{
				ID = 1,
				Name = "J.R.R.",
				Surname = "Tolkien"
			};

			Book b = new Book()
			{
				Title = "The Lord of the Rings - 3",
				Year = 1954,

				Author = author
			};

			bookRepository.Add(b);
		}
	}
}