using NUnit.Framework;

namespace DiExample.Examples.Models;

public class ConsoleWriter
{
    public readonly string TokenInfo;

    public ConsoleWriter(string tokenInfo)
    {
        TokenInfo = tokenInfo;
        TestContext.Out.WriteLine($"Зарегестрировали {nameof(ConsoleWriter)} с текстом {TokenInfo}");
    }

    public void WriteText()
    {
        TestContext.Out.WriteLine($"В поле текст: {TokenInfo}");
    }
}