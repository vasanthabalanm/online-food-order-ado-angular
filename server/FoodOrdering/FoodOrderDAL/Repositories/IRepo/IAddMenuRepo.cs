using FoodOrderDAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodOrderDAL.Repositories.IRepo
{
    public interface IAddMenuRepo
    {
        Task<MenuDetailsModel> AddMenu(MenuDetailsModel menu);
        Task<List<MenuDetailsModel>> GetMenuDetials();
        Task<MenuDetailsModel> UpdateMenu(MenuDetailsModel menu);
        Task<string> DeleteMenuDetails(int id, int hotelbranchId);
    }
}
