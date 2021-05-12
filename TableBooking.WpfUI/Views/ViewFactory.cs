namespace WpfUI.Views
{
    public class ViewFactory : IViewFactory
    {
        private readonly LoginView _loginView;
        private readonly RegisterView _registerView;
        private readonly RestaurantsView _restaurantsView;
        private readonly UsersView _usersView;

        public ViewFactory(LoginView loginView, RegisterView registerView, UsersView usersView, RestaurantsView restaurantsView)
        {
            _loginView = loginView;
            _registerView = registerView;
            _usersView = usersView;
            _restaurantsView = restaurantsView;
        }

        public IView CreateView(string viewName)
        {
            return viewName switch
            {
                "Login" => _loginView,
                "Register" => _registerView,
                "Users" => _usersView,
                "Restaurants" => _restaurantsView,
                _ => _registerView,
            };
        }
    }
}