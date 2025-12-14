using UsersAPI.Domain.DTOs.Customers;
using UsersAPI.Domain.DTOs.Users;
using UsersAPI.Domain.Entitys;

namespace UsersAPI.Domain.Mappers
{
    public class UserMapper
    {
        public static UserSimpleDTO User_To_UserSimpleDTO(User user)
        {
            return new UserSimpleDTO()
            {
                Email = user.Email
            };
        }
    }
}
