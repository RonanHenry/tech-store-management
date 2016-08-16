using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Data.Entity;
using System.Threading.Tasks;
using System.Web.Http;
using TechStoreLibrary.Models;
using TechStoreWeb.Controllers.Base;

namespace TechStoreWeb.Controllers
{
    /// <summary>
    /// Manages the workers.
    /// </summary>
    public class WorkersController : BaseController
    {
        #region Routes
        /// <summary>
        /// Retrieves all the workers from the API remote database.
        /// </summary>
        /// <returns>The list of workers.</returns>
        [HttpGet]
        [Route("api/workers")]
        public async Task<IEnumerable<Worker>> GetAllAsync()
        {
            return await DataResource.DbSetWorkers.Include(w => w.Address).ToListAsync();
        }

        /// <summary>
        /// Retrieves a worker by its Id.
        /// </summary>
        /// <param name="id">Id of the worker to retrieve.</param>
        /// <returns>The worker.</returns>
        [HttpGet]
        [Route("api/worker/{id}")]
        public async Task<Worker> GetAsync(int id)
        {
            return await DataResource.DbSetWorkers.FindAsync(id) as Worker;
        }

        /// <summary>
        /// Adds the worker in the API remote database.
        /// </summary>
        /// <param name="worker">The worker to insert.</param>
        /// <returns>The HTTP status code.</returns>
        [HttpPost]
        [Route("api/worker")]
        public async Task<IHttpActionResult> PostAsync(Worker worker)
        {
            DataResource.DbSetWorkers.Add(worker);
            return Ok(await DataResource.SaveChangesAsync());
        }

        /// <summary>
        /// Updates the worker in the API remote database.
        /// </summary>
        /// <param name="worker">The worker to update.</param>
        /// <returns>The HTTP status code.</returns>
        [HttpPut]
        [Route("api/worker")]
        public async Task<IHttpActionResult> PutAsync(Worker worker)
        {
            DataResource.Entry(worker).State = EntityState.Modified;
            DataResource.Entry(worker.Address).State = EntityState.Modified;
            return Ok(await DataResource.SaveChangesAsync());
        }

        /// <summary>
        /// Deletes the worker from the API remote database.
        /// </summary>
        /// <param name="worker">The worker to delete.</param>
        /// <returns>The HTTP status code.</returns>
        [HttpDelete]
        [Route("api/worker")]
        public async Task<IHttpActionResult> DeleteAsync(Worker worker)
        {
            DataResource.DbSetWorkers.Attach(worker);
            DataResource.DbSetWorkers.Remove(worker);
            return Ok(await DataResource.SaveChangesAsync());
        }
        #endregion
    }
}
