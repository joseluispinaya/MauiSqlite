using MauiSqlite.Mobile.ViewModels;

namespace MauiSqlite.Mobile.Views;

public partial class LoadingView : ContentPage
{
	public LoadingView(LoadingViewModel vm)
	{
		InitializeComponent();
		BindingContext = vm;
	}
}