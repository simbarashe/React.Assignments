using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using static System.Console;

namespace SortBDay
{
    public class Program
    {
        public static void Main(string[] args)
        {
            try
            {
                //setup our DI
                var serviceProvider = new ServiceCollection()
                    .AddLogging()
                    .AddSingleton<IPersonRepository, PersonRepository>()
                    .BuildServiceProvider();

                //configure console logging
                serviceProvider
                    .GetService<ILoggerFactory>()
                    .AddConsole(LogLevel.Debug);

                var logger = serviceProvider.GetService<ILoggerFactory>()
                    .CreateLogger<Program>();

                var personRepository = serviceProvider.GetService<IPersonRepository>();
                var people = personRepository.Get();
                foreach (var person in people)
                {
                    logger.LogInformation($"FullName: {person.FullName} ; Age: {person.Age} ");
                }

                logger.LogInformation($"The average age is: {people.Average(s => s.Age)}");

            }
            catch (Exception exception)
            {
                WriteLine($"The following error was successfully handled: {exception.Message}" );
            }
            finally
            {
                ReadLine();
            }
        }
    }
}
