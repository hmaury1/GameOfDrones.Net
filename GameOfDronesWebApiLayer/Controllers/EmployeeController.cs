using GameOfDronesContractsLayer.Contracts.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using GameOfDronesWebApiLayer.Controllers.Filters;
using GameOfDronesDataAccessLayer.Implementations;

namespace GameOfDronesWebApiLayer.Controllers
{
    [Route("api/employee/")]
    public class EmployeeController : ApiController
    {
        public readonly IAppService Appservice;

        public EmployeeController(IAppService AppService)
        {
            Appservice = AppService;
        }
        
        [HttpGet]
        public IHttpActionResult GetEmployeeDataByName([FromUri] PagingModel pagingModel, string name)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Please check the parameters");
            }

            var empData = Appservice.GetPagedEmployeeDataByName(name, pagingModel.PageSize, pagingModel.PageNo).ToList();

            if (!empData.Any())
            {
                return NotFound();
            }

            return Ok(empData);
        }

        [Route("api/Employee/{id:int}")]
        [HttpGet]
        public IHttpActionResult GetEmployeeDataById(int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Please check the parameters");
            }

            var empData = Appservice.GetPagedEmployeeDataById(id, 1, 1).ToList();

            if (!empData.Any())
            {
                return NotFound();
            }

            return Ok(empData);
        }
    }
}
