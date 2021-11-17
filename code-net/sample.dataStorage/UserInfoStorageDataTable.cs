using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using sample.Core.DataStorage;
using sample.Data.Entities;
using sample.Data.Options;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sample.DataStorage
{
    public class UserInfoStorageDataTable : IUserInfoStorageService
    {
        private readonly ILogger<UserInfoStorageDataTable> _logger;
        private readonly BaseConfig _baseConfig;
        private readonly SqlConnection _sqlConnection;
        private bool disposedValue;

        public UserInfoStorageDataTable(ILogger<UserInfoStorageDataTable> logger, IOptions<BaseConfig> options)
        {
            _logger = logger;
            _baseConfig = options.Value;
            _sqlConnection = new SqlConnection(options.Value.ConnectionString);
            _sqlConnection.Open();
        }

        public List<UserInfo> List()
        {
            DataTable dt = new DataTable();
            List<UserInfo> userInfos = new List<UserInfo>();
            SqlCommand cmd = new SqlCommand("Select * from UserInfos", _sqlConnection);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(dt);
            foreach (DataRow row in dt.Rows)
            {
                userInfos.Add(new UserInfo()
                {
                    Address = row["name"].ToString(),
                    Email = row["name"].ToString(),
                    Username = row["name"].ToString(),
                    Mobile = row["name"].ToString(),
                    FirstName = row["name"].ToString(),
                    Id = row["name"].ToString(),
                    LastName = row["name"].ToString(),
                });
            }
            return userInfos;
        }

        public void Add(UserInfo user)
        {
            string sql = "INSERT INTO UserInfos(Id,FirstName,LastName,Email,Mobile,Address) VALUES(@val1,@val2,@val3,@val4,@val5,@val6)";
            _sqlConnection.Open(); using (SqlCommand cmd = new SqlCommand(sql, _sqlConnection))
            {
                cmd.Parameters.Add("@val1", SqlDbType.VarChar, 50).Value = user.Id;
                cmd.Parameters.Add("@val2", SqlDbType.VarChar, 50).Value = user.FirstName;
                cmd.Parameters.Add("@val3", SqlDbType.VarChar, 50).Value = user.LastName;
                cmd.Parameters.Add("@val4", SqlDbType.VarChar, 50).Value = user.Email;
                cmd.Parameters.Add("@val5", SqlDbType.VarChar, 50).Value = user.Mobile;
                cmd.Parameters.Add("@val6", SqlDbType.VarChar, 50).Value = user.Address;
                cmd.CommandType = CommandType.Text;
                cmd.ExecuteNonQuery();
            }
        }

        public void Update(UserInfo user)
        {
            _logger.LogError("Called not implemented method");
            throw new NotImplementedException();
        }

        public UserInfo GetById(string id)
        {
            DataTable dt = new DataTable();
            SqlCommand cmd = new SqlCommand($"Select * from UserInfos where Id={id}", _sqlConnection);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(dt);
            if (dt.Rows.Count > 0)
            {
                DataRow row = dt.Rows[0];
                return new()
                {
                    Address = row["Address"].ToString(),
                    Email = row["Email"].ToString(),
                    Username = row["Username"].ToString(),
                    Mobile = row["Mobile"].ToString(),
                    FirstName = row["FirstName"].ToString(),
                    Id = row["Id"].ToString(),
                    LastName = row["LastName"].ToString(),
                };
            }
            return null;
        }

        public void Delete(string id)
        {
            string sql = $"DELETE FROM UserInfos WHERE id == { id}";
            using (SqlCommand cmd = new SqlCommand(sql, _sqlConnection))
            {
                cmd.CommandType = CommandType.Text;
                cmd.ExecuteNonQuery();
            }
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    _sqlConnection.Close();
                    _sqlConnection.Dispose();
                }

                disposedValue = true;
            }
        }

        public void Dispose()
        {
            // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }

        public UserInfo GetByUsername(string username)
        {
            DataTable dt = new DataTable();
            _sqlConnection.Open();
            SqlCommand cmd = new SqlCommand($"Select * from UserInfos where Username={username}", _sqlConnection);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(dt);
            if (dt.Rows.Count > 0)
            {
                DataRow row = dt.Rows[0];
                return new()
                {
                    Address = row["Address"].ToString(),
                    Email = row["Email"].ToString(),
                    Username = row["Username"].ToString(),
                    Mobile = row["Mobile"].ToString(),
                    FirstName = row["FirstName"].ToString(),
                    Id = row["Id"].ToString(),
                    LastName = row["LastName"].ToString(),
                };
            }
            return null;
        }
    }
}