namespace WpfUI.Views
{
    public interface IViewFactory
    {
        IView CreateView(string viewName);
    }
}