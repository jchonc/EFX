using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using EFX.Data.Business;
using Microsoft.AspNetCore.Mvc;
using Morcatko.AspNetCore.JsonMergePatch;
using Newtonsoft.Json.Linq;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace EFX.Web
{
    public class IncidentsController : Controller
    {
        [HttpGet]
        [Route("api/incidents/{id}")]
        public object GetIncident([FromQuery] string id)
        {
            const string firstIncidentId = "22C0745E-94D4-4FBA-B9AE-2932C4BD4D10";
            using (var context = new BusinessContext())
            {
                var firstInc = context.Incidents.Find(Guid.Parse(firstIncidentId));
                if (firstInc != null)
                {
                    return Content(firstInc.AsJson(), "application/json");
                }
                else
                {
                    return NotFound($"Incident ${id} not found");
                }
            }
        }

        [HttpPatch]
        [Route("api/incidents/{id}")]
        [Consumes(JsonMergePatchDocument.ContentType)]
        public object PutIncident([FromQuery] string id, [FromBody]JsonMergePatchDocument<Incident> patch)
        {
            const string firstIncidentId = "22C0745E-94D4-4FBA-B9AE-2932C4BD4D10";
            using (var context = new BusinessContext())
            {
                var firstInc = context.Incidents.Find(Guid.Parse(firstIncidentId));
                if (firstInc != null)
                {
                    try
                    {
                        firstInc.ApplyPatch(patch);
                        firstInc.Validate();
                        context.SaveChanges(true);
                    }
                    catch(Exception ecpt)
                    {
                        return BadRequest(ecpt.Message);
                    }
                }
                else
                {
                    return NotFound($"Incident ${id} not found");
                }
            }
            return Ok();
        }
    }
}
