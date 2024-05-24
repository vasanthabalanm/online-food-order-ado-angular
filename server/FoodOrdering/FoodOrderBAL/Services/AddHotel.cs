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
    public class AddHotel : IAddHotel
    {
        private readonly IAddHotelRepo _repo;
        public AddHotel(IAddHotelRepo repo)
        {
            _repo = repo;
        }

        public async Task<HotelModel> AddHotels(HotelModel hotel)
        {
            return await _repo.AddHotel(hotel);
        }

        public async Task<string> DeleteHotelDetails(int id, string role)
        {
            return await _repo.DeleteHotelDetails(id, role);
        }

        public async Task<List<HotelModel>> GethotelDetials()
        {
            return await _repo.GethotelDetials();
        }
        public async Task<HotelModel> UpdateHotel(HotelModel hotel)
        {
            return await _repo.UpdateHotel(hotel);
        }
    }
}
