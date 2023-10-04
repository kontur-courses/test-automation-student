using System;
using DiExample.Examples.Models;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;

namespace DiExample.Examples;

public class Examples_AddTransient : Base
{
    [Test]
    public void DiTest_AddTransient()
    {
        var text = Guid.NewGuid().ToString();
        var container = new ServiceCollection()
            .AddTransient<Token>()
            .AddSingleton<ConsoleWriter>(_ => new ConsoleWriter(text))
            .BuildServiceProvider();

        // 2 раза берем из контейнера объект Token и кладем в разные переменные 
        var tokenInstance1 = container.GetRequiredService<Token>();
        var tokenInstance2 = container.GetRequiredService<Token>();

        // Проверяем, что они разные. Мы использовали AddTransient
        Assert.AreNotEqual(tokenInstance1.Id, tokenInstance2.Id);
        Log(nameof(tokenInstance1) + " -> " + tokenInstance1.Id);
        Log(nameof(tokenInstance2) + " -> " + tokenInstance2.Id);
        
        // Берем из контейнера 2 разных экземпляра 1 объекта ConsoleWriter 
        var writerInstance1 = container.GetRequiredService<ConsoleWriter>();
        var writerInstance2 = container.GetRequiredService<ConsoleWriter>();

        // они по прежнему одинаковы, т.к. остался Singleton
        Assert.AreEqual(text, writerInstance1.TokenInfo);
        Assert.AreEqual(text, writerInstance2.TokenInfo);
        writerInstance1.WriteText();
        writerInstance2.WriteText();
    }

    [Test]
    public void DiTest_Transient_And_Singleton()
    {
        var container = new ServiceCollection()
            .AddTransient<Token>()
            // Смотрим как поведет себя система, если регистрировать Writer
            // используя токен из контейнера
            .AddSingleton<ConsoleWriter>(s =>
            {
                var text = s.GetRequiredService<Token>().Id.ToString();
                return new ConsoleWriter(text);
            })
            .BuildServiceProvider();

        var tokenInstance1 = container.GetRequiredService<Token>();
        var tokenInstance2 = container.GetRequiredService<Token>();

        Assert.AreNotEqual(tokenInstance1.Id, tokenInstance2.Id);
        Log(nameof(tokenInstance1) + " -> " + tokenInstance1.Id);
        Log(nameof(tokenInstance2) + " -> " + tokenInstance2.Id);

        var writerInstance1 = container.GetRequiredService<ConsoleWriter>();
        var writerInstance2 = container.GetRequiredService<ConsoleWriter>();

        // у нас по прежнему должны быть одинаковые объекты
        Assert.AreEqual(writerInstance2.TokenInfo, writerInstance1.TokenInfo);
        writerInstance1.WriteText();
        writerInstance2.WriteText();
    }

    [Test]
    public void DiTest_Transient_And_Singleton_2()
    {
        // теперь посмотрим на то, как регистрируются 2 разных писателя, которые внутри конструктора принимают Token
        var container = new ServiceCollection()
            .AddTransient<Token>()
            .AddSingleton<ConsoleTokenWriter1>()
            .AddSingleton<ConsoleTokenWriter2>()
            .BuildServiceProvider();

        var tokenInstance1 = container.GetRequiredService<Token>();
        var tokenInstance2 = container.GetRequiredService<Token>();

        Assert.AreNotEqual(tokenInstance1.Id, tokenInstance2.Id);
        Log(nameof(tokenInstance1) + " -> " + tokenInstance1.Id);
        Log(nameof(tokenInstance2) + " -> " + tokenInstance2.Id);

        var writer1Instance1 = container.GetRequiredService<ConsoleTokenWriter1>();
        var writer1Instance2 = container.GetRequiredService<ConsoleTokenWriter1>();

        Assert.AreEqual(writer1Instance2.TokenInfo, writer1Instance1.TokenInfo);
        writer1Instance1.WriteText();
        writer1Instance2.WriteText();

        var writer2Instance1 = container.GetRequiredService<ConsoleTokenWriter2>();
        var writer2Instance2 = container.GetRequiredService<ConsoleTokenWriter2>();

        Assert.AreEqual(writer2Instance1.TokenInfo, writer2Instance2.TokenInfo);
        writer2Instance1.WriteText();
        writer2Instance2.WriteText();

        
        // Гуиды у писателей должны быть разные т.к. при конструировании контейнер находит объект Token для первого врайтера
        // Затем находит объект Token для второго врайтера. 
        // Токен добавлен как Transient, а значит будет 2 разных токена для врайтеров.
        // Поэтому врайтеры будут разные.
        Assert.AreNotEqual(writer1Instance2.TokenInfo, writer2Instance2.TokenInfo);
        Assert.AreNotEqual(writer1Instance1.TokenInfo, writer2Instance1.TokenInfo);
    }
}