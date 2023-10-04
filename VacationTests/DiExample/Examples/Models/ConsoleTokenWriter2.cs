namespace DiExample.Examples.Models;

public class ConsoleTokenWriter2 : ConsoleWriter
{
    public ConsoleTokenWriter2(Token token) : base(token.Id.ToString())
    {
    }
}