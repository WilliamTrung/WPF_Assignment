using BusinessObject;
using BusinessObject.Enum;
using DataAccess.Generic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public class MemberDAO : GenericDAO<Member>
    {
        public MemberDAO(SaleWPFAppContext context) : base(context)
        {

        }

        public override Member Add(Member entity)
        {
            try
            {
                bool flag = true;
                if(entity.Email.Trim() == "")
                {
                    flag = false;
                } else if(entity.Password.Trim() == "")
                {
                    flag = false;
                } else if(entity.Country.Trim() == "")
                {
                    flag = false;
                } else if(entity.City.Trim() == "")
                {
                    flag = false;
                } else if (entity.CompanyName.Trim() == "")
                {
                    flag = false;
                }

                if (flag)
                {
                    Member? checkEmail = Get(member => member.Email.ToLower() == entity.Email.ToLower()).FirstOrDefault();
                    if(checkEmail != null)
                    {
                        throw new Exception("This email has existed!");
                    }
                }
                if (flag)
                {

                    return base.Add(entity);
                } else
                {
                    throw new Exception("Invalid member values!");
                }
                
            } catch
            {
                throw;
            }
            
        }
        public string Confirm(Member entity)
        {
            string result = "";
            result += "Email: "+ entity.Email + "\n";
            result += "Password: " + entity.Password + "\n";
            result += "Company: " + entity.CompanyName + "\n";
            result += "City: " + entity.City + "\n";
            result += "Country: " + entity.Country + "\n";
            result += "Role: " + Enum.GetName(typeof(Role), entity.Role) + "\n";
            return result;
        }
        public Member? Login(string email, string password)
        {
            Member? loginUser = Get(user => user.Email == email && user.Password == password).FirstOrDefault();
            return loginUser;
        }

        public override Member Update(int id, Member entity)
        {
            try
            {
                var current = Get(c => c.MemberId == id).FirstOrDefault();
                if (current == null)
                {
                    throw new Exception($"Member not found at id: {id}");
                }
                if (current.Email != entity.Email)
                {
                    Member? checkEmail = Get(member => member.Email.ToLower() == entity.Email.ToLower() && member.MemberId != current.MemberId).FirstOrDefault();
                    if (checkEmail == null)
                    {
                        current.Email = entity.Email;
                    }                        
                }
                if(current.City != entity.City)
                {
                    current.City = entity.City;
                }
                if (current.Country != entity.Country)
                {
                    current.Country = entity.Country;
                }
                if (current.CompanyName != entity.CompanyName)
                {
                    current.CompanyName = entity.CompanyName;
                }
                if (current.Password != entity.Password)
                {
                    current.Password = entity.Password;
                }
                if(current.Role != entity.Role)
                {
                    if(current.Role != BusinessObject.Enum.Role.User && entity.Role != BusinessObject.Enum.Role.User)
                    {
                        current.Role = entity.Role;
                    } else
                    {
                        throw new Exception("Role User cannot be changed or changed to!");
                    }
                    
                }
                return base.Update(id, entity);
            } catch
            {
                throw;
            }
            
        }
    }
}
