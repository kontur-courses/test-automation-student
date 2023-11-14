using System.Reflection;
using Kontur.Selone.Elements;
using Kontur.Selone.Extensions;
using Kontur.Selone.Selectors;
using Kontur.Selone.Selectors.Context;
using Kontur.Selone.Selectors.XPath;
using OpenQA.Selenium;
using SeloneCore.Controls;
using SeloneCore.Controls.BaseWebElements;
using SeloneCore.Page;

namespace VacationTests.Infrastructure;

public class ControlFactory : IControlFactory
{
    private readonly object[] dependencies;
    public ControlFactory(params object[] dependencies) => this.dependencies = dependencies;

    public TControl CreateControl<TControl>(ISearchContext container, string tid) where TControl : ControlBase
        => CreateControl<TControl>(container.Search(x => x.WithTid(tid)));

    public TControl CreateControl<TControl>(IContextBy contextBy) where TControl : ControlBase
        => (TControl) CreateInstance(typeof(TControl), contextBy, dependencies.Prepend(this).ToArray());

    public TPage CreatePage<TPage>(IWebDriver webDriver) where TPage : PageBase
    {
        var allDependencies = dependencies.Prepend(this).Prepend(webDriver).ToArray();
        return (TPage) CreateInstance(typeof(TPage), new PageContext(webDriver), allDependencies);
    }

    public ElementsCollection<TItem> CreateElementsCollection<TItem>(ISearchContext itemsSearchContext, string tid)
        where TItem : ControlBase
        => CreateElementsCollection<TItem>(itemsSearchContext, x => x.WithTid(tid).FixedByIndex());

    public ElementsCollection<TItem> CreateElementsCollection<TItem>(ISearchContext itemsSearchContext,
        ItemByLambda findItem) where TItem : ControlBase
    {
        return new ElementsCollection<TItem>(itemsSearchContext,
            findItem,
            (s, b, _) => CreateControl<TItem>(new ContextBy(s, b)));
    }

    private static object CreateInstance(Type controlType, IContextBy contextBy, object[] dependencies)
    {
        // У объекта, который хотим создать, проверяем, что конструктор есть и он один
        var constructors = controlType.GetConstructors();
        if (constructors.Length != 1)
            throw new NotSupportedException($"Контрол {controlType} должен иметь только один конструктор");
        var constructor = constructors.Single();
        // У конструктора получаеям все его входные парметры, которые ему нужны
        var parameters = constructor.GetParameters();
        var args = new List<object>();
        // Провеярем, что среди наших зависимостей есть все необходимые для создания объекта
        foreach (var parameterInfo in parameters)
        {
            var arg =
                dependencies.Prepend(contextBy).FirstOrDefault(dep =>
                    dep != null && dep.GetType().IsAssignableTo(parameterInfo.ParameterType)) ??
                throw new NotSupportedException(
                    $"Не поддерживаемый тип {parameterInfo.ParameterType} параметра конструктора контрола {controlType}");
            args.Add(arg);
        }

        // Вызываем конструктор и передаём ему все входные параметры
        var value = constructor.Invoke(args.ToArray());

        // Получаем контекст, по которому будем искать все контролы, входящие в состав нашего объекта
        var searchContext = contextBy switch
        {
            PageContext _ => contextBy.SearchContext,
            ContextBy _ => contextBy.SearchContext.SearchElement(contextBy.By)
        };
        if (searchContext == null)
            throw new NotSupportedException(
                "Для автоматической инициализации полей контрола должен быть известен ISearchContext. " +
                "Либо укажите IContextBy, либо передайте в зависимости WebDriver.");
        // Инициализируем контролы объекта
        InitializePropertiesWithControls(value, searchContext, dependencies);

        // Возвращаем экземпляр объекта
        return value;
    }

    private static void InitializePropertiesWithControls(object control, ISearchContext searchContext,
        params object[] dependencies)
    {
        // У переданного объекта ищем все свойства, наследющиеся от ControlBase
        var controlProps = control.GetType().GetProperties()
            .Where(p => typeof(ControlBase).IsAssignableFrom(p.PropertyType)).ToList();

        // Для каждого найденного свойства:
        foreach (var prop in controlProps)
        {
            // проверяем, что доступен метод set;
            if (prop.SetMethod is null) continue;

            // находим атрибут BaseSearchByAttribute или его наследника ByTidAttribute
            var attribute = prop.GetCustomAttribute<BaseSearchByAttribute>(true);
            // если атрибут не найден, то берём название самого свойства,
            // а если атрибут найден, берём его значение
            var contextBy = attribute == null
                ? searchContext.Search(x => x.WithTid(prop.Name))
                : searchContext.Search((ByLambda) attribute.SearchCriteria);

            // создаём экземпляр свойства через CreateInstance,
            // чтобы иницаилизировать у сложных контролов ещё и их свойства
            var value = CreateInstance(prop.PropertyType, contextBy, dependencies);
            // присваиваем свойству объекта полученный экземпляр
            prop.SetValue(control, value);
        }
    }
}