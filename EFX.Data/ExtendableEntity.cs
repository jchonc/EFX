using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using System.Dynamic;
using Morcatko.AspNetCore.JsonMergePatch;

namespace EFX.Data
{
    public class ValidationError : ApplicationException
    {
        public ValidationError(string message) : base(message) { }
    }

    public class ExtendableEntity
    {
        public string DetailsJson { get; set; }

        public bool ShouldSerializeDetailsJson()
        {
            return false;
        }

        private ExpandoObject _details;
        public dynamic Details
        {
            get
            {
                if (_details == null)
                {
                    if (!String.IsNullOrEmpty(DetailsJson))
                    {
                        _details = Newtonsoft.Json.JsonConvert.DeserializeObject<ExpandoObject>(DetailsJson);
                    }
                    else
                    {
                        _details = new ExpandoObject();
                    }
                }
                return (dynamic)_details;
            }
        }

        public string AsJson()
        {
            //Pretent the list holds disallowed attributes
            string[] forbiddenAttrNames = new string[2] { "bankAccount", "TenantId" };
            return Newtonsoft.Json.JsonConvert.SerializeObject(this, Newtonsoft.Json.Formatting.Indented,
                new Newtonsoft.Json.JsonSerializerSettings { ContractResolver = new SafeJsonContractResolver(forbiddenAttrNames) });
        }

        public void SaveDetails()
        {
            DetailsJson = Newtonsoft.Json.JsonConvert.SerializeObject(_details);
        }

        public void ApplyPatch(JsonMergePatchDocument patch)
        {
            //This patch is the log too, if you think of it.
            //Pretent the list holds disallowed attributes
            string[] forbiddenAttrNames = new string[2] { "bankAccount", "TenantId" };
            patch.ContractResolver = new SafeJsonContractResolver(forbiddenAttrNames);
            //Casting generic to local concrete class at runtime is not very easy
            Type thisPatchType = typeof(JsonMergePatchDocument<>).MakeGenericType(this.GetType());
            thisPatchType.GetMethod("ApplyTo").Invoke(patch, new object[] { this });
            SaveDetails();
        }

        public void Validate()
        {
            int moduleId = 1;
            string entityName = "Incident";

            var metaInfo = FetchMetaTree(moduleId, entityName);



        }


    }
}
