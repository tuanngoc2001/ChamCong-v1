using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChamCong.Common.Utils
{
    public class Helper
    {
        public static ActionResult TransformData(Response data)
        {
            var result = new ObjectResult(data) { StatusCode = (int)data.Code };
            return result;
        }
    }
}
