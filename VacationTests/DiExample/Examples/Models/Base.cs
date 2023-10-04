using NUnit.Framework;

namespace DiExample.Examples.Models;

public class Base
{
    protected void Log(string text)
    {
        TestContext.Out.WriteLine(text);
    }
}