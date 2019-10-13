using Gas_Go_v1.Models;
using Gas_Go_v1.Services;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Gas_Go_v1.Controllers
{
    public class EmailSenderController : Controller
    {
        [Authorize(Roles = "Admin")]
        public ActionResult Send_Email()
        {
            return View(new SendEmailViewModel());
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public ActionResult Send_Email(SendEmailViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    String toEmail = model.ToEmail;
                    String subject = model.Subject;
                    String contents = model.Contents;
                    HttpPostedFileBase postedFile = model.Upload;
                    String fileAddress;
                    EmailSender es = new EmailSender();

                    if (postedFile != null)
                    {
                        string path = Server.MapPath("~/Uploads/");
                    if (!Directory.Exists(path))
                        {
                            Directory.CreateDirectory(path);
                        }

                        String fileName = Path.GetFileName(postedFile.FileName);
                        fileAddress = path + fileName;
                        postedFile.SaveAs(fileAddress);
                        es.Send(toEmail, subject, contents, fileAddress, fileName);
                    }
                    if (postedFile == null)
                        es.Send(toEmail, subject, contents, null, null);


                    ViewBag.Result = "Email has been send.";

                    ModelState.Clear();

                    return View(new SendEmailViewModel());


                }
                catch
                {
                    return View();
                }
            }
            return View();
        }

        
        public ActionResult Upload_File()
        {
            return View(new SendEmailViewModel());
        }

        [HttpPost]
        public ActionResult Upload_File(SendEmailViewModel model)
        {

                try
                {
                    HttpPostedFileBase postedFile = model.Upload;
                    String fileAddress;

                    if (postedFile != null)
                    {
                        string path = Server.MapPath("~/Uploads/");
                        if (!Directory.Exists(path))
                        {
                            Directory.CreateDirectory(path);
                        }

                        String fileName = Path.GetFileName(postedFile.FileName);
                        fileAddress = path + fileName;
                        postedFile.SaveAs(fileAddress);
                        ViewBag.Result = "File uploaded successfully.";
                    }
                    ModelState.Clear();

                    return View(new SendEmailViewModel());


                }
                catch
                {
                    return View();
                }
        }
    }
}