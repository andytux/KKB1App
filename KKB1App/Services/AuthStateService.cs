using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;

namespace KKB1App.Services
{
    public class AuthStateService
    {
        private readonly ProtectedSessionStorage protectedSessionStorage;

        public bool IsLoggedIn { get; set; }
        public string? UserName { get; set; }
        public Guid? UserId { get; set; }

        public event Action? AuthStateChanged;

        public AuthStateService(ProtectedSessionStorage protectedSessionStorage)
        {
            this.protectedSessionStorage = protectedSessionStorage;
        }

        public async Task Login(string userName, Guid userId)
        {
            IsLoggedIn = true;
            UserName = userName;
            UserId = userId;

            await protectedSessionStorage.SetAsync("UserName", UserName);
            await protectedSessionStorage.SetAsync("UserId", UserId);

            HandleAuthState();
        }

        public async Task Logout()
        {
            IsLoggedIn = false;
            UserName= null;
            UserId = null;

            await protectedSessionStorage.DeleteAsync("UserName");
            await protectedSessionStorage.DeleteAsync("UserId");

            HandleAuthState();
        }

        private void HandleAuthState()
        {
            AuthStateChanged?.Invoke();
        }

        public async Task InitializeAuthState()
        {
            var storedUserName = await protectedSessionStorage.GetAsync<string>("UserName");
            var storedUserId = await protectedSessionStorage.GetAsync<Guid>("UserId");

            if(storedUserId.Success && storedUserName.Success)
            {
                UserName = storedUserName.Value;
                UserId = storedUserId.Value;
                IsLoggedIn = true;

                HandleAuthState();
            }
        }
    }
}
