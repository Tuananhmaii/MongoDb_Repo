using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using MongoDb_Repo.Domain.Models;

public class UserService
{
    private readonly HttpClient _httpClient;

    public UserService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<bool> AddUserAsync(User user)
    {
        var response = await _httpClient.PostAsJsonAsync("api/user", user);
        return response.IsSuccessStatusCode;
    }

    public async Task<User> GetUserByEmailAsync(string email)
    {
        return await _httpClient.GetFromJsonAsync<User>($"api/user/{email}");
    }

    public async Task<List<User>> GetUserList()
    {
        return await _httpClient.GetFromJsonAsync<List<User>>($"api/user");
    }

    public async Task<bool> UpdateUserAsync(string id, User user)
    {
        var response = await _httpClient.PutAsJsonAsync($"api/user/{id}", user);
        return response.IsSuccessStatusCode;
    }

    // Add other methods as needed
}
