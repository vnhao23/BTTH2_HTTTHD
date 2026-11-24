namespace QTKD.Api.DTOs;

// API trả DTO thay vì entity: tránh vòng lặp JSON Contract <-> Customer
// và chỉ lộ đúng những trường client cần.
public record CustomerResponse(int Id, string Name, string Status);

public record ContractResponse(
    int Id,
    string ContractNo,
    int CustomerId,
    string CustomerName,
    DateOnly StartDate,
    DateOnly EndDate,
    string Status);
