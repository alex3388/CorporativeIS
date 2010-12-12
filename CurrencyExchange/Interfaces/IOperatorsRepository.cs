using CurrencyExchange.Domain;

namespace CurrencyExchange.Interfaces
{
    public interface IOperatorsRepository
    {
        void Add(Operators op);
        void Update(Operators op);
        Operators getById(int id);
        Operators getByLogin(string login);
    }
}
