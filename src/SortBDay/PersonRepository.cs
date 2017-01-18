using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace SortBDay
{
    public class PersonRepository : IPersonRepository
    {
        private readonly char[] delimiters;
        private readonly string _filePath;
        public PersonRepository()
        {
            _filePath = Directory.GetCurrentDirectory() + @"\Files\people.txt";
            delimiters = new char[] { ',' };
        }

        public IEnumerable<Person> Get()
        {
            try
            {
                char[] delimiters = new char[] { ',' };
                var people = new List<Person>();
                using (StreamReader reader = File.OpenText(_filePath))
                {
                    while (true)
                    {
                        var line = reader.ReadLine();
                        if (line == null)
                        {
                            break;
                        }
                        var parts = line.Split(delimiters);

                        if (parts.Length != 2) continue;

                        var person = new Person
                        {
                            FullName = parts[0],
                            Age = Convert.ToInt32(parts[1])
                        };
                        people.Add(person);
                    }
                }
                return SortByAssendingOrder(people);
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        private static IEnumerable<Person> SortByAssendingOrder(IEnumerable<Person> people)
        {
            try
            {
                var sortedPeople = new List<Person>();
                people = people ?? new List<Person>();
                if (!people.Any()) return sortedPeople;

                var noOfPeople = people.Count();
                for (var i = 0; i < noOfPeople; i++)
                {
                    if (i == 0)
                    {
                        sortedPeople.Add(people.First());
                        continue;
                    }

                    var person = people.ElementAt(i);

                    var noOfPeopleSorted = sortedPeople.Count();
                    for (var s = 0; s < noOfPeopleSorted; s++)
                    {
                        var currentSortedPerson = sortedPeople.ElementAt(s);

                        var currentResult = string.Compare(person.FullName , currentSortedPerson.FullName, true);
                        if (currentResult < 0)
                        {
                            sortedPeople.Insert(s, person);
                            break;
                        }
                        else
                        {
                            if (s == noOfPeopleSorted - 1)
                            {
                                sortedPeople.Add(person);
                                break;
                            }
                            var nextIndex = s + 1;
                            var nextSortedPerson = sortedPeople.ElementAt(nextIndex);

                            var nextResult = string.Compare(person.FullName, nextSortedPerson.FullName,  true);
                            if (nextResult < 0)
                            {
                                sortedPeople.Insert(nextIndex, person);
                                break;
                            }
                            else
                            {
                                continue;
                            }
                        }
                    }

                }
                return sortedPeople;
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }
    }
}
