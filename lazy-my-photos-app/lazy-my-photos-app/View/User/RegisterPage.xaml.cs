using lazy_my_photos_app.ViewModel.User;

namespace lazy_my_photos_app.View.User;

public partial class RegisterPage : ContentPage
{
    private RegisterViewModel _viewModel = new();

	public RegisterPage()
	{
		InitializeComponent();
		BindingContext = _viewModel;
	}
}