using Uuid.Core;

return Run(args);

static int Run(string[] args)
{
    var service = new LongestIncreasingSubsequenceService(
        new IntegerSequenceParser(),
        new LongestStrictlyIncreasingSubsequenceFinder(),
        new SubsequenceFormatter());

    if (args.Length > 0)
    {
        return SolveOnce(service, string.Join(' ', args));
    }

    if (Console.IsInputRedirected)
    {
        return SolveOnce(service, Console.In.ReadToEnd().TrimEnd('\r', '\n'));
    }

    while (true)
    {
        Console.Write("Enter integers separated by single spaces: ");
        var input = Console.ReadLine();
        if (input is null || input.Length == 0)
        {
            return 0;
        }

        SolveOnce(service, input);
    }
}

static int SolveOnce(LongestIncreasingSubsequenceService service, string input)
{
    try
    {
        Console.WriteLine($"the sequence is {service.Solve(input)}");
        return 0;
    }
    catch (FormatException ex)
    {
        Console.Error.WriteLine(ex.Message);
        return 1;
    }
    catch (OverflowException ex)
    {
        Console.Error.WriteLine(ex.Message);
        return 1;
    }
}
