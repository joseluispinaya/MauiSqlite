using MauiSqlite.Mobile.ViewModels;

namespace MauiSqlite.Mobile.Views;

public partial class AddAfiliadoView : ContentPage
{
	public AddAfiliadoView(AddAfiliadoViewModel vm)
	{
		InitializeComponent();
        BindingContext = vm;

    }
}