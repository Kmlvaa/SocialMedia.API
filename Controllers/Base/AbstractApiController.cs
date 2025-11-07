using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SocialMediaAPİ.DB;

namespace SocialMediaAPİ.Controllers.Base
{
    [ApiController]
    [Authorize]
    [Route("api/[controller]")]
    public class AbstractApiController<TController> : ControllerBase where TController : class
    {
        protected readonly IServiceProvider ServiceProvider;
        private ILogger<TController> _logger;
        private IMapper _autoMApper;
        private AppDbContext _appDbContext;

        protected IMapper AutoMapper => _autoMApper ??= ServiceProvider.GetRequiredService<IMapper>();
        protected ILogger Logger => _logger ??= ServiceProvider.GetRequiredService<ILogger<TController>>();
        protected AppDbContext AppDbContext => _appDbContext ??= ServiceProvider.GetRequiredService<AppDbContext>();

        public AbstractApiController(IServiceProvider serviceProvider)
        {
            ServiceProvider = serviceProvider;
        }



    }
}
