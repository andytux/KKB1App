using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;

namespace KKB1App.Services
{
    public class AuthStateService
    {
        private readonly ProtectedSessionStorage storage;

        public bool IsLoggedIn { get; set; } = false;
        public string? UserName { get; set; } = null;
        public Guid? UserId { get; set; } = null;

        public event Action? AuthStateChanged;

        public AuthStateService(ProtectedSessionStorage storage)
        {
            this.storage = storage;
        }

        /// <summary>
        /// Initialisiert den Authorisationsstatus falls man die seite aktualisiert werden aus dem storage die values geladen
        /// </summary>
        /// <returns></returns>
        public async Task InitializeAuthState()
        {
            var storedUserId = await storage.GetAsync<Guid>(nameof(UserId));
            var storedUserName = await storage.GetAsync<string>(nameof(UserName));

            if (storedUserId.Success && storedUserName.Success)
            {
                UserId = storedUserId.Value;
                UserName = storedUserName.Value;
                IsLoggedIn = true;

                HandleAuthStateChanged();
            }
        }

        /// <summary>
        /// Loggt den User ein und speichert die values im Storage
        /// </summary>
        /// <param name="username"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        public async Task Login(string username, Guid userId)
        {
            IsLoggedIn = true;
            UserId = userId;
            UserName = username;

            await storage.SetAsync(nameof(UserId), userId);
            await storage.SetAsync(nameof(UserName), username);

            HandleAuthStateChanged();
        }

        /// <summary>
        /// Loggt den User aus und entfernt die values aus dem storage
        /// </summary>
        /// <returns></returns>
        public async Task Logout()
        {
            IsLoggedIn = false;
            UserId = null;
            UserName = null;

            await storage.DeleteAsync(nameof(UserId));
            await storage.DeleteAsync(nameof(UserName));

            HandleAuthStateChanged();
        }

        private void HandleAuthStateChanged()
        {
            AuthStateChanged?.Invoke();
        }

    }
}

