using GameOfDronesContractsLayer.Contracts.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;

namespace GDWebApiLayer.Controllers
{
    [Route("api/move/")]
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class MoveController : ApiController
    {
        public readonly IAppService Appservice;

        public MoveController(IAppService AppService)
        {
            Appservice = AppService;
        }

        [HttpGet]
        [Route("api/move/moves")]
        public IHttpActionResult Get()
        {
            var _Data = Appservice.GetMoves();

            if (!_Data.Any())
            {
                return NotFound();
            }

            return Ok(_Data);
        }
    }
}
