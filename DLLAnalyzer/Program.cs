using System.IO;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;

namespace DLLAnalyzer
{
    internal class Program
    {

        static void Main(string[] args)
        {
            Console.WriteLine("Welcome to DLLAnalyzer");
            if (args.Length == 0)
            {
                Console.WriteLine("Not enough arguments...");
                return;
            }
            else
            {
               Options opts = new Options();
               opts.Parse(args);
               Program.Run(opts);
            }
        }

        static void Run(Options options)
        {
            var assembly = System.Reflection.Assembly.LoadFile(options.File);
            Type[] types = assembly.GetTypes();
            foreach (var type in types)
            {
                Regex classRegexp = new Regex(options.ClassRegexp);
                if (!classRegexp.IsMatch(type.FullName??string.Empty))
                {
                    continue;
                }
                Console.WriteLine("---Type---");
                Console.WriteLine($"Type name: {type.FullName}");
                var methods = type.GetMethods();
                if(options.Depth == Options.eDepth.Method || options.Depth == Options.eDepth.Full)
                foreach (var method in methods)
                {
                    Regex methodRegexp = new Regex(options.MethodRegexp);
                    if (!methodRegexp.IsMatch(method.Name ?? string.Empty))
                    {
                        continue;
                    }
                    Console.WriteLine("\t---Method---");
                    Console.WriteLine($"\tMethod name: {method.Name}");
                    Console.WriteLine($"\tReturn type: {method.ReturnType}");
                    Console.WriteLine($"\tReturn parameter: {method.ReturnParameter}");

                    var parameters = method.GetParameters();
                        
                    if(options.Depth == Options.eDepth.Full)
                    for (int i = 0; i < parameters.Length; i++)
                    {
                        var parameter = parameters[i];
                        Console.WriteLine("\t\t---Parameter---");
                        Console.WriteLine($"\t\tParameter: {i}");
                        Console.WriteLine($"\t\tParameter name: {parameter.Name}");
                        Console.WriteLine($"\t\tParameter name: {parameter.ParameterType.FullName}");
                        Console.WriteLine("\t\t---EndParameter---");
                        Console.WriteLine(" ");
                    }

                    Console.WriteLine("\t---EndMethod---");
                    Console.WriteLine(" ");
                }
                Console.WriteLine("---EndType---");
            }
        }
    }
}