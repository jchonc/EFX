using System;
using System.Collections.Generic;

namespace EFX.Data.Business
{
    public partial class Patients
    {
        public Guid Id { get; set; }
        public Guid TenantId { get; set; }
        public string FirstName { get; set; }
        public string MiddleInitial { get; set; }
        public string LastName { get; set; }
        public DateTime? Dob { get; set; }
        public string Sex { get; set; }
        public string Race { get; set; }
        public string CountryCode { get; set; }
        public string HomePhone { get; set; }
    }
}
