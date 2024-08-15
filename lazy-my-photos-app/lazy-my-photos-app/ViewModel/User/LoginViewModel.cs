using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace lazy_my_photos_app.ViewModel.User
{
    [ObservableObject]
    public partial class LoginViewModel
    {
        [RelayCommand]
        async Task Register()
        {
            await Shell.Current.GoToAsync("RegisterPage");
        }

    }
}
