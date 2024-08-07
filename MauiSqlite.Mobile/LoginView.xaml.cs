using MauiSqlite.Mobile.ViewModels;

namespace MauiSqlite.Mobile;

public partial class LoginView : ContentPage
{
	public LoginView(LoginViewModel vm)
	{
		InitializeComponent();
        BindingContext = vm;

    }
}