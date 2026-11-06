using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Kotlin.Jvm;
using System.Collections.ObjectModel;

public partial class DataEntryViewModel : ObservableObject
{
    readonly ApiService _api = new();
    [ObservableProperty] string itemId = "";
    [ObservableProperty] string itemName = "";
    [ObservableProperty] string itemDescription = "";
    [ObservableProperty] string message = "";
    public ObservableCollection<Item> Items { get; } = new();

    public async Task LoadAsync()
    {
        try
        {
            var list = await _api.GetItemsAsync();
            Items.Clear();
            foreach (var i in list) Items.Add(i);
        }
        catch { Message = "Could not load items."; }
    }

    [RelayCommand]
    async Task Save()
    {
        if (string.IsNullOrWhiteSpace(ItemId) || string.IsNullOrWhiteSpace(ItemName)
            || string.IsNullOrWhiteSpace(ItemDescription))
        {
            await Shell.Current.DisplayAlert("Missing data",
                "Please enter Item ID, Item Name and Item Description.", "OK");
            return;
        }
        try
        {
            var ok = await _api.SaveItemAsync(new Item
            { ItemId = ItemId, ItemName = ItemName, ItemDescription = ItemDescription });
            if (ok)
            {
                ItemId = ItemName = ItemDescription = "";
                Message = "Saved.";
                await LoadAsync();
            }
            else Message = "Save failed (Item ID may already exist).";
        }
        catch { Message = "Save failed. Could not reach the server."; }
    }
}