using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using TechStoreLibrary.Models;
using TechStoreWeb.Controllers.Base;

namespace TechStoreWeb.Controllers
{
    /// <summary>
    /// Manages the carts.
    /// </summary>
    public class CartController : BaseController
    {
        #region Routes
        /// <summary>
        /// Adds a Cart in the API remote database.
        /// </summary>
        /// <param name="cart">The Cart to insert.</param>
        /// <returns>The HTTP status code.</returns>
        [HttpPost]
        [Route("api/cart")]
        public async Task<IHttpActionResult> PostCartAsync(Cart cart)
        {
            DataResource.Entry(cart.Customer).State = EntityState.Modified;
            foreach (var item in cart.Items)
            {
                DataResource.Entry(item.Product).State = EntityState.Modified;
            }
            DataResource.DbSetCarts.Add(cart);
            return Ok(await DataResource.SaveChangesAsync());
        }
        #endregion
    }
}
