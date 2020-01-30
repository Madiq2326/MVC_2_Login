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
using System.Threading.Tasks;
using System.Data.Entity;

namespace MVC_2.Controllers
{
    public class UserController : Controller
    {

        ApplicationDbContext myContext = new ApplicationDbContext();
        // GET: User
        public async Task<ActionResult> Index()
        {
            var check = await myContext.Users.ToListAsync();
            return View(check);
        }

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Login(User user)

        {
            try
            {
                var check = await myContext.Users.FirstOrDefaultAsync(a => a.Username.Equals(user.Username));

                bool what = class_hash.ValidatePassword(user.Password, check.Password);

                if (check != null && what == true)
                {
                    return RedirectToAction("Index");
                }

                else
                {
                    return RedirectToAction("Login");
                }
            }

            catch (Exception ex)
            {
                return View("Error_404");
            }
        }

        public async Task<ActionResult> Edit(int id)
        {
            var edit = await myContext.Users.FindAsync(id);

            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Edit(int id, User user)
        {
            try
            {
                var check = await myContext.Users.FindAsync(id);

                check.Password = user.Password;

                return RedirectToAction("Index");
            }

            catch (Exception ex)
            {
                return View("Error_404");
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
        public async Task<ActionResult> Create(User user)
        {
            try
            {
                string ori_Password = user.Password;

                user.Password = class_hash.HashPassword(user.Password);

                myContext.Users.Add(user);

                var role = await myContext.Roles.FirstOrDefaultAsync(a => a.Id == 2);

                user.Role_id = role;

                var result = await myContext.SaveChangesAsync();

                if(result > 0)
                {
                    MailMessage mm = new MailMessage("Madiq2326@gmail.com", user.Email);
                    mm.Subject = "Madiq Group";
                    mm.Body = "Hi " + user.Username + " thanks for registering to our new application \n This Is Your New Password : " + ori_Password + "\n And your hash password is " + user.Password;

                    SmtpClient smtp = new SmtpClient();
                    smtp.Host = "smtp.gmail.com";
                    smtp.Port = 587;
                    smtp.EnableSsl = true;

                    smtp.UseDefaultCredentials = false;
                    smtp.Credentials = new NetworkCredential("Madiq2326@gmail.com", "Mind2326");
                    smtp.Send(mm);

                    return RedirectToAction("Login");
                }

                else
                {
                   return RedirectToAction("Create");
                }
            }
            catch (Exception ex)
            {
                return View("Error_404");
            }
        }

        public async Task<ActionResult> Delete(int id)
        {
            var delete = await myContext.Users.FindAsync(id);
            return View();
        }

        // POST: Roles/Delete/5
        [HttpPost]
        public async Task<ActionResult> Delete(int id, User user)
        {
            try
            {
                var delete = await myContext.Users.FindAsync(id);
                myContext.Users.Remove(delete);
                myContext.SaveChanges();
                // TODO: Add delete logic here

                return RedirectToAction("Login");
            }
            catch (Exception ex)
            {
                return View("Error_404");
            }
        }

        public ActionResult Error_404()
        {
            return View();
        }

        public ActionResult test()
        {
            return View();
        }

        public async Task<ActionResult> tables()
        {
            var check = await myContext.Users.ToListAsync();
            return View(check);
        }
    }
}
