namespace DiExample.Selenium.Page
{
    public interface IPage
    {
        string Url { get; }
        string Title { get; }
        string CompositeUrl(string path);
    }
}