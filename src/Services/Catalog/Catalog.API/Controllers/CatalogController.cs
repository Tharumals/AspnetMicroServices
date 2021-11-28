using Catalog.API.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Catalog.API.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class CatalogController : ControllerBase
    {
        private readonly IProductRepositiry _repositiry;
        private readonly ILogger<CatalogController> _logger;

        public CatalogController(IProductRepositiry repositiry, ILogger<CatalogController> logger)
        {
            _repositiry = repositiry;
            _logger = logger;
        }
    }
}
