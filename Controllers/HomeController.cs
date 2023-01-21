using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using System;

namespace ApiCache.Controllers
{
    [Route("/")]
    public class HomeController : Controller
    {
        private readonly IMemoryCache _memCache;

        public HomeController(IMemoryCache memCache)
        {
            _memCache = memCache;
        }

        public IActionResult Index()
        {

            DateTime currentTime;
            bool AlreadyExists = _memCache.TryGetValue("currentTime", out currentTime);
            if (!AlreadyExists)
            {
                currentTime = DateTime.Now;
                var cacheEntryOptions = new MemoryCacheEntryOptions()
                    .SetSlidingExpiration(TimeSpan.FromSeconds(10));
                _memCache.Set("currentTime", currentTime, cacheEntryOptions);
            }


            return View(currentTime);
        }
    }
}
