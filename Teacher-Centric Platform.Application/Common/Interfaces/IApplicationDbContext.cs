using Microsoft.EntityFrameworkCore;
namespace Teacher_Centric_Platform.Application.Common.Interfaces
{
    public interface IApplicationDbContext
    {
        
        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}
