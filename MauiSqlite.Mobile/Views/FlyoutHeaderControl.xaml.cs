using MauiSqlite.Mobile.ViewModels;

namespace MauiSqlite.Mobile.Views;

public partial class FlyoutHeaderControl : StackLayout
{
	public FlyoutHeaderControl()
	{
		InitializeComponent();
        BindingContext = new FlyoutHeaderControlModel();
    }
}