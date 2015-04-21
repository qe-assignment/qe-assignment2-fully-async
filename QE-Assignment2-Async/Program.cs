using System;
using System.IO;
using System.Reflection;
using System.Threading.Tasks;
using FullContactApi;
using Nito.AsyncEx;

namespace QE_Assignment2_Async
{
    class Program
    {
        static void Main(string[] args)
        {
            var apiKey = String.Empty;
            try
            {
                var assembly = Assembly.GetExecutingAssembly();
                using (var textStreamReader = new StreamReader(assembly.GetManifestResourceStream("QE_Assignment2_Async.FullContactKey.txt")))
                {
                    apiKey = textStreamReader.ReadLine();
                }
            }
            catch 
            {
                Console.WriteLine("Error accessing resources!");
                Environment.Exit(-1);
            }

            var api = new MyFullContactApi(apiKey);
            AsyncContext.Run(() => MainAsync(api));
        }

        static async void MainAsync(MyFullContactApi api)
        {
            while (true)
            {
                Console.WriteLine("Enter email address: ");
                Console.Write(">> ");

                var input = await GetInput();
                if (input == "exit")
                    return;

                Display(input, api);
            }
        }

        static async Task<string> GetInput()
        {
            return await Task.Run(() => Console.ReadLine());
        }

        static async void Display(string input, MyFullContactApi api)
        {
            var response = await api.LookupPersonByEmailAsync(input);
            Console.WriteLine(response);
        }
    }
}
