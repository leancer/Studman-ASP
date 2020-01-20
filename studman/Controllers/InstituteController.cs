using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using studman.Models;
namespace studman.Controllers
{
    public class InstituteController : Controller
    {
        //
        // GET: /Institute/

        public JsonResult Index()
        {
            //JsonResult js = new JsonResult();
            adamlye_studmanEntities ins = new adamlye_studmanEntities();

            return Json(ins.tbl_institute, JsonRequestBehavior.AllowGet);
            
            //return View();
        }

    }
}
