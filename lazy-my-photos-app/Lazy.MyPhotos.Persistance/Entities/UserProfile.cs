using CommunityToolkit.Mvvm.ComponentModel;

namespace Lazy.MyPhotos.Persistence.Entities;

[ObservableObject]
public partial class UserProfile
{
    [ObservableProperty]
    private string userId;

    [ObservableProperty]
    private string name;

    [ObservableProperty]
    private string email;
}