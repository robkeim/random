//-----------------------------------------------------------------------
// <copyright file="RegistrationViewModel.cs" company="RobKeim">
//     Rob Keim Corporation
// </copyright>
//-----------------------------------------------------------------------
namespace Robkeim.ViewModels
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using Robkeim.BusinessLogic;
    using Robkeim.Models;

    /// <summary>
    /// This is the view model for a registration
    /// </summary>
    public class RegistrationViewModel
    {
        /// <summary>
        /// Initializes a new instance of the RegistrationViewModel class
        /// </summary>
        public RegistrationViewModel()
        {
        }

        /// <summary>
        /// Initializes a new instance of the RegistrationViewModel class
        /// </summary>
        /// <param name="registration_">The registration model used to generate the view model</param>
        public RegistrationViewModel(Registration registration_)
        {
            this.Person = Helpers.GetNameFromEnumValue(registration_.Person);
            this.Participating = registration_.Participating ? "Yes" : "No";
            this.AddressLine1 = registration_.AddressLine1;
            this.AddressLine2 = registration_.AddressLine2;
            this.Date = registration_.Date;
            this.ID = registration_.ID;
            this.Registration = registration_;
        }

        /// <summary>
        /// Gets or sets a value for the person's name
        /// </summary>
        [Required(ErrorMessage = "You must select a value")]
        public string Person { get; set; }
        
        /// <summary>
        /// Gets or sets a value indicating whether the person is participating
        /// </summary>
        public string Participating { get; set; }
        
        /// <summary>
        /// Gets or sets a value for first line of the person's address
        /// </summary>
        public string AddressLine1 { get; set; }

        /// <summary>
        /// Gets or sets a value for the second line of the person's address
        /// </summary>
        public string AddressLine2 { get; set; }

        /// <summary>
        /// Gets or sets a value for the date that the person registered
        /// </summary>
        public DateTime Date { get; set; }

        /// <summary>
        /// Gets or sets a value for the database id for the registration
        /// </summary>
        public int ID { get; set; }

        /// <summary>
        /// Gets or sets the internal Registration model object
        /// </summary>
        private Registration Registration { get; set; }

        /// <summary>
        /// Gets the internal Registration model object
        /// </summary>
        /// <returns>Registration object</returns>
        public Registration GetRegistration()
        {
            this.Registration = this.Registration ?? new Registration();
            this.Registration.Person = Helpers.GetEnumValueFromName(this.Person);
            this.Registration.Participating = this.Participating == "Yes";
            this.Registration.AddressLine1 = this.AddressLine1;
            this.Registration.AddressLine2 = this.AddressLine2;
            this.Registration.Date = (this.Date == DateTime.MinValue) ? DateTime.Now.AddHours(3) : this.Date;

            return this.Registration;
        }
    }
}