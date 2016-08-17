using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Data.Entity;
using System.Web.Http;
using TechStoreWeb.Controllers.Base;
using System.Threading.Tasks;
using TechStoreLibrary.Models;

namespace TechStoreWeb.Controllers
{
    /// <summary>
    /// Manages the customers.
    /// </summary>
    public class CustomersController : BaseController
    {
        #region Routes
        /// <summary>
        /// Retrieves all the customers from the API remote database.
        /// </summary>
        /// <returns>The list of customers.</returns>
        [HttpGet]
        [Route("api/customers")]
        public async Task<IEnumerable<Customer>> GetAllAsync()
        {
            return await DataResource.DbSetCustomers.Include(c => c.Address).ToListAsync();
        }

        /// <summary>
        /// Retrieves a customer by its Id.
        /// </summary>
        /// <param name="id">Id of the customer to retrieve.</param>
        /// <returns>The customer.</returns>
        [HttpGet]
        [Route("api/customer/{id}")]
        public async Task<Customer> GetAsync(int id)
        {
            return await DataResource.DbSetCustomers.FindAsync(id) as Customer;
        }

        /// <summary>
        /// Adds the customer in the API remote database.
        /// </summary>
        /// <param name="customer">The customer to insert.</param>
        /// <returns>The HTTP status code.</returns>
        [HttpPost]
        [Route("api/customer")]
        public async Task<IHttpActionResult> PostAsync(Customer customer)
        {
            DataResource.DbSetCustomers.Add(customer);
            return Ok(await DataResource.SaveChangesAsync());
        }

        /// <summary>
        /// Updates the customer in the API remote database.
        /// </summary>
        /// <param name="customer">The customer to update.</param>
        /// <returns>The HTTP status code.</returns>
        [HttpPut]
        [Route("api/customer")]
        public async Task<IHttpActionResult> PutAsync(Customer customer)
        {
            DataResource.Entry(customer).State = EntityState.Modified;
            DataResource.Entry(customer.Address).State = EntityState.Modified;
            return Ok(await DataResource.SaveChangesAsync());
        }

        /// <summary>
        /// Deletes the customer from the API remote database.
        /// </summary>
        /// <param name="customer">The customer to delete.</param>
        /// <returns>The HTTP status code.</returns>
        [HttpDelete]
        [Route("api/customer")]
        public async Task<IHttpActionResult> DeleteAsync(Customer customer)
        {
            DataResource.DbSetCustomers.Attach(customer);
            DataResource.DbSetCustomers.Remove(customer);
            return Ok(await DataResource.SaveChangesAsync());
        }
        #endregion
    }
}
