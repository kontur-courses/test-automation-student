﻿using Kontur.Selone.Elements;
using Kontur.Selone.Selectors;
using Kontur.Selone.Selectors.Context;
using OpenQA.Selenium;
using SeloneCore.Controls.BaseWebElements;
using SeloneCore.Page;

namespace SeloneCore;

public interface IPageObjectFactory
{
    /// <summary>Создать страницу типа TPageObject</summary>
    TPage CreatePage<TPage>(IWebDriver webDriver) where TPage : PageBase;

    /// <summary>Создать контрол типа TPageElement</summary>
    TControl CreateControl<TControl>(ISearchContext container, string tid) where TControl : ControlBase;

    /// <summary>Создать контрол типа TPageElement</summary>
    TControl CreateControl<TControl>(IContextBy contextBy) where TControl : ControlBase;    
    
    /// <summary>Создать контрол типа TBox</summary>
    TBox CreateLightBox<TBox>(ISearchContext context) where TBox : Lightbox;

    /// <summary>Создать коллекцию контролов типа TItem</summary>
    ElementsCollection<TItem> CreateElementsCollection<TItem>(ISearchContext itemsSearchContext,
        string tid) where TItem : ControlBase;

    /// <summary>Создать коллекцию контролов типа TItem</summary>
    ElementsCollection<TItem> CreateElementsCollection<TItem>(ISearchContext itemsSearchContext,
        ItemByLambda findItem) where TItem : ControlBase;

    TBox CreateLightBox<TBox>(IContextBy contextBy) where TBox : Lightbox;
}