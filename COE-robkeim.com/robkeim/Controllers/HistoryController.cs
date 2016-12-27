//-----------------------------------------------------------------------
// <copyright file="HistoryController.cs" company="RobKeim">
//     Rob Keim Corporation
// </copyright>
//-----------------------------------------------------------------------
namespace Robkeim.Controllers
{
    using System.Collections.Generic;
    using System.Web.Mvc;
    using Robkeim.Models;
    using Robkeim.ViewModels;

    /// <summary>
    /// The controller for all of the history operations
    /// </summary>
    [Authorize(Roles = "View")]
    public class HistoryController : Controller
    {
        /// <summary>
        /// The database context
        /// </summary>
        private TransactionDBContext db = new TransactionDBContext();

        /// <summary>
        /// The index method for the history controller
        /// </summary>
        /// <returns>The ViewResult</returns>
        public ViewResult Index()
        {
            return View(this.GetTransactions());
        }

        /// <summary>
        /// Dispose of an instance of the class
        /// </summary>
        /// <param name="disposing">Bool indicating who is calling the method</param>
        protected override void Dispose(bool disposing)
        {
            this.db.Dispose();
            base.Dispose(disposing);
        }

        /// <summary>
        /// Get the transaction view models from the transaction models
        /// </summary>
        /// <returns>List of transaction view models</returns>
        private List<TransactionViewModel> GetTransactions()
        {
            List<TransactionViewModel> transactions = new List<TransactionViewModel>();

            foreach (var transaction in this.db.Transactions)
            {
                transactions.Add(new TransactionViewModel(transaction));
            }

            return transactions;
        }
    }
}