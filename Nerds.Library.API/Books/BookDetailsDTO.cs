using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace Nerds.Library.Books
{
    public sealed class BookDetailsDTO
    {
        public Guid Id { get; set; }
        public string UniqueBarcode { get; set; }
        public DateTimeOffset? PublicationDate { get; set; }
        public string ISBN { get; set; }
        public string Publisher { get; set; }

        public string Title { get; set; }
        public IEnumerable<string> Authors { get; set; }
        public IEnumerable<string> Genres { get; set; }

        static internal BookDetailsDTO FromBook(Book book)
        {
            Debug.Assert(book != null, "book != null");
            Debug.Assert(book.Template != null, "book.Template != null", "Book.Id={0}", book.Id);
            return new BookDetailsDTO
            {
                Id = book.Id,
                UniqueBarcode = book.UniqueBarcode,
                PublicationDate = book.Template.Publication?.PublicationDate,
                ISBN = book.Template.Publication?.ISBN,
                Publisher = book.Template.Publication?.Publisher?.Name,
                Title = book.Template.Title?.Caption,
                Authors = book.Template.Authors?.Select(a => a.FullName)?.ToArray(),
                Genres = book.Template.Genres?.Select(a => a.Caption)?.ToArray()
            };
        }
    }
}
