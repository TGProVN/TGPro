﻿using Microsoft.AspNetCore.Mvc;

namespace Modules.Catalog.Controllers;

[ApiController]
//[Route("api/v{version:apiVersion}/[controller]")]
[Route("api/[controller]")]
internal abstract class BaseCatalogController : ControllerBase
{ }
