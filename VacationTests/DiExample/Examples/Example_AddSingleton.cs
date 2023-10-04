using System;
using System.Collections.Generic;
using System.Linq;
using DiExample.Examples.Models;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;

namespace DiExample.Examples;

public class Examples_AddSingleton : Base
{
    [Test]
    public void DiTest_AddSingleton()
    {
        var text = Guid.NewGuid().ToString();

        // Регистрируем контейнер 
        var container = new ServiceCollection()
            .AddSingleton<Token>() // 1 объект на весь проект
            .AddSingleton<ConsoleWriter>(_ => new ConsoleWriter(text))
            .BuildServiceProvider();

        // 2 раза берем из контейнера объект Token и кладем в разные переменные
        var tokenInstance1 = container.GetRequiredService<Token>();
        var tokenInstance2 = container.GetRequiredService<Token>();

        // Проверяем, что экземпляры одинаковые, потому что использовался Singleton
        Assert.AreEqual(tokenInstance1.Id, tokenInstance2.Id);

        // Для наглядности можно смотреть в консоль
        Log(nameof(tokenInstance1) + " -> " + tokenInstance1.Id);
        Log(nameof(tokenInstance2) + " -> " + tokenInstance2.Id);

        // Берем из контейнера 2 разных экземпляра 1 объекта ConsoleWriter
        var writerInstance1 = container.GetRequiredService<ConsoleWriter>();
        var writerInstance2 = container.GetRequiredService<ConsoleWriter>();

        // Проверяем, что экземпляры одинаковые, потому что использовался Singleton
        Assert.AreEqual(text, writerInstance1.TokenInfo);
        Assert.AreEqual(text, writerInstance2.TokenInfo);

        // Для наглядности можно смотреть в консоль
        writerInstance1.WriteText();
        writerInstance2.WriteText();
    }
    
    // Ниже необязательный пример для любознательных, можно пропустить.
    // Тестируем, что при использовании AddSingleton объект лежит в одной и тойже области памяти
    [Test]
    public unsafe void DiTest_AddSingleton_pointer()
    {
        // Регистрируем контейнер 
        var container = new ServiceCollection()
            .AddSingleton<Token>() // 1 объект на весь проект
            .BuildServiceProvider();
        var result = new List<string>();

        // Берем 5 раз из контейнера объект Token
        for (var i = 0; i < 5; i++)
        {
            var tokenInstance = container.GetRequiredService<Token>();
            var id = tokenInstance.Id;

            // достаем указатели на ячейки памяти
            TypedReference reference = __makeref(tokenInstance);
            IntPtr pointerToken = **(IntPtr**) (&reference);
            Guid* pointerTokenId = &id;

            // логируем адреса
            Log($"{nameof(tokenInstance)} : ссылка на адрес памяти -> {(long) pointerToken:X}");
            Log($"{nameof(pointerTokenId)}: {id} : ссылка на адрес памяти -> {(long) pointerTokenId:X}");
            
            // и добавляем в список
            result.Add($"{(long) pointerToken:X}:{(long) pointerTokenId:X}");
        }
        
        // проверяем, что для всех 5 раз адреса были одинаковые
        Assert.AreEqual(1, result.Distinct().Count());
    }
}