using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;
using System.Web.Http.Results;

namespace ApiMorseCode.Controllers
{
    public class TranslateController : ApiController
    {
        public class model
        {
            public string text;
            public model() { }

        }
       
        
        [HttpPost]
        [Route("api/translate/2text")]
        public IHttpActionResult text(model param)
        {
            try
            {
                var result = MorseCode.CodeHelper.translate2Human(param.text);
                return Ok<string>(result);
            }
           catch(Exception e)
            {
                return new ExceptionResult(e, this);
            }
            
        }

        [HttpPost]
        [Route("api/translate/2morse")]
        public IHttpActionResult morse(model param)
        {
            try
            {
                var result = MorseCode.CodeHelper.translate2Morse(param.text);
                return Ok<string>(result);
            }
            catch (Exception e)
            {
                return new ExceptionResult(e, this);
            }
        }
    }
}
