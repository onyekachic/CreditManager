using System.Threading.Tasks;

namespace API.Interfaces
{
    public interface IUnitOfWork
    {
        IUserRepository UserRepository {get; }
        IMessageRepository MessageRepository {get;}
        ILikesRepository LikesRepository {get; }
        ICustomerRepository CustomerRepository {get; }
        ICreditRepository CreditRepository {get; }
        ICreditPayItemRepository CreditPayItemRepository {get; }
        Task<bool> Complete();
        bool HasChanges();
    }
}