using MauiSqlite.Mobile.ViewModels;

namespace MauiSqlite.Mobile.Views;

public partial class AfiliadoView : ContentPage
{
	public AfiliadoView(AfiliadoViewModel mv)
	{
		InitializeComponent();
        BindingContext = mv;

    }
}