//-----------------------------------------------------------------------
// <copyright file="TransactionViewModel.cs" company="RobKeim">
//     Rob Keim Corporation
// </copyright>
//-----------------------------------------------------------------------
namespace Robkeim.ViewModels
{
    using System;
    using Robkeim.BusinessLogic;
    using Robkeim.Models;

    /// <summary>
    /// This is the transaction view model
    /// </summary>
    public class TransactionViewModel
    {
        /// <summary>
        /// Initializes a new instance of the TransactionViewModel class
        /// </summary>
        /// <param name="transaction">The Transaction model used to initialize</param>
        public TransactionViewModel(Transaction transaction)
        {
            this.ID = transaction.ID;
            this.Giver = Helpers.GetNameFromEnumValue(transaction.Giver);
            this.Receiver = Helpers.GetNameFromEnumValue(transaction.Receiver);
            this.Year = transaction.Year;
        }

        /// <summary>
        /// Gets or sets the id used by the database
        /// </summary>
        public int ID { get; set; }

        /// <summary>
        /// Gets or sets the giver
        /// </summary>
        public string Giver { get; set; }

        /// <summary>
        /// Gets or sets the receiver
        /// </summary>
        public string Receiver { get; set; }

        /// <summary>
        /// Gets or sets the year the gift was exchanged
        /// </summary>
        public DateTime Year { get; set; }
    }
}