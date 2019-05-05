using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Nerds.Library.Books
{
    /// <summary>
    /// Controlling books.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private readonly Organization organization;

        public BooksController(Organization organization)
        {
            this.organization = organization;
        }

        /// <summary>
        /// Gets the list of all available books in this library.
        /// </summary>
        /// <remarks>
        /// Satisfies requirement 1; interpreting 'available' as 'owned' (independent of current
        /// reservation status).
        /// </remarks>
        /// <returns>The owned books.</returns>
        [HttpGet]
        public ActionResult<IEnumerable<BookDetailsDTO>> Get()
        {
            return Ok(organization.OwnedBooks.Select(BookDetailsDTO.FromBook));
        }

        /// <summary>
        /// Gets all details for a specific book.
        /// </summary>
        /// <remarks>
        /// Satisfies requirement 2; using <paramref name="uniqueBarcode"/> as selector so that it is
        /// useful in physical settings.
        /// </remarks>
        /// <returns>The <see cref="BookDetailsDTO"/>.</returns>
        [HttpGet("{uniqueBarcode}")]
        public ActionResult<BookDetailsDTO> GetDetails(string uniqueBarcode)
        {
            var book = organization.OwnedBooks.FirstOrDefault(b => string.Equals(b.UniqueBarcode, uniqueBarcode, StringComparison.OrdinalIgnoreCase));
            if (book == null)
            {
                return NotFound(uniqueBarcode);
            }

            var result = BookDetailsDTO.FromBook(book);
            return Ok(result);
        }
    }
}
