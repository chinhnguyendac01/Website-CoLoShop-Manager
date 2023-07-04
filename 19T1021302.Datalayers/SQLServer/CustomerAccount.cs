using _19T1021302.DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _19T1021302.Datalayers.SQLServer
{
    public class CustomerAccount : _BaseDAL, IUserAccountDAL
    {
        public CustomerAccount(string connectionString) : base(connectionString)
        {
        }

        public UserAccount Acthorize(string userName, string password)
        {
            throw new NotImplementedException();
        }

        public bool ChangePassword(string userName, string oldPassword, string newPassword)
        {
            throw new NotImplementedException();
        }
    }
}
