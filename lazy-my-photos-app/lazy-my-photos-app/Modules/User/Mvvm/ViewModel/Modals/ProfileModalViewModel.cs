using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using Lazy.MyPhotos.App.Infrastructure.Messages.User;

namespace Lazy.MyPhotos.App.Modules.User.Mvvm.ViewModel.Modals
{
    public partial class ProfileModalViewModel : ObservableObject
    {

        [RelayCommand]
        public async Task Close()
        {
            await Application.Current!.MainPage!.Navigation!.PopModalAsync();
        }

        [RelayCommand]
        public void Logout()
        {
            LogoutInternal();
        }

        private static void LogoutInternal()
        {
            MainThread.BeginInvokeOnMainThread(() =>
            {
                WeakReferenceMessenger.Default.Send(new UserLogoutMessage());
            });
        }
    }
}
