using Entities;
using Microsoft.EntityFrameworkCore;
using Repositories.Contracts;

namespace Repositories;

public class CsoRepository: RepositoryBase<Cso>, ICsoRepository
{
    public CsoRepository(ApplicationDbContext context) : base(context)
    {
    }

    public async Task<IEnumerable<Cso>> GetCso()
    {
        return await FindAll().ToListAsync();
    }

    public async Task<Cso?> GetCsoByIdAsync(int id)
    {
        return await FindByCondition(b => b.CsoId == id).SingleOrDefaultAsync(b => b.CsoId == id);
    }

    public async Task<Cso?> GetCsoByEmail(string email)
    {
        return await FindByCondition(b => b.Email.Equals(email)).SingleOrDefaultAsync();
    }

    public async Task<Cso?> GetCsoByUsername(string username)
    {
        return await FindByCondition(b => b.Username.Equals(username)).FirstOrDefaultAsync();
    }

    public async Task<Cso?> GetCsoByAddress(string address) =>
        await FindByCondition(b => b.Address.Equals(address)).SingleOrDefaultAsync();

    public void CreateCso(Cso restaurant)
    {
        Create(restaurant);
    }

    public void UpdateCso(Cso restaurant)
    {
        Update(restaurant);
    }

    public void UpdateCsoPassword(Cso restaurant)
    {
        Update(restaurant);
    }

    public void DeleteCso(Cso restaurant)
    {
        Delete(restaurant);
    }
}