using Microsoft.AspNetCore.Identity;
using UsersAPI.AppLogic.UseCasesInterfaces.Customers;
using UsersAPI.Domain.DTOs.Customers;
using UsersAPI.Domain.Entitys;
using UsersAPI.Domain.EntitysExceptions;
using UsersAPI.Domain.Mappers;
using UsersAPI.Domain.ReposInterfaces;

namespace UsersAPI.AppLogic.UseCasesImplementation.Customers
{
    public class CreateCustomer : ICreateCustomer
    {
        private IUserRepository repository;
        PasswordHasher<Person> passwordHasher;

        public CreateCustomer(IUserRepository repository)
        {
            this.repository = repository;
            this.passwordHasher = new PasswordHasher<Person>();
        }

        public void Run(CreateCustomerDTO dto)
        {
            if (repository.FindByEmail(dto.Email) != null)
                throw new UserException("Please be so kind of choosing another email");
            Person person = CreatePerson(dto);
            repository.Create(CustomerMapper.CreateCustomerDTO_To_Customer(dto, person));
        }

        public Person CreatePerson(CreateCustomerDTO dto)
        {
            Person person = new Person()
            {
                Password = dto.Password,
                Name = dto.Name,
                LastName = dto.LastName
            };
            person.Password = passwordHasher.HashPassword(person, person.Password);
            return person;
        }
    }
}
