using EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Abstract
{
    public interface IAdminService
    {
        List<Admin> GetList();
        Admin GetUsernamePassword(string username, string password);
        void AdminAdd(Admin admin);
        Admin GetByID(int id);
        Admin GetByUserName(string username);
        void AdminDelete(Admin admin);
        void AdminUpdate(Admin admin);
    }
}
