using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using HouseholdBudget.Models;
using Microsoft.AspNet.Identity;
using System.Threading.Tasks;

namespace HouseholdBudget
{
    public class HouseholdController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Household
        [Authorize]
        public ActionResult Index()
        {
            var user = db.Users.Find(User.Identity.GetUserId());
            var household = user.household;

            return View(household);
        }

        // GET: Household/Members
        [Authorize]
        public ActionResult Members()
        {
            var user = db.Users.Find(User.Identity.GetUserId());
            var household = user.household;
            

            return View(household);
        }

        //GET: Household/Leave
        [Authorize]
        public ActionResult Leave()
        {

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Leave(int? id)
        {
            var user = db.Users.Find(User.Identity.GetUserId());
            user.householdId = null;
            db.SaveChanges();

            return RedirectToAction("Index","Home",null);
        }

        // POST: Invitation
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Invitation(Invitation invitation)
        {   
            //Random Number Generator
            Random num = new Random();
            var RandomNum=num.Next();
            
            var user = db.Users.Find(User.Identity.GetUserId());
            var name = user.FullName;
            var houseId = user.householdId;
            invitation.householdId = houseId.GetValueOrDefault();
            invitation.Code = RandomNum;
            db.Invitations.Add(invitation);
            db.SaveChanges();

            var houseName = db.Households.FirstOrDefault(h => h.Id == invitation.householdId).Name;
            var callbackRegister = Url.Action("Register", "Account", new { Code = invitation.Code }, protocol: Request.Url.Scheme);
            var callbackJoin = Url.Action("Join", "Invitations", new { InvCd = invitation.Code }, protocol: Request.Url.Scheme);

            // Send the Email
            var svc2 = new EmailService();
            var msg2 = new IdentityMessage();
            msg2.Destination = invitation.Email;
            msg2.Subject = "Household Invitation";
            msg2.Body = name +" has invited you on join " + houseName+ " your login code is " + RandomNum +" Please click  <a href\"" + callbackRegister +"\"> here </a>" + " to register."  ;
            await svc2.SendAsync(msg2);
            
            return RedirectToAction("Index");
           
        }

        // GET: Household/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Household household = db.Households.Find(id);
            if (household == null)
            {
                return HttpNotFound();
            }
            return View(household);
        }

        // GET: Household/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Household/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name")] Household household)
        {
            if (ModelState.IsValid)
            {
                
             
                db.Households.Add(household);
                db.SaveChanges();
                var user = db.Users.Find(User.Identity.GetUserId());
                user.householdId = household.Id;
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(household);
        }

        // GET: Household/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Household household = db.Households.Find(id);
            if (household == null)
            {
                return HttpNotFound();
            }
            return View(household);
        }

        // POST: Household/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name")] Household household)
        {
            if (ModelState.IsValid)
            {
                db.Entry(household).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(household);
        }

        // GET: Household/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Household household = db.Households.Find(id);
            if (household == null)
            {
                return HttpNotFound();
            }
            return View(household);
        }

        // POST: Household/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Household household = db.Households.Find(id);
            //ApplicationUser User = db.Users.Find(User.Identity.GetUserId()).HouseholdId;
            db.Households.Remove(household);
            //TODO:Need to remove from user table 
            //var user = db.Users.Find(User.Identity.GetUserId());
            //db.Users.Remove(user.HouseholdId);
           db.SaveChanges();

            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
