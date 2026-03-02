using System;
using System.Collections.Generic;
using System.Text;

namespace Hospital.Infrastructure.Logging
{
    public static class Logger
    {
        private static readonly string filepath = "errorlog.txt";
        public static void Log(Exception ex)
        {
            try
            {
                using (StreamWriter writer = new StreamWriter(filepath, true)) {
                    writer.WriteLine("----");
                    writer.WriteLine($"Date : {DateTime.Now}");
                    writer.WriteLine($"message: {ex.Message}");
                    writer.WriteLine($"Stacktrace: {ex.StackTrace}");
                    writer.WriteLine($"----");
                    writer.WriteLine();
                }

            }
            catch
            {

            }
        }
    }
}
