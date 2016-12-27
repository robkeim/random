//-----------------------------------------------------------------------
// <copyright file="RelationshipInitializer.cs" company="RobKeim">
//     Rob Keim Corporation
// </copyright>
//-----------------------------------------------------------------------
namespace Robkeim.Models
{
    using System.Collections.Generic;
    using System.Data.Entity;

    /// <summary>
    /// Initializes the information about all of the relationships between people
    /// </summary>
    public class RelationshipInitializer : DropCreateDatabaseIfModelChanges<RelationshipDBContext>
    {
        /// <summary>
        /// Populate the database with an initial set of data
        /// </summary>
        /// <param name="context">The context to populate</param>
        protected override void Seed(RelationshipDBContext context)
        {
            var relationships = new List<Relationship>()
            {
                // Siblings
                
                // Spouses
            };

            relationships.ForEach(r => context.Relationships.Add(r));
        }
    }
}