using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace Nerds.Library.Business
{
    /// <summary>
    /// Controlling the business.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class BusinessController : ControllerBase
    {
        private readonly Organization organization;

        public BusinessController(Organization organization)
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

        // TODO:
        // - Get reservable available books (req 1)
        // - Get reservations for customer (that are expiring soon) (req 4)
        // - Reserve book (req 3)
        // - Return book (req 5, req 9)
    }
}
