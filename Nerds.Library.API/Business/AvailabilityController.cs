using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Linq;

namespace Nerds.Library.Business
{
    /// <summary>
    /// Controlling the business.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class AvailabilityController : ControllerBase
    {
        private readonly Organization organization;

        public AvailabilityController(Organization organization)
        {
            this.organization = organization;
        }

        /// <summary>
        /// Gets all availability information per book in this library.
        /// </summary>
        /// <remarks>Satisfies requirement 1; interpreting 'available' as 'not reserved'.</remarks>
        /// <returns>The availability information on the books.</returns>
        [HttpGet]
        public ActionResult<IEnumerable<AvailabilityDetailsDTO>> GetAll()
        {
            return Ok(organization.BookBusinesses.SelectMany(b => b.GetBookAvailabilities(DateTimeOffset.UtcNow)).Select(AvailabilityDetailsDTO.FromAvailability));
        }

        /// <summary>
        /// Reserves the book instance with the specified barcode for the customer.
        /// </summary>
        /// <remarks>Satisfies requirement 3.</remarks>
        /// <param name="uniqueBarcode">The unique barcode on the book.</param>
        /// <param name="customerId">The customer id.</param>
        /// <returns>The <see cref="AvailabilityDetailsDTO"/>.</returns>
        [HttpPost("reserve/{uniqueBarcode}")]
        public ActionResult<AvailabilityDetailsDTO> ReserveBook(string uniqueBarcode, [Required] Guid customerId)
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

            var availability = new Availability { Book = book, Reservation = reservation };
            return AvailabilityDetailsDTO.FromAvailability(availability);
        }

        // TODO:
        // - Get reservations for customer (that are expiring soon) (req 4)
        // - Reserve book (req 3)
        // - Return book (req 5, req 9)
    }
}
