using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;

public class ApiService
{
    static readonly string BaseUrl = DeviceInfo.Platform == DevicePlatform.Android
        ? "http://10.0.2.2:5000" : "http://localhost:5000";
    readonly HttpClient _http = new() { BaseAddress = new Uri(BaseUrl) };

    public async Task<bool> LoginAsync(string user, string pass)
    {
        var req = new HttpRequestMessage(HttpMethod.Get, "api/auth/login");
        var creds = Convert.ToBase64String(Encoding.UTF8.GetBytes($"{user}:{pass}"));
        req.Headers.Authorization = new AuthenticationHeaderValue("Basic", creds);
        var res = await _http.SendAsync(req);
        return res.IsSuccessStatusCode;
    }

    public async Task<List<Item>> GetItemsAsync() =>
        await _http.GetFromJsonAsync<List<Item>>("api/items") ?? new();

    public async Task<bool> SaveItemAsync(Item item)
    {
        var res = await _http.PostAsJsonAsync("api/items", item);
        return res.IsSuccessStatusCode;
    }
}