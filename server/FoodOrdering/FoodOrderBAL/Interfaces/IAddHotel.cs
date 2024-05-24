using FoodOrderDAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodOrderBAL.Interfaces
{
    public interface IAddHotel
    {
        Task<HotelModel> AddHotels(HotelModel hotel);
        Task<List<HotelModel>> GethotelDetials();
        Task<string> DeleteHotelDetails(int id, string role);
        Task<HotelModel> UpdateHotel(HotelModel hotel);
    }
}
