using Microsoft.EntityFrameworkCore;
using QTKD.Api.Data;
using QTKD.Api.DTOs;

namespace QTKD.Api.Services;

// Lớp xử lý nghiệp vụ. Tuần 05 chỉ có thao tác đọc;
// tuần 06 sẽ thêm Create/Update/Submit và kiểm tra CTR-01..03 tại đây.
public class QtkdQueryService
{
    private readonly QtkdDbContext _db;

    public QtkdQueryService(QtkdDbContext db) => _db = db;

    public async Task<List<CustomerResponse>> GetCustomersAsync() =>
        await _db.Customers
            .AsNoTracking()
            .OrderBy(c => c.Id)
            .Select(c => new CustomerResponse(c.Id, c.Name, c.Status))
            .ToListAsync();

    // Trả null nếu không có -> Controller đổi thành 404.
    public async Task<ContractResponse?> GetContractAsync(int id) =>
        await _db.Contracts
            .AsNoTracking()
            .Where(c => c.Id == id)
            .Select(c => new ContractResponse(
                c.Id, c.ContractNo, c.CustomerId, c.Customer!.Name,
                c.StartDate, c.EndDate, c.Status.ToString()))
            .FirstOrDefaultAsync();
}
