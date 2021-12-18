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
    public class PlanController : ControllerBase
    {
        private readonly PlanServiceRepository planService;

        public PlanController(PlanServiceRepository planService)
        {
            this.planService = planService;
        }

        [HttpGet]
        public async Task<List<Plan>> GetPlans()
        {
            var planList = await planService.GetList();

            return planList;
        }

        [HttpGet("{id:length(24)}")]
        public async Task<ActionResult<Plan>> GetPlan(string id)
        {
            var plan = await planService.Get(id);


            if (plan is null)
            {
                return NotFound();
            }

            return plan;
        }

        [HttpPost]
        public async Task<IActionResult> CreatePlan(Plan newPlan)
        {

            await planService.Create(newPlan);

            return CreatedAtAction(nameof(GetPlan), new { id = newPlan.Id }, newPlan);
        }

        [HttpPut("{id:length(24)}")]
        public async Task<IActionResult> UpdatePlan(string id, Plan updatedPlan)
        {
            var plan = await planService.Get(id);

            if (plan is null)
            {
                return NotFound();
            }

            updatedPlan.Id = plan.Id;

            await planService.Update(id, updatedPlan);

            return NoContent();
        }

        [HttpDelete("{id:length(24)}")]
        public async Task<IActionResult> DeletePlan(string id)
        {
            var plan = await planService.Get(id);

            if (plan is null)
            {
                return NotFound();
            }

            await planService.Delete(id);

            return NoContent();
        }
    }
}
