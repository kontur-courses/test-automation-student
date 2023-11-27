using System;

namespace DiExample.Examples.Models;

public class DoubleToken : ConsoleWriter
{
    public DoubleToken(Token token, Token token2) : base($"\n1: {token.Id}\n2: {token2.Id}")
    {
        Token1 = token.Id;
        Token2 = token2.Id;
    }

    public Guid Token1 { get; }
    public Guid Token2 { get; }
}