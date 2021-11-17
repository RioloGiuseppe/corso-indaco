using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using sample.Core.DataStorage;
using sample.Data.Entities;
using sample.Data.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sample.DataStorage
{
    public class UserInfoStorageListService : IUserInfoStorageService
    {
        private readonly ILogger<UserInfoStorageListService> _logger;
        private readonly BaseConfig _baseConfig;
        private readonly List<UserInfo> _userInfos;
        private bool disposedValue;

        public UserInfoStorageListService(ILogger<UserInfoStorageListService> logger, IOptions<BaseConfig> options)
        {
            _logger = logger;
            _baseConfig = options.Value;
            _userInfos = new();
        }


        public void Add(UserInfo user)
        {
            _userInfos.Add(user);
        }

        public void Delete(string id)
        {
            var i = _userInfos.FirstOrDefault(o => o.Id == id);
            if (i != null)
            {
                _userInfos.Remove(i);
            }
        }

        public UserInfo GetById(string id)
        {
            return _userInfos.FirstOrDefault(o => o.Id == id);
        }

        public UserInfo GetByUsername(string username)
        {
            return _userInfos.FirstOrDefault(o => o.Username == username);
        }

        public List<UserInfo> List()
        {
            return _userInfos.ToList();
        }

        public void Update(UserInfo user)
        {
            throw new NotImplementedException();
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    _userInfos.Clear();
                }

                // TODO: free unmanaged resources (unmanaged objects) and override finalizer
                // TODO: set large fields to null
                disposedValue = true;
            }
        }

        // // TODO: override finalizer only if 'Dispose(bool disposing)' has code to free unmanaged resources
        // ~UserInfoStorageListService()
        // {
        //     // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
        //     Dispose(disposing: false);
        // }

        public void Dispose()
        {
            // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }


    }
}
