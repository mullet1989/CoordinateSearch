using System.Collections.Generic;
using System.Text.Json;
using CoordinateSearchApi.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace CoordinateSearchApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SearchController : ControllerBase
    {
        private readonly ILogger<SearchController> _logger;
        private readonly SearchService _searchService;

        public SearchController(
            ILogger<SearchController> logger,
            SearchService searchService)
        {
            _logger = logger;
            _searchService = searchService;
        }

        [HttpGet]
        [Route("coordinates")]
        public IActionResult Coordinates(float latitude, float longitude)
        {
            return new JsonResult(_searchService.Search(latitude, longitude), new JsonSerializerOptions
            {
                WriteIndented = true
            });
        }

        [HttpGet]
        [Route("query")]
        public IActionResult Query(string term)
        {
            return new JsonResult(_searchService.Query(term), new JsonSerializerOptions
            {
                WriteIndented = true
            });
        }

        [HttpGet]
        [Route("radius")]
        public IActionResult Radius(float latitude, float longitude, float radius)
        {
            return new JsonResult(_searchService.Point(latitude, longitude, radius), new JsonSerializerOptions
            {
                WriteIndented = true
            });
        }
    }
}