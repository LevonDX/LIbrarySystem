using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibrarySystem.Data.Entities
{
	public class Book : EntityBase
	{
		public string Title { get; set; }
		public int? Year { get; set; }

		public Book()
		{
			this.Title = "Unknown";
		}

		public int? AuthorID { get; set; }
		private Author? _author;
		public Author? Author
		{
			get { return _author;}

			set
			{
				if (value != null)
				{
					this._author = value;
					this.AuthorID = value.ID;
				}
			}
		}

		override public string ToString()
		{
			return $"{this.Title} {this.Year}";
		}
	}
}