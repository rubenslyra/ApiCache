using System;
using System.Buffers;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;

namespace ApiCache.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class HomeController : ControllerBase
    {
        private readonly IMemoryCache _memCache;

        public HomeController(IMemoryCache memCache)
        {
            _memCache = memCache;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            DateTime currentTime;
            bool AlreadyExists = _memCache.TryGetValue("currentTime", out currentTime);
            if(!AlreadyExists)
            {
                currentTime = DateTime.Now;
                var cacheEntryOptions = new MemoryCacheEntryOptions()
                    .SetSlidingExpiration(TimeSpan.FromSeconds(10));
                _memCache.Set("currentTime", currentTime, cacheEntryOptions);
            }
            
            return Ok($"{currentTime} | Api em execução!");
            
            /**
                var cacheKey = "cacheKey";
                var cacheEntry = await _memCache.GetOrCreateAsync(cacheKey, async entry =>
                {
                    entry.AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(10);
                    return await Task.FromResult("Hello World");
                });
            */            
        }


    }
}