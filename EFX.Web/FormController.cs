using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using React;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace EFX.Web
{
    public class FormController : Controller
    {
        // GET: api/<controller>
        [HttpGet]
        [Route("forms/{formName}")]
        public object GetTranspiledForm(string formName)
        {
            //Ideally this should be from database...
            string formContent = @"
<MetaForm readOnly={{ false }}>
    <MetaField path=""name"" />
</MetaForm >";

            string fakeSource = $"class {formName} extends React.Component {{ render() {{ return ({formContent}); }} }}";
            var environment = React.ReactEnvironment.Current;
            var result = environment.Babel.Transform(fakeSource);
            return Content(result, "text/javascript");
        }

    }
}
