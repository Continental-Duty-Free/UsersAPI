using Microsoft.AspNetCore.Identity;
using MyProject.AppLogic.UseCasesInterfaces.Users;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using UsersAPI.Domain.DTOs.Users;
using UsersAPI.Domain.Entitys;
using UsersAPI.Domain.Mappers;
using UsersAPI.Domain.ReposInterfaces;

namespace MyProject.AppLogic.UseCasesImplementation.Users
{
    public class CreateUser : ICreateUser
    {
        private IUserRepository repository;
        PasswordHasher<Person> passwordHasher;

        public CreateUser(IUserRepository repository)
        {
            this.repository = repository;
            this.passwordHasher = new PasswordHasher<Person>();
        }

        public void Run(CreateUserDTO dto)
        {
            if (repository.FindByEmail(dto.Email) != null)
                throw new Exception("Please be so kind of choosing another email");
            var createdUser = repository.Create(UserMapper.CreateUserDTO_To_User(dto));
            CreatePerson(dto, createdUser);
        }

        public void CreatePerson(CreateUserDTO dto, User user)
        {   
            Person person = new Person()
            {
                UserId = user.Id,
                Password = dto.Password,
                Name = dto.Name,
                LastName = dto.LastName
            };
            person.Password = passwordHasher.HashPassword(person, person.Password); ;
            repository.CreatePerson(user, person);
        }
    }
}