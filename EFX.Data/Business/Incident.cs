using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;

namespace EFX.Data.Business
{
    public partial class Incident: ExtendableEntity
    {
        public Guid Id { get; set; }
        public Guid TenantId { get; set; }
        public Guid? LocationId { get; set; }
        public string FileId { get; set; }
        public string State { get; set; }
        public string GeneralIncidentType { get; set; }
        public string SpecificIncidentType { get; set; }
        public string EventSeverity { get; set; }
        public string EventDescription { get; set; }
        public string CreatedBy { get; set; }
        public DateTimeOffset? CreatedOn { get; set; }
        public string LastModifiedBy { get; set; }
        public DateTimeOffset? LastModifiedOn { get; set; }
    }
}
