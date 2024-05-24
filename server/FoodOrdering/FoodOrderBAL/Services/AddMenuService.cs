using FoodOrderBAL.Interfaces;
using FoodOrderDAL.Models;
using FoodOrderDAL.Repositories.IRepo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodOrderBAL.Services
{
    public class AddMenuService : IAddMenu
    {
        private readonly IAddMenuRepo _repo;
        public AddMenuService(IAddMenuRepo repo)
        {
            _repo = repo;
        }

        public async Task<MenuDetailsModel> AddMenu(MenuDetailsModel menu)
        {
            return await _repo.AddMenu(menu);
        }

        public async Task<List<MenuDetailsModel>> GetMenuDetials()
        {
            return await _repo.GetMenuDetials();
        }

        public async Task<MenuDetailsModel> UpdateMenu(MenuDetailsModel menu)
        {
            return await _repo.UpdateMenu(menu);
        }

        public async Task<string> DeleteMenuDetails(int id, int hotelbranchId)
        {
            return await _repo.DeleteMenuDetails(id, hotelbranchId);
        }
    }
}
