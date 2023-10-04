namespace DiExample.Examples.Models;

public class ConsoleTokenWriter1 : ConsoleWriter
{
    public ConsoleTokenWriter1(Token token) : base(token.Id.ToString())
    {
    }
}