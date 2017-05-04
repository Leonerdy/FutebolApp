
using Soccer.Models;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace Soccer.ViewModels
{
    public class MainViewModel 
    {

        #region propriedades
        public LoginViewModel Login { get; set; }

        public User CurrentUser { get; set; }

        public ObservableCollection<MenuItemViewModel> Menu { get; set; }
        #endregion

        #region Construtor
        public MainViewModel()
        {
            instance = this;
            Login = new LoginViewModel();
            LoadMenu();

        } 
        #endregion
        #region Singleton
        private static MainViewModel instance;

        public static MainViewModel GetInstance()
        {
            if (instance == null)
            {
                instance = new MainViewModel();
            }

            return instance;
        }
        #endregion

        #region Methods
        private void LoadMenu()
        {
            Menu = new ObservableCollection<MenuItemViewModel>();
            Menu.Add(new MenuItemViewModel
            {
                Icon = "predictions.png",
                PageName = "SelectTournamentPage",
                Title = "Predictions",
            });

            Menu.Add(new MenuItemViewModel
            {
                Icon = "groups.png",
                PageName = "GroupsPage",
                Title = "Groups",
            });

            Menu.Add(new MenuItemViewModel
            {
                Icon = "tournaments.png",
                PageName = "TournamentsPage",
                Title = "Tournaments",
            });

            Menu.Add(new MenuItemViewModel
            {
                Icon = "myresults.png",
                PageName = "ResultsPage",
                Title = "My Results",
            });

            Menu.Add(new MenuItemViewModel
            {
                Icon = "config.png",
                PageName = "ConfigPage",
                Title = "Config",
            });

            Menu.Add(new MenuItemViewModel
            {
                Icon = "logut.png",
                PageName = "LoginPage",
                Title = "Logut",
            });

        }
        #endregion
    }
}
