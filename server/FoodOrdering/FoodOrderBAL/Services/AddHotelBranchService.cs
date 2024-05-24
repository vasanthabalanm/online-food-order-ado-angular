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
    public class AddHotelBranchService : IAddHotelBranch
    {
        private readonly IAddBranchRepo _repo;
        public AddHotelBranchService(IAddBranchRepo repo)
        {
            _repo = repo;
        }

        public async Task<HotelBranchModel> AddBranch(HotelBranchModel hotel)
        {
            return await _repo.AddBranch(hotel);
        }

        public async Task<HotelBranchModel> UpdateHotel(HotelBranchModel hotel)
        {
            return await _repo.UpdateHotel(hotel);
        }

        public async Task<List<HotelBranchModel>> GethotelDetials()
        {
            return await _repo.GethotelDetials();
        }

        public async Task<string> DeleteHotelbranch(int id, int hotelid)
        {
            return await _repo.DeleteHotelbranch(id, hotelid);
        }
    }
}
