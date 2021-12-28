using Microsoft.AspNetCore.Mvc;
using ParcellBackend.Data.Models;
using ParcellBackend.Data.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ParcellBackend.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class HomePageController : ControllerBase
    {
        private readonly HomePageServiceRepository homePageService;

        public HomePageController(HomePageServiceRepository homePageService)
        {
            this.homePageService = homePageService;
        }

        [HttpGet]
        public async Task<List<HomePage>> GetHomePages()
        {
            var homePageList = await homePageService.GetList();

            return homePageList;
        }

        [HttpGet("{id:length(24)}")]
        public async Task<ActionResult<HomePage>> GetHomePage(string id)
        {
            var homePage = await homePageService.Get(id);


            if (homePage is null)
            {
                return NotFound();
            }

            return homePage;
        }


    }
}
