
using GalaSoft.MvvmLight.Command;
using Soccer.Services;
using System.Windows.Input;
using System;

namespace Soccer.ViewModels
{
   public class MenuItemViewModel
    {
        #region Atributos
        private NavigationService navigationService;

        #endregion

        #region Properties
        public string Icon { get; set; }

        public string Title { get; set; }

        public string PageName { get; set; }
        #endregion

        #region Construtor
        public MenuItemViewModel()
        {
            navigationService = new NavigationService();
        }
        #endregion

        #region Comandos
        public ICommand NavigateCommand { get { return new RelayCommand(Navigate); } }

        private void Navigate()
        {
            if (PageName == "LoginPage")
            {
                navigationService.SetMainPage("LoginPage");
            }
        }
        #endregion
    }

       
 }
