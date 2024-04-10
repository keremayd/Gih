using Entities;

namespace Repositories.Contracts;

public interface ICsoRepository
{
    Task<IEnumerable<Cso>> GetCso();
    Task<Cso?> GetCsoByIdAsync(int id);
    Task<Cso?> GetCsoByEmail(string email);
    Task<Cso?> GetCsoByUsername(string nickname);
    Task<Cso?> GetCsoByAddress(string address);
    void CreateCso(Cso restaurant);
    void UpdateCso(Cso restaurant);
    void UpdateCsoPassword(Cso restaurant);
    void DeleteCso(Cso restaurant);
}