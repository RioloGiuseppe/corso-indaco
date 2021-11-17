using sample.Data.Entities;
using System;
using System.Collections.Generic;

namespace sample.Core.DataStorage
{
    public interface IUserInfoStorageService : IDisposable
    {
        void Add(UserInfo user);
        void Delete(string id);
        UserInfo GetById(string id);
        UserInfo GetByUsername(string id);
        List<UserInfo> List();
        void Update(UserInfo user);
    }
}