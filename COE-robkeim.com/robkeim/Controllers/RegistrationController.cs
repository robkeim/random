//-----------------------------------------------------------------------
// <copyright file="RegistrationController.cs" company="RobKeim">
//     Rob Keim Corporation
// </copyright>
//-----------------------------------------------------------------------
namespace Robkeim.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web.Mvc;
    using Robkeim.BusinessLogic;
    using Robkeim.Models;
    using Robkeim.ViewModels;

    /// <summary>
    /// This is the controller for all of the registration operations
    /// </summary>
    [Authorize(Roles = "View")]
    public class RegistrationController : Controller
    {
        /// <summary>
        /// The database context
        /// </summary>
        private RegistrationDBContext db = new RegistrationDBContext();

        /// <summary>
        /// The index method for the registration controller
        /// </summary>
        /// <returns>The ViewResult</returns>
        public ViewResult Index()
        {
            var registrations = this.db.Registrations.OrderBy(r => r.Person);

            ViewBag.HaveConfirms = registrations.Where(registration => registration.Participating).Any();
            ViewBag.HaveDeclines = registrations.Where(registration => !registration.Participating).Any();

            return View(this.GetRegistrations());
        }

        /// <summary>
        /// The create method for registrations
        /// </summary>
        /// <returns>The ActionResult</returns>
        public ActionResult Create()
        {
            List<string> participants = new List<string>();

            foreach (int value in Enum.GetValues(typeof(Participant)))
            {
                participants.Add(Helpers.GetNameFromEnumValue(value));
            }

            List<int> registrations = (from registration in this.db.Registrations
                                               select registration.Person).ToList();

            IEnumerable<string> currentlyRegistered = from registration in registrations
                                               select Helpers.GetNameFromEnumValue(registration);

            ViewData["participants"] = new SelectList(participants.Except(currentlyRegistered));
            ViewData["yesNo"] = new SelectList(new List<string> { "Yes", "No" });

            return View();
        }

        /// <summary>
        /// The create method for registration posts
        /// </summary>
        /// <param name="registration">The registration view model</param>
        /// <returns>The ActionResult</returns>
        [HttpPost]
        public ActionResult Create(RegistrationViewModel registration)
        {
            if (ModelState.IsValid)
            {
                Registration submit = registration.GetRegistration();

                bool alreadyRegistered = this.db.Registrations.Where(r => r.Person == submit.Person).Any();

                if (alreadyRegistered)
                {
                    var test = this.db.Registrations.Where(r => r.Person == submit.Person).Single();

                    this.db.Registrations.Remove(test);
                }

                this.db.Registrations.Add(submit);
                this.db.SaveChanges();
                Helpers.SendEmails(registration.Person, false, registration.Participating == "Yes", registration.AddressLine1, registration.AddressLine2);
                return RedirectToAction("Index");
            }

            return View(registration);
        }

        /// <summary>
        /// Allows registrations to be edited
        /// </summary>
        /// <param name="id">The id of the record to edit</param>
        /// <returns>The ActionResult</returns>
        public ActionResult Edit(int id)
        {
            Registration registration = this.db.Registrations.Find(id);

            ViewData["yesNo"] = new SelectList(new List<string> { "Yes", "No" });

            return View(new RegistrationViewModel(registration));
        }

        /// <summary>
        /// Updates a registration
        /// </summary>
        /// <param name="registration">The updated registration</param>
        /// <returns>The ActionResult</returns>
        [HttpPost]
        public ActionResult Edit(RegistrationViewModel registration)
        {
            if (ModelState.IsValid)
            {
                int personId = registration.GetRegistration().Person;
                Registration old = this.db.Registrations.Where(r => r.Person == personId).Single();

                this.db.Registrations.Remove(old);
                this.db.Registrations.Add(registration.GetRegistration());
                
                this.db.SaveChanges();

                Helpers.SendEmails(registration.Person, true, registration.Participating == "Yes", registration.AddressLine1, registration.AddressLine2);
                return RedirectToAction("Index");
            }

            return View(registration);
        }

        /// <summary>
        /// Delete all of the current registrations
        /// WARNING: This method is irreversible
        /// </summary>
        /// <returns>String with the operation result</returns>
        [Authorize(Roles = "Admin")]
        public string DeleteAll()
        {
            foreach (var registration in this.db.Registrations)
            {
                this.db.Registrations.Remove(registration);
            }

            this.db.SaveChanges();

            return "Done";
        }

        /// <summary>
        /// Registers all of the possible participants
        /// WARNING: This method is irreversible
        /// </summary>
        /// <returns>String with the operation result</returns>
        [Authorize(Roles = "Admin")]
        public string RegisterAll()
        {
            foreach (int value in Enum.GetValues(typeof(Participant)))
            {
                Registration reg = new Registration
                {
                    Person = value,
                    Participating = true,
                    AddressLine1 = "Default",
                    Date = DateTime.Now.AddHours(3)
                };

                this.db.Registrations.Add(reg);
            }

            this.db.SaveChanges();

            return "Done";
        }

        /// <summary>
        /// Dispose of instances of the class
        /// </summary>
        /// <param name="disposing">Bool indicating who is calling the dispose method</param>
        protected override void Dispose(bool disposing)
        {
            this.db.Dispose();
            base.Dispose(disposing);
        }

        /// <summary>
        /// Return a list of view model objects
        /// </summary>
        /// <returns>List of registration view models</returns>
        private List<RegistrationViewModel> GetRegistrations()
        {
            List<RegistrationViewModel> registrations = new List<RegistrationViewModel>();

            foreach (var registration in this.db.Registrations.OrderBy(reg => reg.Person))
            {
                registrations.Add(new RegistrationViewModel(registration));
            }

            return registrations;
        }
    }
}