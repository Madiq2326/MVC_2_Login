using MVC_2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BCrypt.Net;
using MVC_2.Hash;
using Dapper;
using System.Data.SqlClient;
using System.Configuration;
using System.Net.Mail;
using System.Net;

namespace MVC_2.Controllers
{
    public class UserController : Controller
    {

        ApplicationDbContext myContext = new ApplicationDbContext();
        // GET: User
        public ActionResult Index()
        {
            var check = myContext.Users.ToList() ;
            return View(check);
        }

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(User user)
        {
            var check = myContext.Users.FirstOrDefault(a => a.Email.Equals(user.Email));

            bool what = class_hash.ValidatePassword(user.Password, check.Password);

            if(check != null && what == true)
            {
                 return RedirectToAction("Index");
            }

            else
            {
                 return RedirectToAction("Login");
            }
        }

        public ActionResult Edit(int id)
        {
            var edit = myContext.Users.Find(id);

            return View();
        }

        [HttpPost]
        public ActionResult Edit(int id, User user)
        {
            try
            {
                var check = myContext.Users.Find(id);

                check.Password = user.Password;

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: User/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: User/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: User/Create
        [HttpPost]
        public ActionResult Create(User user)
        {
            try
            {
                string ori_Password = user.Password;

                user.Password = class_hash.HashPassword(user.Password);

                // TODO: Add insert logic here

                myContext.Users.Add(user);

                myContext.SaveChanges();

                MailMessage mm = new MailMessage("Madiq2326@gmail.com", user.Email);
                mm.Subject = "Madiq Group";
                mm.Body = "Hi " + user.Username + " thanks for registering to our new application \n This Is Your New Password : " + ori_Password;

                SmtpClient smtp = new SmtpClient();
                smtp.Host = "smtp.gmail.com";
                smtp.Port = 587;
                smtp.EnableSsl = true;

                smtp.UseDefaultCredentials = false;
                smtp.Credentials = new NetworkCredential("Madiq2326@gmail.com", "Mind2326");
                smtp.Send(mm);

                return RedirectToAction("Login");
            }
            catch
            {
                return View();
            }
        }

        public ActionResult Delete(int id)
        {
            var delete = myContext.Users.Find(id);
            return View();
        }

        // POST: Roles/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, User user)
        {
            try
            {
                var delete = myContext.Users.Find(id);
                myContext.Users.Remove(delete);
                myContext.SaveChanges();
                // TODO: Add delete logic here

                return RedirectToAction("Login");
            }
            catch
            {
                return View();
            }
        }
    }
}
