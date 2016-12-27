//-----------------------------------------------------------------------
// <copyright file="Registration.cs" company="RobKeim">
//     Rob Keim Corporation
// </copyright>
//-----------------------------------------------------------------------
namespace Robkeim.Models
{
    using System;
    using System.Data.Entity;

    /// <summary>
    /// This class represents a registration
    /// </summary>
    public class Registration
    {
        /// <summary>
        /// Gets or sets the id used by the database
        /// </summary>
        public int ID { get; set; }

        /// <summary>
        /// Gets or sets the person who is registering
        /// </summary>
        public int Person { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the person is planning on participating
        /// </summary>
        public bool Participating { get; set; }

        /// <summary>
        /// Gets or sets the first line of the person's address
        /// </summary>
        public string AddressLine1 { get; set; }

        /// <summary>
        /// Gets or sets the second line of the person's address
        /// </summary>
        public string AddressLine2 { get; set; }

        /// <summary>
        /// Gets or sets the date when the user registered
        /// </summary>
        public DateTime Date { get; set; }
    }

    /// <summary>
    /// This class manages the interface with the database
    /// </summary>
    public class RegistrationDBContext : DbContext
    {
        /// <summary>
        /// Gets or sets the database set
        /// </summary>
        public DbSet<Registration> Registrations { get; set; }
    }
}