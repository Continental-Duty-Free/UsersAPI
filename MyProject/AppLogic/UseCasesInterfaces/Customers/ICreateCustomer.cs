using UsersAPI.Domain.DTOs.Customers;

namespace UsersAPI.AppLogic.UseCasesInterfaces.Customers
{
    public interface ICreateCustomer
    {
        void Run(CreateCustomerDTO user);
    }
}
