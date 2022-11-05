var scenarioTypes = typeof(Program).Assembly.GetTypes().Where(t => typeof(IScenario).IsAssignableFrom(t) && t != typeof(IScenario)).ToList();

foreach (var eachScenarioType in scenarioTypes)
{
    WriteBanner(eachScenarioType);
    var instance = (IScenario)Activator.CreateInstance(eachScenarioType)!;
    
    await instance.Run();
}

Console.WriteLine("Finished");


static void WriteBanner(Type eachScenarioType)
{
    Console.WriteLine();
    Console.WriteLine();
    Console.WriteLine();
    Console.WriteLine("==========================================================");
    Console.WriteLine($"Running {eachScenarioType.Name}");
}

public interface IScenario
{
    Task Run();
}