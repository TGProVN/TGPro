using Microsoft.AspNetCore.Mvc;

namespace Modules.Catalog.Controllers;

internal class BrandsController : BaseCatalogController
{
    [HttpGet]
    public async Task<IActionResult> GetAllAsync()
    {
        return Ok();
    }
}
