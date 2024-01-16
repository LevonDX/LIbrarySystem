using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibrarySystem.Data.Entities
{
	public class Author : EntityBase
	{
        public Author()
        {
            this.Name = "Unknown";
            this.Books = new List<Book>();
        }

		public string Name { get; set; }
		public string? Surname { get; set; }

        public List<Book> Books { get; set; }
    }
}