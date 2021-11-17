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
    public class UserInfoStorageSqlService : IUserInfoStorageService
    {
        private readonly ILogger<UserInfoStorageSqlService> _logger;
        private readonly BaseConfig _baseConfig;
        private readonly SqlConnection _sqlConnection;
        private bool disposedValue;

        public UserInfoStorageSqlService(ILogger<UserInfoStorageSqlService> logger, IOptions<BaseConfig> options)
        {
            _logger = logger;
            _baseConfig = options.Value;
            _sqlConnection = new SqlConnection(options.Value.ConnectionString);
            _sqlConnection.Open();
        }

        public List<UserInfo> List()
        {
            List<UserInfo> lstStudent = new List<UserInfo>();

            SqlCommand cmd = new SqlCommand("SELECT * FROM UserInfos", _sqlConnection);
            cmd.CommandType = CommandType.Text;
            SqlDataReader rdr = cmd.ExecuteReader();

            while (rdr.Read())
            {
                UserInfo user = new UserInfo();
                user.Id = rdr["Id"].ToString();
                user.FirstName = rdr["FirstName"].ToString();
                user.LastName = rdr["LastName"].ToString();
                user.Email = rdr["Email"].ToString();
                user.Mobile = rdr["Mobile"].ToString();
                user.Address = rdr["Address"].ToString();

                lstStudent.Add(user);
            }
            return lstStudent;
        }

        public void Add(UserInfo user)
        {
            string sql = "INSERT INTO UserInfos(Id,FirstName,LastName,Email,Mobile,Address) VALUES(@val1,@val2,@val3,@val4,@val5,@val6)";
            _sqlConnection.Open(); 
            using (SqlCommand cmd = new SqlCommand(sql, _sqlConnection))
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
            UserInfo user = new UserInfo();

            string sqlQuery = "SELECT * FROM UserInfos WHERE Id= " + id;
            SqlCommand cmd = new SqlCommand(sqlQuery, _sqlConnection);
            SqlDataReader rdr = cmd.ExecuteReader();

            if (!rdr.HasRows)
            {
                return null;
            }

            while (rdr.Read())
            {
                user.Id = rdr["Id"].ToString();
                user.FirstName = rdr["FirstName"].ToString();
                user.Username = rdr["Username"].ToString();
                user.LastName = rdr["LastName"].ToString();
                user.Email = rdr["Email"].ToString();
                user.Mobile = rdr["Mobile"].ToString();
                user.Address = rdr["Address"].ToString();
            }
            return user;
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
            UserInfo user = new UserInfo();

            string sqlQuery = "SELECT * FROM UserInfos WHERE Username= " + username;
            SqlCommand cmd = new SqlCommand(sqlQuery, _sqlConnection);
            SqlDataReader rdr = cmd.ExecuteReader();
            
            if (!rdr.HasRows)
            {
                return null;
            }

            while (rdr.Read())
            {
                user.Id = rdr["Id"].ToString();
                user.FirstName = rdr["FirstName"].ToString();
                user.Username = rdr["Username"].ToString();
                user.LastName = rdr["LastName"].ToString();
                user.Email = rdr["Email"].ToString();
                user.Mobile = rdr["Mobile"].ToString();
                user.Address = rdr["Address"].ToString();
            }
            return user;
        }
    }
}
