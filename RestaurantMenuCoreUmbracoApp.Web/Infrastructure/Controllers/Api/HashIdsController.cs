using Microsoft.AspNetCore.Mvc;
using RestaurantMenuCoreUmbracoApp.Web.Infrastructure.Handlers;
using Umbraco.Cms.Web.Common.Controllers;

namespace RestaurantMenuCoreUmbracoApp.Web.Infrastructure.Controllers.Api
{
    [Route("api/v1/hashids/")]
    public class HashIdsController : UmbracoApiController
    {
        [Route("hashid/{id}")]
        public IActionResult IntToHashId(int id)
        {
            if(id < 0) return NotFound();
            var hashId = HashIdsHandler.IntToHashId(id);
            return Ok(hashId);
        }

        [Route("int/{hashId}")]
        public IActionResult HashIdToInt(string hashId)
        {
            if (string.IsNullOrEmpty(hashId)) return NotFound();
            var id = HashIdsHandler.HashIdToInt(hashId);
            if(id < 0) return NotFound();
            return Ok(id);
        }
    }
}
