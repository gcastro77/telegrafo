using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Telegrafo.Models.Mensajes;


namespace WebApplication1.Controllers
{
    public class TelegraphController : Controller
    {
        
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Bits2Morse(string bits)
        {
            var model = new Message { IsOK = true };
            try
            { 
               model.ResultMorse = MorseCode.CodeHelper.decodeBits2Morse(bits);
               model.ResultHumano = MorseCode.CodeHelper.translate2Human(model.ResultMorse);

            }
            catch(Exception ex)
            {
                model.IsOK = false;
                model.ErrorMessage = ex.Message;
            }
            return Json(model);
        }
    }
}