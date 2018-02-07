using EFX.Data.Business;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.JsonPatch.Operations;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;

namespace EFX.Tests
{
    [TestClass]
    public class IncidentCURD
    {
        const string defaultTenantId = "858EE4F5-89AF-4A42-90BE-63021F866B45";
        const string firstIncidentId = "22C0745E-94D4-4FBA-B9AE-2932C4BD4D10";

        [TestMethod]
        public void CanFetchJson()
        {
            using (var context = new BusinessContext())
            {
                DateTimeOffset thisMoment = DateTimeOffset.UtcNow;
                var newInc = new Incident
                {
                    Id = Guid.NewGuid(),
                    TenantId = Guid.Parse(defaultTenantId),
                    FileId = "First Something",
                    CreatedBy = "Jian",
                    CreatedOn = thisMoment,
                    LastModifiedBy = "Jian",
                    LastModifiedOn = thisMoment,
                    EventSeverity = "High",
                    EventDescription = "Nothing happened",
                    GeneralIncidentType = "Fall",
                    SpecificIncidentType = "Fall From Bed",
                    State = "New",
                    DetailsJson = @"{
    medicationInvolved: true,
    medicationOrdered: [{
        name: 'Pills',
        dosage: '200mg'
    }]
}"
                };
                context.Incidents.Add(newInc);
                context.SaveChanges(true);
            }
        }

        [TestMethod]
        public void CanChangeFields()
        {
            using (var context = new BusinessContext())
            {
                DateTimeOffset thisMoment = DateTimeOffset.UtcNow;
                var firstInc = context.Incidents.Find(Guid.Parse(firstIncidentId));
                Assert.IsNotNull(firstInc);

                firstInc.EventSeverity = "Low";
                firstInc.LastModifiedBy = "Alex";
                firstInc.LastModifiedOn = thisMoment;

                firstInc.Details.injured = true;
                firstInc.Details.injuryNatures = "Some simple value";

                firstInc.SaveDetails();

                context.SaveChanges(true);
            }
        }

        [TestMethod]
        public void CanChangeViaPath()
        {
            using (var context = new BusinessContext())
            {
                DateTimeOffset thisMoment = DateTimeOffset.UtcNow;
                var firstInc = context.Incidents.Find(Guid.Parse(firstIncidentId));
                Assert.IsNotNull(firstInc);

                List<Operation<Incident>> changes = new List<Operation<Incident>>
                {
                    new Operation<Incident> { op = "replace", path = "EventDescription", value = "SV1" }
                };

                JsonPatchDocument<Incident> patch = new JsonPatchDocument<Incident>(changes, new EFX.Data.SafeJsonContractResolver());
                patch.ApplyTo(firstInc);

                Assert.AreEqual(firstInc.EventDescription, "SV1");

                context.SaveChanges(true);
            }
        }

    }
}

