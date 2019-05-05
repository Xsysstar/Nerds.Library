using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace Nerds.Library.Business
{
    /// <summary>
    /// Controlling the business.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly Organization organization;

        public CustomerController(Organization organization)
        {
            this.organization = organization;
        }

        /// <summary>
        /// Gets the list of all available books in this library, grouped by template/business.
        /// </summary>
        /// <remarks>Satisfies requirement 1; interpreting 'available' as 'not reserved'.</remarks>
        /// <returns>The owned books.</returns>
        [HttpGet]
        public ActionResult<IEnumerable<Customer>> GetAll()
        {
            // TODO: transform to DTO
            //return Ok(organization.Customers.Select(CustomerDTO.FromCustomer));
            return Ok(organization.Customers);
        }
    }
}
