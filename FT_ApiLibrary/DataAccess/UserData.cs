using FT_ApiLibrary.Internal.DataAccess;
using FT_ApiLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FT_ApiLibrary.DataAccess
{
    public class UserData : IUserData
    {
        private readonly ISqlDataAccess _sql;

        public UserData(ISqlDataAccess sql)
        {
            _sql = sql;
        }

        public UserModel GetUserById(string Id)
        {
            var output = _sql.LoadData<UserModel, dynamic>("dbo.proc_User_GetById", new { Id }, "FunTranslationDB").FirstOrDefault();

            return output;
        }


        public void SaveUser(UserModel user)
        {
            try
            {
                //Save in the database
                _sql.SaveData("dbo.Proc_User_Insert", user, "FunTranslationDB");
            }
            catch (Exception ex)
            {
                throw new Exception($"Could not save the user!\n{ex.Message}");
            }
        }
    }
}
