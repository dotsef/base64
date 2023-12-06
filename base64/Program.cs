using System.Text;

if (!Console.IsInputRedirected)
{
    Console.Error.WriteLine("Pipe something into base64 for it to be encoded or decoded");
    return 1;
}

if (args.Length is not 1 || args[0] is "help" or "?" or "--help" or "-h")
{
    Console.Error.WriteLine("""
        base64 encodes or decodes stdin into stdout

        Examples:

          echo 'example' | base64 encode

          echo 'ZXhhbXBsZQ==' | base64 decode
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