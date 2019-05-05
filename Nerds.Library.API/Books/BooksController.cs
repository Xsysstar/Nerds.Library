using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

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
        /// Gets the list of all available books in this library
        /// </summary>
        /// <remarks>Requirement 1.</remarks>
        /// <returns>The owned books.</returns>
        [HttpGet]
        public ActionResult<IEnumerable<Book>> Get()
        {
            return new ActionResult<IEnumerable<Book>>(organization.OwnedBooks);
        }
    }
}
