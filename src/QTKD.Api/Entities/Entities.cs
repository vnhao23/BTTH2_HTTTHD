namespace QTKD.Api.Entities;

// Trạng thái hợp đồng theo sơ đồ trạng thái Phase 1.
// Tuần 05 chỉ dùng Draft; các giá trị còn lại để sẵn cho CTR-01/CTR-03 ở tuần 06.
public enum ContractStatus
{
    Draft,
    RevisionRequested,
    PendingApproval,
    Approved,
    Rejected
}

public class Customer
{
    public int Id { get; set; }
    public string Name { get; set; } = "";
    public string Status { get; set; } = "Active";

    public List<Contract> Contracts { get; set; } = new();
}

public class Contract
{
    public int Id { get; set; }
    public string ContractNo { get; set; } = "";
    public int CustomerId { get; set; }
    public Customer? Customer { get; set; }
    public DateOnly StartDate { get; set; }
    public DateOnly EndDate { get; set; }
    public ContractStatus Status { get; set; } = ContractStatus.Draft;
}

public class PriceList
{
    public int Id { get; set; }
    public string Name { get; set; } = "";
    public DateOnly EffectiveDate { get; set; }

    public List<PriceItem> Items { get; set; } = new();
}

public class PriceItem
{
    public int Id { get; set; }
    public int PriceListId { get; set; }
    public PriceList? PriceList { get; set; }
    public string ServiceCode { get; set; } = "";
    public decimal UnitPrice { get; set; }
}
