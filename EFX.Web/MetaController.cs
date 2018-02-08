using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace EFX.Web
{
    public class MetaController : Controller
    {

        object dictionary = new[] {
            new {
                module = 1,
                name= "pandora",
                entities= new [] {
                    new {
                        name = "patient",
                        attributes= new [] {
                            new {
                                attrId= "11001",
                                attrCaption= "First Name",
                                attrDataType= "string",
                                attrCtrlType= "text",
                                attrCtrlParams= ""
                            }, new {
                                attrId= "11002",
                                attrCaption= "Last Name",
                                attrDataType= "string",
                                attrCtrlType= "text",
                                attrCtrlParams= ""
                            }, new {
                                attrId= "11003",
                                attrCaption= "Gender",
                                attrDataType= "string",
                                attrCtrlType= "picklist",
                                attrCtrlParams= "Gender"
                            }, new {
                                attrId= "11004",
                                attrCaption= "Birthday",
                                attrDataType= "date",
                                attrCtrlType= "date",
                                attrCtrlParams= ""
                            }, new {
                                attrId= "11005",
                                attrCaption= "Owner",
                                attrDataType= "user",
                                attrCtrlType= "lookup",
                                attrCtrlParams= "users"
                            }, new {
                                attrId= "11006",
                                attrCaption= "Age",
                                attrDataType= "number",
                                attrCtrlType= "number",
                                attrCtrlParams= ""
                            }
                        }
                    }
                }
            }
        };

        // GET: api/<controller>
        [HttpGet]
        [Route("dictionary/{mid}/{entityName}")]
        public object GetMeta(int mid, string entityName)
        {
            return dictionary;

        }


        [HttpGet]
        [Route("picklists")]
        public object GetAllLists()
        {
            var knownPickLists = new[] {
                new {
                    listName = "Gender",
                    items = new[] {
                        new { value= "GD_MALE", label= "Male", description= "Gentleman" },
                        new { value= "GD_FEMALE", label= "Female", description= "Lady" },
                        new { value= "GD_LONG", label= "This is a reaaaaaaaaaaally long option", description= "long" }
                    }
                }
            };
            return knownPickLists;
        }

        [HttpPost]
        [Route("picklists")]
        public object GetSomeLists([FromBody]string[] names)
        {
            var knownPickLists = new[] {
                new {
                    listName = "Gender",
                    items = new[] {
                        new { value= "GD_MALE", label= "Male", description= "Gentleman" },
                        new { value= "GD_FEMALE", label= "Female", description= "Lady" },
                        new { value= "GD_LONG", label= "This is a reaaaaaaaaaaally long option", description= "long" }
                    }
                }
            };
            return knownPickLists.Where(i => names.Contains(i.listName));
        }

        [HttpGet]
        [Route("picklists/{listName}")]
        public object GetList(string listName)
        {
            var knownPickLists = new[] {
                new {
                    listName = "Gender",
                    items = new[] {
                        new { value= "GD_MALE", label= "Male", description= "Gentleman" },
                        new { value= "GD_FEMALE", label= "Female", description= "Lady" },
                        new { value= "GD_LONG", label= "This is a reaaaaaaaaaaally long option", description= "long" }
                    }
                }
            };
            return knownPickLists.SingleOrDefault(i => listName == i.listName);
        }

    }
}
