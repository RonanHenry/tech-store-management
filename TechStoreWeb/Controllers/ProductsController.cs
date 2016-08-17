using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using TechStoreLibrary.Models;
using TechStoreWeb.Controllers.Base;

namespace TechStoreWeb.Controllers
{
    /// <summary>
    /// Manages the products.
    /// </summary>
    public class ProductsController : BaseController
    {
        #region CPU Routes
        /// <summary>
        /// Retrieves all the CPUs from the API remote database.
        /// </summary>
        /// <returns>The list of CPU products.</returns>
        [HttpGet]
        [Route("api/cpus")]
        public async Task<IEnumerable<CPU>> GetAllCPUsAsync()
        {
            if (HttpContext.Current.Request.QueryString["stock"] != null)
            {
                return await DataResource.DbSetCPUs.Where(cpu => cpu.Stock > 0).ToListAsync();
            }
            else
            {
                return await DataResource.DbSetCPUs.ToListAsync();
            }
        }

        /// <summary>
        /// Retrieves a CPU product by its id.
        /// </summary>
        /// <param name="id">Id of the CPU product to retrieve.</param>
        /// <returns>The CPU product.</returns>
        [HttpGet]
        [Route("api/cpu/{id}")]
        public async Task<CPU> GetCPUAsync(int id)
        {
            return await DataResource.DbSetCPUs.FindAsync(id) as CPU;
        }

        /// <summary>
        /// Adds a CPU product in the API remote database.
        /// </summary>
        /// <param name="cpu">The CPU product to insert.</param>
        /// <returns>The HTTP status code.</returns>
        [HttpPost]
        [Route("api/cpu")]
        public async Task<IHttpActionResult> PostCPUAsync(CPU cpu)
        {
            DataResource.DbSetCPUs.Add(cpu);
            return Ok(await DataResource.SaveChangesAsync());
        }

        /// <summary>
        /// Updates a CPU product in the API remote database.
        /// </summary>
        /// <param name="cpu">The CPU product to update.</param>
        /// <returns>The HTTP status code.</returns>
        [HttpPut]
        [Route("api/cpu")]
        public async Task<IHttpActionResult> PutCPUAsync(CPU cpu)
        {
            DataResource.Entry(cpu).State = EntityState.Modified;
            return Ok(await DataResource.SaveChangesAsync());
        }

        /// <summary>
        /// Deletes a CPU product from the API remote database.
        /// </summary>
        /// <param name="cpu">The CPU product to delete.</param>
        /// <returns>The HTTP status code.</returns>
        [HttpDelete]
        [Route("api/cpu")]
        public async Task<IHttpActionResult> DeleteCPUAsync(CPU cpu)
        {
            DataResource.DbSetCPUs.Attach(cpu);
            DataResource.DbSetCPUs.Remove(cpu);
            return Ok(await DataResource.SaveChangesAsync());
        }
        #endregion

        #region GPU Routes
        /// <summary>
        /// Retrieves all the GPUs from the API remote database.
        /// </summary>
        /// <returns>The list of GPU products.</returns>
        [HttpGet]
        [Route("api/gpus")]
        public async Task<IEnumerable<GPU>> GetAllGPUsAsync()
        {
            if (HttpContext.Current.Request.QueryString["stock"] != null)
            {
                return await DataResource.DbSetGPUs.Where(gpu => gpu.Stock > 0).ToListAsync();
            }
            else
            {
                return await DataResource.DbSetGPUs.ToListAsync();
            }
            
        }

        /// <summary>
        /// Retrieves a GPU product by its id.
        /// </summary>
        /// <param name="id">Id of the GPU product to retrieve.</param>
        /// <returns>The GPU product.</returns>
        [HttpGet]
        [Route("api/gpu/{id}")]
        public async Task<GPU> GetGPUAsync(int id)
        {
            return await DataResource.DbSetGPUs.FindAsync(id) as GPU;
        }

        /// <summary>
        /// Adds a GPU product in the API remote database.
        /// </summary>
        /// <param name="gpu">The GPU product to insert.</param>
        /// <returns>The HTTP status code.</returns>
        [HttpPost]
        [Route("api/gpu")]
        public async Task<IHttpActionResult> PostGPUAsync(GPU gpu)
        {
            DataResource.DbSetGPUs.Add(gpu);
            return Ok(await DataResource.SaveChangesAsync());
        }

        /// <summary>
        /// Updates a GPU product in the API remote database.
        /// </summary>
        /// <param name="gpu">The GPU product to update.</param>
        /// <returns>The HTTP status code.</returns>
        [HttpPut]
        [Route("api/gpu")]
        public async Task<IHttpActionResult> PutGPUAsync(GPU gpu)
        {
            DataResource.Entry(gpu).State = EntityState.Modified;
            return Ok(await DataResource.SaveChangesAsync());
        }

        /// <summary>
        /// Deletes a GPU product from the API remote database.
        /// </summary>
        /// <param name="gpu">The GPU product to delete.</param>
        /// <returns>The HTTP status code.</returns>
        [HttpDelete]
        [Route("api/gpu")]
        public async Task<IHttpActionResult> DeleteGPUAsync(GPU gpu)
        {
            DataResource.DbSetGPUs.Attach(gpu);
            DataResource.DbSetGPUs.Remove(gpu);
            return Ok(await DataResource.SaveChangesAsync());
        }
        #endregion

        #region Motherboard Routes
        /// <summary>
        /// Retrieves all the Motherboards from the API remote database.
        /// </summary>
        /// <returns>The list of Motherboard products.</returns>
        [HttpGet]
        [Route("api/motherboards")]
        public async Task<IEnumerable<Motherboard>> GetAllMotherboardsAsync()
        {
            if (HttpContext.Current.Request.QueryString["stock"] != null)
            {
                return await DataResource.DbSetMotherboards.Where(mb => mb.Stock > 0).ToListAsync();
            }
            else
            {
                return await DataResource.DbSetMotherboards.ToListAsync();
            }
            
        }

        /// <summary>
        /// Retrieves a Motherboard product by its id.
        /// </summary>
        /// <param name="id">Id of the Motherboard product to retrieve.</param>
        /// <returns>The Motherboard product.</returns>
        [HttpGet]
        [Route("api/motherboard/{id}")]
        public async Task<Motherboard> GetMotherboardAsync(int id)
        {
            return await DataResource.DbSetMotherboards.FindAsync(id) as Motherboard;
        }

        /// <summary>
        /// Adds a Motherboard product in the API remote database.
        /// </summary>
        /// <param name="motherboard">The Motherboard product to insert.</param>
        /// <returns>The HTTP status code.</returns>
        [HttpPost]
        [Route("api/motherboard")]
        public async Task<IHttpActionResult> PostMotherboardAsync(Motherboard motherboard)
        {
            DataResource.DbSetMotherboards.Add(motherboard);
            return Ok(await DataResource.SaveChangesAsync());
        }

        /// <summary>
        /// Updates a Motherboard product in the API remote database.
        /// </summary>
        /// <param name="motherboard">The Motherboards product to update.</param>
        /// <returns>The HTTP status code.</returns>
        [HttpPut]
        [Route("api/motherboard")]
        public async Task<IHttpActionResult> PutMotherboardAsync(Motherboard motherboard)
        {
            DataResource.Entry(motherboard).State = EntityState.Modified;
            return Ok(await DataResource.SaveChangesAsync());
        }

        /// <summary>
        /// Deletes a Motherboard product from the API remote database.
        /// </summary>
        /// <param name="motherboard">The Motherboard product to delete.</param>
        /// <returns>The HTTP status code.</returns>
        [HttpDelete]
        [Route("api/motherboard")]
        public async Task<IHttpActionResult> DeleteMotherboardAsync(Motherboard motherboard)
        {
            DataResource.DbSetMotherboards.Attach(motherboard);
            DataResource.DbSetMotherboards.Remove(motherboard);
            return Ok(await DataResource.SaveChangesAsync());
        }
        #endregion

        #region Memory Routes
        /// <summary>
        /// Retrieves all the Memory products from the API remote database.
        /// </summary>
        /// <returns>The list of Memory products.</returns>
        [HttpGet]
        [Route("api/memorys")]
        public async Task<IEnumerable<Memory>> GetAllMemoriesAsync()
        {
            if (HttpContext.Current.Request.QueryString["stock"] != null)
            {
                return await DataResource.DbSetMemories.Where(ram => ram.Stock > 0).ToListAsync();
            }
            else
            {
                return await DataResource.DbSetMemories.ToListAsync();
            }
            
        }

        /// <summary>
        /// Retrieves a Memory product by its id.
        /// </summary>
        /// <param name="id">Id of the Memory product to retrieve.</param>
        /// <returns>The Memory product.</returns>
        [HttpGet]
        [Route("api/memory/{id}")]
        public async Task<Memory> GetMemoryAsync(int id)
        {
            return await DataResource.DbSetMemories.FindAsync(id) as Memory;
        }

        /// <summary>
        /// Adds a Memory product in the API remote database.
        /// </summary>
        /// <param name="memory">The Memory product to insert.</param>
        /// <returns>The HTTP status code.</returns>
        [HttpPost]
        [Route("api/memory")]
        public async Task<IHttpActionResult> PostMemoryAsync(Memory memory)
        {
            DataResource.DbSetMemories.Add(memory);
            return Ok(await DataResource.SaveChangesAsync());
        }

        /// <summary>
        /// Updates a Memory product in the API remote database.
        /// </summary>
        /// <param name="memory">The Memory product to update.</param>
        /// <returns>The HTTP status code.</returns>
        [HttpPut]
        [Route("api/memory")]
        public async Task<IHttpActionResult> PutMemoryAsync(Memory memory)
        {
            DataResource.Entry(memory).State = EntityState.Modified;
            return Ok(await DataResource.SaveChangesAsync());
        }

        /// <summary>
        /// Deletes a Memory product from the API remote database.
        /// </summary>
        /// <param name="memory">The Memory product to delete.</param>
        /// <returns>The HTTP status code.</returns>
        [HttpDelete]
        [Route("api/memory")]
        public async Task<IHttpActionResult> DeleteMemoryAsync(Memory memory)
        {
            DataResource.DbSetMemories.Attach(memory);
            DataResource.DbSetMemories.Remove(memory);
            return Ok(await DataResource.SaveChangesAsync());
        }
        #endregion

        #region Storage Routes
        /// <summary>
        /// Retrieves all the Storage products from the API remote database.
        /// </summary>
        /// <returns>The list of Storage products.</returns>
        [HttpGet]
        [Route("api/storages")]
        public async Task<IEnumerable<Storage>> GetAllStoragesAsync()
        {
            if (HttpContext.Current.Request.QueryString["stock"] != null)
            {
                return await DataResource.DbSetStorages.Where(storage => storage.Stock > 0).ToListAsync();
            }
            else
            {
                return await DataResource.DbSetStorages.ToListAsync();
            }

        }

        /// <summary>
        /// Retrieves a Storage product by its id.
        /// </summary>
        /// <param name="id">Id of the Storage product to retrieve.</param>
        /// <returns>The Storage product.</returns>
        [HttpGet]
        [Route("api/storage/{id}")]
        public async Task<Storage> GetStorageAsync(int id)
        {
            return await DataResource.DbSetStorages.FindAsync(id) as Storage;
        }

        /// <summary>
        /// Adds a Storage product in the API remote database.
        /// </summary>
        /// <param name="storage">The Storage product to insert.</param>
        /// <returns>The HTTP status code.</returns>
        [HttpPost]
        [Route("api/storage")]
        public async Task<IHttpActionResult> PostStorageAsync(Storage storage)
        {
            DataResource.DbSetStorages.Add(storage);
            return Ok(await DataResource.SaveChangesAsync());
        }

        /// <summary>
        /// Updates a Storage product in the API remote database.
        /// </summary>
        /// <param name="storage">The Storage product to update.</param>
        /// <returns>The HTTP status code.</returns>
        [HttpPut]
        [Route("api/storage")]
        public async Task<IHttpActionResult> PutStorageAsync(Storage storage)
        {
            DataResource.Entry(storage).State = EntityState.Modified;
            return Ok(await DataResource.SaveChangesAsync());
        }

        /// <summary>
        /// Deletes a Storage product from the API remote database.
        /// </summary>
        /// <param name="storage">The Storage product to delete.</param>
        /// <returns>The HTTP status code.</returns>
        [HttpDelete]
        [Route("api/storage")]
        public async Task<IHttpActionResult> DeleteStorageAsync(Storage storage)
        {
            DataResource.DbSetStorages.Attach(storage);
            DataResource.DbSetStorages.Remove(storage);
            return Ok(await DataResource.SaveChangesAsync());
        }
        #endregion

        #region PSU Routes
        /// <summary>
        /// Retrieves all the PSU products from the API remote database.
        /// </summary>
        /// <returns>The list of PSU products.</returns>
        [HttpGet]
        [Route("api/psus")]
        public async Task<IEnumerable<PSU>> GetAllPSUsAsync()
        {
            if (HttpContext.Current.Request.QueryString["stock"] != null)
            {
                return await DataResource.DbSetPSUs.Where(psu => psu.Stock > 0).ToListAsync();
            }
            else
            {
                return await DataResource.DbSetPSUs.ToListAsync();
            }
        }

        /// <summary>
        /// Retrieves a PSU product by its id.
        /// </summary>
        /// <param name="id">Id of the PSU product to retrieve.</param>
        /// <returns>The PSU product.</returns>
        [HttpGet]
        [Route("api/psu/{id}")]
        public async Task<PSU> GetPSUAsync(int id)
        {
            return await DataResource.DbSetPSUs.FindAsync(id) as PSU;
        }

        /// <summary>
        /// Adds a PSU product in the API remote database.
        /// </summary>
        /// <param name="psu">The PSU product to insert.</param>
        /// <returns>The HTTP status code.</returns>
        [HttpPost]
        [Route("api/psu")]
        public async Task<IHttpActionResult> PostPSUAsync(PSU psu)
        {
            DataResource.DbSetPSUs.Add(psu);
            return Ok(await DataResource.SaveChangesAsync());
        }

        /// <summary>
        /// Updates a PSU product in the API remote database.
        /// </summary>
        /// <param name="psu">The PSU product to update.</param>
        /// <returns>The HTTP status code.</returns>
        [HttpPut]
        [Route("api/psu")]
        public async Task<IHttpActionResult> PutPSUAsync(PSU psu)
        {
            DataResource.Entry(psu).State = EntityState.Modified;
            return Ok(await DataResource.SaveChangesAsync());
        }

        /// <summary>
        /// Deletes a PSU product from the API remote database.
        /// </summary>
        /// <param name="psu">The PSU product to delete.</param>
        /// <returns>The HTTP status code.</returns>
        [HttpDelete]
        [Route("api/psu")]
        public async Task<IHttpActionResult> DeletePSUAsync(PSU psu)
        {
            DataResource.DbSetPSUs.Attach(psu);
            DataResource.DbSetPSUs.Remove(psu);
            return Ok(await DataResource.SaveChangesAsync());
        }
        #endregion

        #region Case Routes
        /// <summary>
        /// Retrieves all the Case products from the API remote database.
        /// </summary>
        /// <returns>The list of Case products.</returns>
        [HttpGet]
        [Route("api/cases")]
        public async Task<IEnumerable<Case>> GetAllCasesAsync()
        {
            if (HttpContext.Current.Request.QueryString["stock"] != null)
            {
                return await DataResource.DbSetCases.Where(pcCase => pcCase.Stock > 0).ToListAsync();
            }
            else
            {
                return await DataResource.DbSetCases.ToListAsync();
            }
            
        }

        /// <summary>
        /// Retrieves a Case product by its id.
        /// </summary>
        /// <param name="id">Id of the Case product to retrieve.</param>
        /// <returns>The Case product.</returns>
        [HttpGet]
        [Route("api/case/{id}")]
        public async Task<Case> GetCaseAsync(int id)
        {
            return await DataResource.DbSetCases.FindAsync(id) as Case;
        }

        /// <summary>
        /// Adds a Case product in the API remote database.
        /// </summary>
        /// <param name="pcCase">The Case product to insert.</param>
        /// <returns>The HTTP status code.</returns>
        [HttpPost]
        [Route("api/case")]
        public async Task<IHttpActionResult> PostCaseAsync(Case pcCase)
        {
            DataResource.DbSetCases.Add(pcCase);
            return Ok(await DataResource.SaveChangesAsync());
        }

        /// <summary>
        /// Updates a Case product in the API remote database.
        /// </summary>
        /// <param name="pcCase">The Case product to update.</param>
        /// <returns>The HTTP status code.</returns>
        [HttpPut]
        [Route("api/case")]
        public async Task<IHttpActionResult> PutCaseAsync(Case pcCase)
        {
            DataResource.Entry(pcCase).State = EntityState.Modified;
            return Ok(await DataResource.SaveChangesAsync());
        }

        /// <summary>
        /// Deletes a Case product from the API remote database.
        /// </summary>
        /// <param name="pcCase">The Case product to delete.</param>
        /// <returns>The HTTP status code.</returns>
        [HttpDelete]
        [Route("api/case")]
        public async Task<IHttpActionResult> DeleteCaseAsync(Case pcCase)
        {
            DataResource.DbSetCases.Attach(pcCase);
            DataResource.DbSetCases.Remove(pcCase);
            return Ok(await DataResource.SaveChangesAsync());
        }
        #endregion
    }
}
