using Soccer.Pages;

namespace Soccer.Services
{
    public class NavigationService
    {
        public void SetMainPage(string pageName)
        {
            switch (pageName)
            {
                case "MasterPage":
                    App.Current.MainPage = new MasterPage();
                    break;
                case "LoginPage":
                    //Logout();
                    App.Current.MainPage = new LoginPage();
                    break;
                default:
                    break;
            }
        }


    }
}
