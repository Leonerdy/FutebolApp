

using GalaSoft.MvvmLight.Command;
using Soccer.Services;
using System.ComponentModel;
using System.Windows.Input;
using System;
using Plugin.Connectivity;
using Soccer.Models;

namespace Soccer.ViewModels
{
    public class LoginViewModel : INotifyPropertyChanged
    {
        #region Events
        public event PropertyChangedEventHandler PropertyChanged;
        #endregion

        #region Attributes
        private ApiService apiService;

        private DialogService dialogService;

        private NavigationService navigationService;

        private string email;

        private string password;

        private bool isRunning;

        private bool isEnabled;

        private bool isRemembered;
        #endregion

        #region Properties
        public string Email
        {
            set
            {
                if (email != value)
                {
                    email = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Email"));
                }
            }
            get
            {
                return email;
            }
        }

        public string Password
        {
            set
            {
                if (password != value)
                {
                    password = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Password"));
                }
            }
            get
            {
                return password;
            }
        }

        public bool IsRunning
        {
            set
            {
                if (isRunning != value)
                {
                    isRunning = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("IsRunning"));
                }
            }
            get
            {
                return isRunning;
            }
        }

        public bool IsEnabled
        {
            set
            {
                if (isEnabled != value)
                {
                    isEnabled = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("IsEnabled"));
                }
            }
            get
            {
                return isEnabled;
            }
        }

        public bool IsRemembered
        {
            set
            {
                if (isRemembered != value)
                {
                    isRemembered = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("IsRemembered"));
                }
            }
            get
            {
                return isRemembered;
            }
        }
        #endregion

        #region Constructors
        public LoginViewModel()
        {
            apiService = new ApiService();
            dialogService = new DialogService();
            navigationService = new NavigationService();


            IsRemembered = true;
            IsEnabled = true;
            Email = null;
            Password = null;
        }
        #endregion

        #region Commands
        public ICommand LoginCommand { get { return new RelayCommand(Login); } }

        private async void Login()
        {
            if (string.IsNullOrEmpty(Email))
            {
                await dialogService.ShowMessage("Erro", "Você deve entrar com seu email.");
                return;
            }

            if (string.IsNullOrEmpty(Password))
            {
                await dialogService.ShowMessage("Erro", "Você deve entrar com sua senha.");
                return;
            }

            IsRunning = true;
            IsEnabled = false;

            if (!CrossConnectivity.Current.IsConnected)
            {
                IsRunning = false;
                IsEnabled = true;
                await dialogService.ShowMessage("Erro", "Verifique sua conexão com a internet.");
                return;
            }

            var isReachable = await CrossConnectivity.Current.IsRemoteReachable("google.com");
            if (!isReachable)
            {
                IsRunning = false;
                IsEnabled = true;
                await dialogService.ShowMessage("Erro", "Verifique sua conexão com a internet.");
                return;
            }

            var token = await apiService.GetToken("http://soccerapi.azurewebsites.net", Email, Password);

            if (token == null)
            {
                IsRunning = false;
                IsEnabled = true;
                await dialogService.ShowMessage("Erro", "O nome de usuário ou senha estão incorretos");
                Password = null;
                return;
            }

            if (string.IsNullOrEmpty(token.AccessToken))
            {
                IsRunning = false;
                IsEnabled = true;
                await dialogService.ShowMessage("Erro", token.ErrorDescription);
                Password = null;
                return;
            }

            var response = await apiService.GetUserByEmail("http://soccerapi.azurewebsites.net",
                "/api", "/Users/GetUserByEmail", token.TokenType, token.AccessToken, token.UserName);

            if (!response.IsSuccess)
            {
                IsRunning = false;
                IsEnabled = true;
                await dialogService.ShowMessage("Erro", "Ocorreu um problema ao recuperar as informações do usuário, tente mais tarde.");
                return;
            }

            Email = null;
            password = null;

            IsRunning = false;
            IsEnabled = true;
            var user = (User)response.Result;
            var mainViewModel = MainViewModel.GetInstance();
            mainViewModel.CurrentUser = user;
            navigationService.SetMainPage("MasterPage");
        }
        #endregion
    }
}
