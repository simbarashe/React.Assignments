using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static System.Console;

namespace LoggingFramework
{
    public class Program
    {
        public static void Main(string[] args)
        {
            try
            {
                using (var logger = new Logger("testlogger.txt", Logger.Level.Info))
                {
                    logger.Debug($"Debug Message Test");
                    logger.Info($"Info Message Test");
                    logger.Error($"Error Message Test");
                    logger.Info($"Demo of Logger starting");
                    for(int i = 0;i<5;i++)
                    {
                        logger.Info($"i = {i}");
                    }
                    logger.Info($"Demo of Logger complete");
                }
            }
            catch (Exception exception)
            {
                WriteLine($"The following error was successfully handled: {exception.Message}");
            }
            finally
            {
                ReadLine();
            }
        }
    }
}
