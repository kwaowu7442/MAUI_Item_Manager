using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

public partial class LoginViewModel : ObservableObject
{
    readonly ApiService _api = new();
    [ObservableProperty] string userName = "";
    [ObservableProperty] string password = "";
    [ObservableProperty] string message = "";

    [RelayCommand]
    async Task Login()
    {
        try
        {
            if (await _api.LoginAsync(UserName, Password))
            {
                Message = "";
                await Shell.Current.GoToAsync("DataEntryPage");
            }
            else Message = "Login failed. Invalid user name or password.";
        }
        catch { Message = "Login failed. Could not reach the server."; }
    }

    [RelayCommand]
    void Cancel() { UserName = ""; Password = ""; Message = ""; }
}