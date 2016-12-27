using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Interview5
{
    public static class Facebook
    {
        public static int FindShortestPath(Person p1, Person p2)
        {
            int currentDistance = 0;

            Dictionary<Person, int> distances = new Dictionary<Person, int>();

            Queue<Person> peopleToProcess = new Queue<Person>();
            peopleToProcess.Enqueue(p1);

            while (peopleToProcess.Count > 0)
            {
                Person curPerson = peopleToProcess.Dequeue();
                if (curPerson == p2)
                {
                    return currentDistance;
                }

                distances[curPerson] = currentDistance;

                foreach (Person friend in curPerson.Friends)
                {
                    if (!distances.ContainsKey(friend))
                    {
                        distances[friend] = currentDistance + 1;
                        peopleToProcess.Enqueue(friend);
                    }
                }

                currentDistance++;
            }

            // If we run out of people without a solution then these two people are
            // not connected
            return -1;
        }

        public class Person
        {
            public Person[] Friends;
        }
    }
}
