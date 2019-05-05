using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace Nerds.Library.Business
{
    /// <summary>
    /// Controlling the business.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class ReservationController : ControllerBase
    {
        private readonly Organization organization;

        public ReservationController(Organization organization)
        {
            this.organization = organization;
        }

        /// <summary>
        /// Gets the list of all available books in this library, grouped by template/business.
        /// </summary>
        /// <remarks>Satisfies requirement 1; interpreting 'available' as 'not reserved'.</remarks>
        /// <returns>The owned books.</returns>
        [HttpGet]
        public ActionResult<IEnumerable<BusinessDetailsDTO>> GetAll()
        {
            return Ok(organization.BookBusinesses.Select(BusinessDetailsDTO.FromBook));
        }

        [HttpPost("{uniqueBarcode}")]
        public ActionResult<Reservation> ReserveBook(string uniqueBarcode, Guid customerId)
        {
            // Find the book template
            var book = organization.OwnedBooks.FirstOrDefault(b => string.Equals(b.UniqueBarcode, uniqueBarcode, StringComparison.OrdinalIgnoreCase));
            if (book == null)
            {
                return NotFound("Book with unique bar code: " + uniqueBarcode);
            }

            // Find the business
            var business = organization.BookBusinesses.FirstOrDefault(b => b.BookTemplate == book.Template);
            Debug.Assert(business != null, "business != null");

            // Find the customer
            var customer = organization.Customers.FirstOrDefault(c => c.Id == customerId);
            if (customer == null)
            {
                return NotFound("Customer: " + customer);
            }

            var reservation = business.ReserveBook(book, customer);

            // TODO: transform to DTO
            return reservation;
        }

        // TODO:
        // - Get reservable available books (req 1)
        // - Get reservations for customer (that are expiring soon) (req 4)
        // - Reserve book (req 3)
        // - Return book (req 5, req 9)
    }
}
