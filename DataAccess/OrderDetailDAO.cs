using BusinessObject;
using DataAccess.Generic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public class OrderDetailDAO : GenericDAO<OrderDetail>
    {
        SaleWPFAppContext _context;

        public OrderDetailDAO(SaleWPFAppContext context) : base(context)
        {
            _context = context;
        }

        public override bool Delete(int id)
        {
            //id == orderId
            try
            {
                var list_details = Get(detail => detail.OrderId == id);
                foreach(var detail in list_details)
                {
                    _context.Remove(detail);
                }
                _context.SaveChanges();
                return true;
            } catch
            {
                throw;
            }
            
        }
    }
}
