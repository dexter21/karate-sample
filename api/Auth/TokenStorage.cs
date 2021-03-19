using System.Collections.Generic;

namespace api.Auth
{
    public interface ITokenStorage
    {
        void Add(string token);
        void Remove(string token);
        bool Validate(string token);
    }

    public class TokenStorage : ITokenStorage
    {
        private static readonly IDictionary<string, string> _storage = new Dictionary<string, string>();

        public void Add(string token)
            => _storage.Add(token, token);

        public void Remove(string token)
            => _storage.Remove(token);

        public bool Validate(string token)
            => _storage.ContainsKey(token);
    }
}