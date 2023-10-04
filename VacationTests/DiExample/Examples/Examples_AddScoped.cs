using DiExample.Examples.Models;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;

namespace DiExample.Examples;

public class Examples_AddScoped : Base
{
    [Test]
    public void DiTest_NotCreateScope()
    {
        var container = new ServiceCollection()
            .AddScoped<Token>()
            .AddScoped<ConsoleTokenWriter1>()
            .AddSingleton<ConsoleTokenWriter2>()
            .BuildServiceProvider();

        // Если мы не создаем скоуп, то все отработает как Singleton
        var tokenInstance1 = container.GetRequiredService<Token>();
        var tokenInstance2 = container.GetRequiredService<Token>();

        Assert.AreEqual(tokenInstance1.Id, tokenInstance2.Id);
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

        Assert.AreEqual(writer1Instance2.TokenInfo, writer2Instance2.TokenInfo);
        Assert.AreEqual(writer1Instance1.TokenInfo, writer2Instance1.TokenInfo);
    }

    [Test]
    public void DiTest_CreateScope()
    {
        var container = new ServiceCollection()
            .AddScoped<Token>()
            .AddScoped<ConsoleTokenWriter1>()
            .AddSingleton<ConsoleTokenWriter2>()
            .BuildServiceProvider();

        // скоуп - это некоторая коробочка для сервисов/инстансов
        // в разных коробочках разные сервисы, если добавляли через AddScoped
        var scop1 = container.CreateScope();
        Log("\nСоздали Первый скоуп");
        var token_scope1_Instance1 = scop1.ServiceProvider.GetRequiredService<Token>();
        var token_scope1_Instance2 = scop1.ServiceProvider.GetRequiredService<Token>();

        Assert.AreEqual(token_scope1_Instance1.Id, token_scope1_Instance2.Id);
        Log(nameof(token_scope1_Instance1) + " -> " + token_scope1_Instance1.Id);
        Log(nameof(token_scope1_Instance2) + " -> " + token_scope1_Instance2.Id);

        var writer1_scope1_Instance1 = scop1.ServiceProvider.GetRequiredService<ConsoleTokenWriter1>();
        var writer1_scope1_Instance2 = scop1.ServiceProvider.GetRequiredService<ConsoleTokenWriter1>();

        Assert.AreEqual(writer1_scope1_Instance2.TokenInfo, writer1_scope1_Instance1.TokenInfo);
        writer1_scope1_Instance1.WriteText();
        writer1_scope1_Instance2.WriteText();

        var writer2_scope1_Instance1 = scop1.ServiceProvider.GetRequiredService<ConsoleTokenWriter2>();
        var writer2_scope1_Instance2 = scop1.ServiceProvider.GetRequiredService<ConsoleTokenWriter2>();

        Assert.AreEqual(writer2_scope1_Instance1.TokenInfo, writer2_scope1_Instance2.TokenInfo);
        writer2_scope1_Instance1.WriteText();
        writer2_scope1_Instance2.WriteText();

        Assert.AreNotEqual(writer1_scope1_Instance2.TokenInfo, writer2_scope1_Instance2.TokenInfo);
        Assert.AreNotEqual(writer1_scope1_Instance1.TokenInfo, writer2_scope1_Instance1.TokenInfo);

        var scop2 = container.CreateScope();
        Log("\nnСоздали Второй скоуп");
        var token_scope2_Instance1 = scop2.ServiceProvider.GetRequiredService<Token>();
        var token_scope2_Instance2 = scop2.ServiceProvider.GetRequiredService<Token>();

        Assert.AreEqual(token_scope2_Instance1.Id, token_scope2_Instance2.Id);
        Log(nameof(token_scope2_Instance1) + " -> " + token_scope2_Instance1.Id);
        Log(nameof(token_scope2_Instance2) + " -> " + token_scope2_Instance2.Id);

        var writer1_scope2_Instance1 = scop2.ServiceProvider.GetRequiredService<ConsoleTokenWriter1>();
        var writer1_scope2_Instance2 = scop2.ServiceProvider.GetRequiredService<ConsoleTokenWriter1>();

        Assert.AreEqual(writer1_scope2_Instance2.TokenInfo, writer1_scope2_Instance1.TokenInfo);
        writer1_scope2_Instance1.WriteText();
        writer1_scope2_Instance2.WriteText();

        var writer2_scope2_Instance1 = scop2.ServiceProvider.GetRequiredService<ConsoleTokenWriter2>();
        var writer2_scope2_Instance2 = scop2.ServiceProvider.GetRequiredService<ConsoleTokenWriter2>();

        Assert.AreEqual(writer2_scope2_Instance1.TokenInfo, writer2_scope2_Instance2.TokenInfo);
        writer2_scope2_Instance1.WriteText();
        writer2_scope2_Instance2.WriteText();

        Assert.AreNotEqual(writer1_scope2_Instance2.TokenInfo, writer2_scope2_Instance2.TokenInfo);
        Assert.AreNotEqual(writer1_scope2_Instance1.TokenInfo, writer2_scope2_Instance1.TokenInfo);
    }
}