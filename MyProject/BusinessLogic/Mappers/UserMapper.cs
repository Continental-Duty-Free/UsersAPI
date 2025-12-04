using MyProject.BusinessLogic.Entitys;
using UsersAPI.BusinessLogic.DTOs.Users;

namespace UsersAPI.BusinessLogic.Mappers
{
    public class UserMapper
    {
        public static UserSimpleDTO User_To_UserSimpleDTO(User user)
        {
            return new UserSimpleDTO()
            {
                Email = user.Email,
                Username = user.Username,
                Name = user.Name
            };
        }

    }
}
