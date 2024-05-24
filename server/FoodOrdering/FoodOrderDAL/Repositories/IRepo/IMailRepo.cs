using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodOrderDAL.Repositories.IRepo
{
    public interface IMailRepo
    {
        string SendMail(string senderMail);
        void SendOrderApproveMail(string ToMail);
    }
}
