﻿using GameOfDronesContractsLayer.Contracts.BusinessEntities;
using GameOfDronesContractsLayer.Contracts.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;

namespace GameOfDronesWebApiLayer.Controllers
{
    [Route("api/game/")]
    [EnableCors(origins: "http://localhost:4200", headers: "*", methods: "*")]
    public class GameController : ApiController
    {
        public readonly IAppService Appservice;

        public GameController(IAppService AppService)
        {
            Appservice = AppService;
        }

        [Route("api/game/statistics")]
        [HttpGet]
        public IHttpActionResult GetStatistics()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Please check the parameters");
            }

            var _Data = Appservice.GetStatistics();

            if (!_Data.Any())
            {
                return NotFound();
            }

            return Ok(_Data);
        }

        [Route("api/game/start")]
        [HttpPost]
        public IHttpActionResult StartNewGame(string playerOneName, string playerTwoName)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Please check the parameters");
            }

            var gameInstance = Appservice.StartNewGame(playerOneName, playerTwoName);

            if (gameInstance == null)
            {
                return NotFound();
            }

            return Ok(gameInstance);
        }

        [Route("api/game/setRound")]
        [HttpPost]
        public IHttpActionResult setRound([FromBody] RoundDataModel round)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Please check the parameters");
            }

            var gameInstance = Appservice.SetRound(round);

            if (gameInstance == null)
            {
                return NotFound();
            }

            return Ok(gameInstance);
        }
    }
}
