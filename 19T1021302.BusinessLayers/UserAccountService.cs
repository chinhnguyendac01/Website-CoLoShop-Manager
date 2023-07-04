using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _19T1021302.Datalayers;
using _19T1021302.DomainModels;

namespace _19T1021302.BusinessLayers
{
    /// <summary>
    /// Các chức năng tác nghiệp liên quan đến tài khoản
    /// </summary>
    public static class UserAccountService
    {
        private static IUserAccountDAL employeeAccountDB;
        private static IUserAccountDAL customerAccountDB;
        static UserAccountService() ///Đặc điểm của static constructor là gì
        {
            string connectionString = ConfigurationManager.ConnectionStrings["DB"].ConnectionString;
            employeeAccountDB = new Datalayers.SQLServer.EmployeeAccount(connectionString);
            customerAccountDB = new Datalayers.SQLServer.CustomerAccount(connectionString);
        }

        public static  UserAccount Authorize(AccountTypes accountType, string userName,string password)
        {
            if (accountType == AccountTypes.Employee)
                return employeeAccountDB.Acthorize(userName, password);
            else
                return customerAccountDB.Acthorize(userName, password);
        }
        public static bool ChangePassword(AccountTypes accountType, string userName,string oldPassword,string newPassword)
        {
            if (accountType == AccountTypes.Employee)
                return employeeAccountDB.ChangePassword(userName, oldPassword, newPassword);
            else
                return employeeAccountDB.ChangePassword(userName, oldPassword, newPassword);
        }
    }
}
