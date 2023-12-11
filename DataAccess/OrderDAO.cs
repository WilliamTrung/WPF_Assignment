using BusinessObject;
using DataAccess.Generic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public class OrderDAO : GenericDAO<Order>
    {
        OrderDetailDAO _orderDetailDAO;
        public OrderDAO(SaleWPFAppContext context) : base(context)
        {
            _orderDetailDAO = new OrderDetailDAO(context);
        }
        public override IEnumerable<Order> Get(Expression<Func<Order, bool>>? filter = null, string? includeProperties = null)
        {
            var list = base.Get(filter, includeProperties);
            //list does not contain OrderDetails
            foreach (var item in list)
            {
                item.OrderDetails = _orderDetailDAO.Get(detail => detail.OrderId == item.OrderId).ToList();
            }
            return list;
        }
    }
}
