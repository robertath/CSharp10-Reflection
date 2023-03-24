using ReflectionExample;

Console.Title = "Learning Reflection";

NetworkMonitor.BootstrapFromConfiguration();

Console.WriteLine("Monitoring network... something went wrong.");

NetworkMonitor.Warn();

Console.ReadLine();