using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using studman.Models;

namespace studman.Controllers
{
    public class StudentController : Controller
    {
        //
        // GET: /Student/

        public ActionResult Index()
        {

            adamlye_studmanEntities st = new adamlye_studmanEntities();

            var data = (from student in st.students.Take(10) select student);
            return Json(new {status = "success",msg = "working",data = data },JsonRequestBehavior.AllowGet);
        }

        public ActionResult me()
        {
            return View();
        }

        [HttpPost]
        public JsonResult register(student stud)
        {
            if (ModelState.IsValid)
            {

                if (this.isEmailExist(stud.email))
                {
                    return Json(new { status = "fail", msg = "Email Already Exist" }, JsonRequestBehavior.AllowGet);
                }

                using (adamlye_studmanEntities st = new adamlye_studmanEntities())
                {

                    st.students.AddObject(stud);
                    int res = st.SaveChanges();
                    if (res > 0)
                    {
                        return Json(new { status = "success", msg = "register Successfull" }, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        return Json(new { status = "fail", msg = "not Able to Register" }, JsonRequestBehavior.AllowGet);
                    }
                }



            }
            else
            {
                return Json(new { status = "fail", msg = "invalid Request" }, JsonRequestBehavior.AllowGet);
            }



        }

        [HttpPost]
        public JsonResult login(student stud)
        {
            using (adamlye_studmanEntities st = new adamlye_studmanEntities())
            {

                var v = st.students.Where(a => a.email == stud.email).FirstOrDefault();
                if (v != null)
                {
                    if (string.Compare(stud.password, v.password) == 0)
                    {
                        return Json(new { status = "Success", msg = "Login Success" }, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        return Json(new { status = "fail", msg = "Wrong Credential" }, JsonRequestBehavior.AllowGet);
                    }
                }
                else
                {
                    return Json(new { status = "fail", msg = "Wrong Credential" }, JsonRequestBehavior.AllowGet);
                }
            }
        }

        private JsonResult Json()
        {
            throw new NotImplementedException();
        }

        [NonAction]
        public bool isEmailExist(string email)
        {
            using (adamlye_studmanEntities se = new adamlye_studmanEntities())
            {

                var v = se.students.Where(a => a.email == email).FirstOrDefault();
                return v != null;
            }
        }

    }
}
