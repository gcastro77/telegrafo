using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Telegrafo.Models.Mensajes
{
    public class Message
    {
        public bool IsOK { get; set; }
        public string ResultMorse { get; set; }
        public string ResultHumano { get; set; }
        public string ErrorMessage { get; set; }

    }
}