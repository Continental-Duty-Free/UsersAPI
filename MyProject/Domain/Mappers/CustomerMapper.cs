using UsersAPI.Domain.DTOs.Customers;
using UsersAPI.Domain.Entitys;

namespace UsersAPI.Domain.Mappers
{
    public class CustomerMapper
    {
        public static Customer CreateCustomerDTO_To_Customer(CreateCustomerDTO dto, Person person)
        {
            return new Customer()
            {
                Email = dto.Email,
                Person = person
            };
        }
    }
}
