//-----------------------------------------------------------------------
// <copyright file="RegistrationInitializer.cs" company="RobKeim">
//     Rob Keim Corporation
// </copyright>
//-----------------------------------------------------------------------
namespace Robkeim.Models
{
    using System.Collections.Generic;
    using System.Data.Entity;

    /// <summary>
    /// Initializes the information about all of the existing registration
    /// </summary>
    public class RegistrationInitializer : DropCreateDatabaseIfModelChanges<RegistrationDBContext>
    {
        /// <summary>
        /// Populate the database with an initial set of data
        /// This is an empty set, and only non-empty for test scenarios
        /// </summary>
        /// <param name="context">The context to populate</param>
        protected override void Seed(RegistrationDBContext context)
        {
            var registrations = new List<Registration>()
            {
            };

            registrations.ForEach(r => context.Registrations.Add(r));
        }
    }
}