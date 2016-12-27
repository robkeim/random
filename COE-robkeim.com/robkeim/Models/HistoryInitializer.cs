//-----------------------------------------------------------------------
// <copyright file="HistoryInitializer.cs" company="RobKeim">
//     Rob Keim Corporation
// </copyright>
//-----------------------------------------------------------------------
namespace Robkeim.Models
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;

    /// <summary>
    /// Initializes all of the information about the history
    /// </summary>
    public class HistoryInitializer : DropCreateDatabaseIfModelChanges<TransactionDBContext>
    {
        /// <summary>
        /// Populate the database with an initial set of data
        /// </summary>
        /// <param name="context">The context to populate</param>
        protected override void Seed(TransactionDBContext context)
        {
            //// The first year of the ornament exchange was 2000
            var transactions = new List<Transaction>()
            {
                // 2011

                // 2010
                
                // 2009
                
                // 2005
                
                // 2004
                
                // 2003          
                
                // 2002
            };

            transactions.ForEach(t => context.Transactions.Add(t));
        }

        /// <summary>
        /// Generate a DateTime for a given year
        /// </summary>
        /// <param name="year">The integer form for the year</param>
        /// <returns>DateTime for Christmas of the specified year</returns>
        private DateTime DateFromYear(int year)
        {
            return new DateTime(year, 12, 25);
        }
    }
}