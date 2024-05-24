using FoodOrderDAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodOrderBAL.Interfaces
{
    public interface IAddHotelBranch
    {
        Task<HotelBranchModel> AddBranch(HotelBranchModel hotel);
        Task<HotelBranchModel> UpdateHotel(HotelBranchModel hotel);
        Task<List<HotelBranchModel>> GethotelDetials();
        Task<string> DeleteHotelbranch(int id, int hotelid);
    }
}
