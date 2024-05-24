using FoodOrderDAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodOrderDAL.Repositories.IRepo
{
    public interface IAddHotelRepo
    {
        Task<HotelModel> AddHotel(HotelModel hotel);
        Task<List<HotelModel>> GethotelDetials();
        Task<string> DeleteHotelDetails(int id, string role);
        Task<HotelModel> UpdateHotel(HotelModel hotel);


    }
}
