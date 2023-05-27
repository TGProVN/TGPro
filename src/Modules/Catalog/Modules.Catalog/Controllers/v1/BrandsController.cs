using Microsoft.AspNetCore.Mvc;

namespace Modules.Catalog.Controllers.v1;

public class BrandsController : CatalogControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetAllAsync()
    {
        return Ok();
    }
}
