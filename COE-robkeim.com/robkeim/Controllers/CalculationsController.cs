//-----------------------------------------------------------------------
// <copyright file="CalculationsController.cs" company="RobKeim">
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

    /// <summary>
    /// This is the controller for all of the calculations
    /// </summary>
    [Authorize(Roles = "Admin")]
    public class CalculationsController : Controller
    {
        /// <summary>
        /// The matrix of everyone's compatability scores
        /// </summary>
        private int[,] compatabilityMatrix;

        /// <summary>
        /// The index method for the calculations controller
        /// </summary>
        /// <returns>The ActionResult</returns>
        public ActionResult Index()
        {
            List<int> participants = this.GetParticipants();
            int numParticipants = participants.Count;

            this.PopulateMatrix(participants);

            ViewBag.Length = numParticipants;
            ViewBag.People = (from person in participants
                              select Helpers.GetNameFromEnumValue(person)).ToList();

            return View(this.compatabilityMatrix);
        }

        /// <summary>
        /// Generate a list of matches for the given registered people
        /// </summary>
        /// <returns>String with an html table containing the results</returns>
        public string GenerateMatches()
        {
            List<int> participants = this.GetParticipants();
            int numParticipants = participants.Count;

            this.PopulateMatrix(participants);

            List<Constraint> givers = new List<Constraint>();

            for (int i = 0; i < numParticipants; i++)
            {
                int totalWeight = 0;
                for (int j = 0; j < numParticipants; j++)
                {
                    if (i == j)
                    {
                        continue;
                    }

                    totalWeight += this.compatabilityMatrix[i, j];
                }

                givers.Add(new Constraint
                    {
                        Name = Helpers.GetNameFromEnumValue(participants.ElementAt(i)),
                        TotalWeight = totalWeight,
                        Index = i
                    });
            }

            List<Constraint> receivers = new List<Constraint>(givers);

            List<Match> matches = new List<Match>();

            while (givers.Count > 0)
            {
                givers = givers.OrderByDescending(g => g.TotalWeight).ToList();

                Constraint giver = givers.ElementAt(0);
                int receiverIndex = 0;
                int minWeight = int.MaxValue;

                for (int i = 0; i < receivers.Count; i++)
                {
                    int weight = this.compatabilityMatrix[giver.Index, receivers.ElementAt(i).Index];

                    if (weight == 0)
                    {
                        minWeight = 0;
                        receiverIndex = i;
                        break;
                    }

                    if (weight < minWeight)
                    {
                        minWeight = weight;
                        receiverIndex = i;
                    }
                }

                matches.Add(new Match
                    {
                        Giver = giver.Name,
                        Receiver = receivers.ElementAt(receiverIndex).Name,
                        Weight = minWeight
                    });

                for (int i = 1; i < givers.Count; i++)
                {
                    givers.ElementAt(i).TotalWeight -= this.GetRelationship(giver.Index, givers.ElementAt(i).Index);
                }

                givers.RemoveAt(0);
                receivers.RemoveAt(receiverIndex);
            }

            string result = "<table>";

            foreach (var match in matches)
            {
                result += string.Format("<tr><td>{0}</td><td>{1}</td><td>{2}</td></tr>", match.Giver, match.Receiver, match.Weight);
            }

            result += "</table>";

            return result;
        }

        /// <summary>
        /// Return the weighted value of a relationship between two people
        /// </summary>
        /// <param name="person1">The id of the first person</param>
        /// <param name="person2">The id of the second person</param>
        /// <returns>The weighted value of the relationship</returns>
        private int GetRelationship(int person1, int person2)
        {
            RelationshipDBContext db = new RelationshipDBContext();
            List<Relationship> relationships = db.Relationships.ToList();
            List<Relationship> matches = (from relationship in relationships
                                          where relationship.Person1 == person1 &&
                                                 relationship.Person2 == person2
                                          select relationship).ToList();
            if (matches.Any())
            {
                return (Relation)matches.First().Relation == Relation.Spouse ? 100 : 50;
            }

            return 0;
        }

        /// <summary>
        /// Returns a list of all of the people who are participating in this year's exchange
        /// </summary>
        /// <returns>List of ints identifying the people</returns>
        private List<int> GetParticipants()
        {
            RegistrationDBContext db = new RegistrationDBContext();

            return db.Registrations.Where(record => record.Participating).Select(record => record.Person).ToList();
        }
         
        /// <summary>
        /// Populates the compatability matrix
        /// </summary>
        /// <param name="participants">The list of people that are participating</param>
        private void PopulateMatrix(List<int> participants)
        {
            int numParticipants = participants.Count;

            this.compatabilityMatrix = new int[numParticipants, numParticipants];

            for (int i = 0; i < numParticipants; i++)
            {
                this.compatabilityMatrix[i, i] = int.MaxValue;
            }

            this.AddRelationships(participants);
            this.AddHistory(participants);
        }

        /// <summary>
        /// Add all of the relationship information into the compatability matrix
        /// </summary>
        /// <param name="participants">List of people participating</param>
        private void AddRelationships(List<int> participants)
        {
            RelationshipDBContext db = new RelationshipDBContext();
            List<Relationship> relationships = db.Relationships.ToList();

            for (int i = 0; i < participants.Count; i++)
            {
                for (int j = 0; j < participants.Count; j++)
                {
                    var matches = from rel in relationships
                                  where rel.Person1 == participants[i] &&
                                  rel.Person2 == participants[j]
                                  select rel;

                    if (matches.Any())
                    {
                        Relationship rel = matches.Single();
                        int newValue = (rel.Relation == (int)Relation.Spouse) ? 100 : 50;

                        this.compatabilityMatrix[i, j] = Math.Max(this.compatabilityMatrix[i, j], newValue);
                    }
                }
            }
        }

        /// <summary>
        /// Add all of the history information into the compatability matrix
        /// </summary>
        /// <param name="participants">List of people participating</param>
        private void AddHistory(List<int> participants)
        {
            TransactionDBContext db = new TransactionDBContext();
            List<Transaction> transactions = db.Transactions.ToList();

            for (int i = 0; i < participants.Count; i++)
            {
                for (int j = 0; j < participants.Count; j++)
                {
                    var matches = from record in transactions
                                  where record.Giver == participants[i] &&
                                  record.Receiver == participants[j]
                                  select record;

                    // Ensure the most recent match is first
                    matches.OrderBy(record => record.Year);

                    if (matches.Any())
                    {
                        Transaction record = matches.First();

                        // 2003 is the first year that we have historical data from the CoE
                        int newValue = record.Year.Year - 2003 + 1;

                        this.compatabilityMatrix[i, j] = Math.Max(this.compatabilityMatrix[i, j], record.Year.Year - 2003 + 1);
                    }
                }
            }
        }

        /// <summary>
        /// Class that defines total weights for each participant
        /// </summary>
        private class Constraint
        {
            /// <summary>
            /// Gets or sets the name of the participant
            /// </summary>
            public string Name { get; set; }

            /// <summary>
            /// Gets or sets the total weight between this person and every other participant
            /// </summary>
            public int TotalWeight { get; set; }

            /// <summary>
            /// Gets or sets the index that the person appears in the participant array
            /// </summary>
            public int Index { get; set; }
        }

        /// <summary>
        /// Class defining a match between two people
        /// </summary>
        private class Match
        {
            /// <summary>
            /// Gets or sets the person giving the gift
            /// </summary>
            public string Giver { get; set; }

            /// <summary>
            /// Gets or sets the person receiving the gift
            /// </summary>
            public string Receiver { get; set; }

            /// <summary>
            /// Gets or sets the weight of the match
            /// </summary>
            public int Weight { get; set; }
        }
    }
}
