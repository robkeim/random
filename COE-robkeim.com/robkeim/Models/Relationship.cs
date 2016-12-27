//-----------------------------------------------------------------------
// <copyright file="Relationship.cs" company="RobKeim">
//     Rob Keim Corporation
// </copyright>
//-----------------------------------------------------------------------
namespace Robkeim.Models
{
    using System.Data.Entity;

    /// <summary>
    /// This is the model for a relationship
    /// </summary>
    public class Relationship
    {
        /// <summary>
        /// Gets or sets the id record used by the database
        /// </summary>
        public int ID { get; set; }

        /// <summary>
        /// Gets or sets the first person
        /// </summary>
        public int Person1 { get; set; }

        /// <summary>
        /// Gets or sets the second person
        /// </summary>
        public int Person2 { get; set; }

        /// <summary>
        /// Gets or sets the relationship between the two people
        /// </summary>
        public int Relation { get; set; }
    }

    /// <summary>
    /// This class manages the interface with the database
    /// </summary>
    public class RelationshipDBContext : DbContext
    {
        /// <summary>
        /// Gets or sets the database set
        /// </summary>
        public DbSet<Relationship> Relationships { get; set; }
    }
}