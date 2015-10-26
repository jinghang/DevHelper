using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ADH.DevHelperWeb.Models
{
    public class JsonResultObj
    {
        public bool IsSuccess { get; set; }
        public string Msg { get; set; }
        public object Data { get; set; }
    }
}