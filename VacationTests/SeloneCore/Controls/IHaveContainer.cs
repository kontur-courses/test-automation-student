using OpenQA.Selenium;

namespace SeloneCore.Controls;

// Об интерфейсах https://ulearn.me/course/basicprogramming/Interfeysy_3df89dfb-7f0f-4123-82ac-364c3a426396
public interface IHaveContainer
{
    IWebElement Container { get; }
}