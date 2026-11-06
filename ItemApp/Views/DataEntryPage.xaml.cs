namespace ItemApp.Views;

public partial class DataEntryPage : ContentPage
{
    readonly DataEntryViewModel _vm = new();

    public DataEntryPage()
    {
        InitializeComponent();
        BindingContext = _vm;
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        await _vm.LoadAsync();
    }
}