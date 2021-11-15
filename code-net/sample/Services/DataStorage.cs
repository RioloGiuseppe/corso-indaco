using Microsoft.Extensions.Logging;
using sample.Data.Entities;
using sample.Services.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace sample.Services
{
    public class DataStorage : IDataStorage
    {
        private readonly ILogger<DataStorage> _logger;
        private readonly List<User> _users;
        private bool disposedValue;

        public DataStorage(ILogger<DataStorage> logger)
        {
            _logger = logger;
            _users = new();
        }

        public void AddUser(User u)
        {
            if (!_users.Any(o => o.Id == u.Id))
            {
                _users.Add(u);
                _logger.LogInformation($"Added user with id {u.Id}");
                return;
            }
            _logger.LogError($"User with id {u.Id} already exists");
        }

        public User GetUser(string id)
        {
            _logger.LogInformation($"Getting user with id {id}");
            var u = _users.FirstOrDefault(o => o.Id == id);
            if (u != null)
            {
                return u;
            }
            _logger.LogError($"User with id {u.Id} not found");
            return null;
        }


        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    _users.Clear();
                }

                // TODO: free unmanaged resources (unmanaged objects) and override finalizer
                // TODO: set large fields to null
                disposedValue = true;
            }
        }

        // // TODO: override finalizer only if 'Dispose(bool disposing)' has code to free unmanaged resources
        // ~DataStorage()
        // {
        //     // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
        //     Dispose(disposing: false);
        // }

        void IDisposable.Dispose()
        {
            // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }
    }
}
