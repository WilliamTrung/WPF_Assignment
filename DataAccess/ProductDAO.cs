using BusinessObject;
using DataAccess.Generic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public class ProductDAO : GenericDAO<Product>
    {
        public ProductDAO(SaleWPFAppContext context) : base(context)
        {

        }
        public string Confirm(Product entity)
        {
            string result = "";
            result += "Name: " + entity.ProductName + "\n";
            result += "CategoryId: " + entity.CategoryId + "\n";
            result += "Weight: " + entity.Weight + "\n";
            result += "Price: " + entity.UnitPrice + "\n";
            result += "InStock: " + entity.UnitsInStock + "\n";
            return result;
        }
        public override Product Add(Product entity)
        {
            try
            {
                bool flag = true;
                if (entity.ProductName.Trim() == "")
                {
                    flag = false;
                } else if(entity.Weight.Trim() == "")
                {
                    flag = false;
                } else if(entity.UnitPrice < 0)
                {
                    flag = false;
                } else if(entity.UnitsInStock <= 0)
                {
                    flag = false;
                }

                if (flag)
                {
                    return base.Add(entity);
                }
                else
                {
                    throw new Exception("Invalid product values!");
                }

            }
            catch
            {
                throw;
            }
        }
    }
}
