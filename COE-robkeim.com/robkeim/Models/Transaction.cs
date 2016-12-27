//-----------------------------------------------------------------------
// <copyright file="Transaction.cs" company="RobKeim">
//     Rob Keim Corporation
// </copyright>
//-----------------------------------------------------------------------
namespace Robkeim.Models
{
    using System;
    using System.Data.Entity;

    /// <summary>
    /// This class models one gift exchange
    /// </summary>
    public class Transaction
    {
        /// <summary>
        /// Gets or sets the id used by the database
        /// </summary>
        public int ID { get; set; }

        /// <summary>
        /// Gets or sets the giver
        /// </summary>
        public int Giver { get; set; }

        /// <summary>
        /// Gets or sets the receiver
        /// </summary>
        public int Receiver { get; set; }

        /// <summary>
        /// Gets or sets the year the gift was exchanged
        /// </summary>
        public DateTime Year { get; set; }
    }

    /// <summary>
    /// This class manages the interface with the database
    /// </summary>
    public class TransactionDBContext : DbContext
    {
        /// <summary>
        /// Gets or sets the database set
        /// </summary>
        public DbSet<Transaction> Transactions { get; set; }
    }
}