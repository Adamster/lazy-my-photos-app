using lazy_my_photos_app.ViewModel.User;

namespace lazy_my_photos_app.View.User;

public partial class LoginPage : ContentPage
{
    private readonly LoginViewModel _viewModel = new();

	public LoginPage()
	{
		InitializeComponent();
        BindingContext = _viewModel;
    }
}