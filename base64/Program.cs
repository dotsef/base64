using System.Text;

if (!Console.IsInputRedirected)
{
    Console.Error.WriteLine("Pipe something into base64 for it to be encoded or decoded");
    return 1;
}

if (args.Length is not 1 || args[0] is "help" or "?" or "--help" or "-h")
{
    const string blue = "\x1b[34m";
    const string fgReset = "\x1b[39m";
    const string bold = "\x1b[1m";
    const string fmReset = "\x1b[22m";
    Console.Error.WriteLine($"""
        base64 encodes or decodes stdin into stdout

        {blue}Examples{fgReset}

          echo 'example' | base64 {bold}{blue}encode{fmReset}{fgReset}
          echo 'ZXhhbXBsZQ==' | base64 {bold}{blue}decode{fmReset}{fgReset}

        """);

    return 0;
}

var input = await Console.In.ReadToEndAsync();

if (args[0] is "encode")
{
    var bytes = Encoding.UTF8.GetBytes(input);
    await Console.Out.WriteLineAsync(Convert.ToBase64String(bytes));
}

else if (args[0] is "decode")
{
    var bytes = Convert.FromBase64String(input);
    await Console.Out.WriteLineAsync(Encoding.UTF8.GetString(bytes));
}

return 0;