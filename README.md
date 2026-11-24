# QTKD – Phase 2 (Tuần 05)

Web API cho hệ thống Quản trị Kinh doanh (QTKD): Customer, Contract, PriceList/PriceItem trên PostgreSQL.
Công nghệ: .NET 10, ASP.NET Core Web API, EF Core 10, Npgsql, Swagger.

## Cấu trúc

```
QTKD.slnx
src/QTKD.Api/
  Controllers/   Lớp API: nhận HTTP request, trả response
  Services/      Lớp nghiệp vụ: QtkdQueryService
  Data/          Lớp truy cập dữ liệu: QtkdDbContext (+ dữ liệu mẫu)
  Entities/      Customer, Contract, PriceList, PriceItem, ContractStatus
  DTOs/          Dữ liệu trả ra cho client
  Migrations/    Schema PostgreSQL do EF Core sinh
docs/            Sơ đồ thành phần, minh chứng
```

Sơ đồ thành phần: [docs/architecture.md](docs/architecture.md)

## Yêu cầu

- .NET SDK 10 (`dotnet --version`)
- PostgreSQL đang chạy (đã thử với PostgreSQL 18)
- Công cụ EF: `dotnet tool install --global dotnet-ef`

## Chạy dự án

### 1. Cấu hình database

`appsettings.json` để trống mật khẩu. Đặt chuỗi kết nối đầy đủ bằng user-secrets (lưu ngoài repo, không lên Git):

```powershell
dotnet user-secrets set "ConnectionStrings:DefaultConnection" "Host=localhost;Port=5432;Database=QtkdDb;Username=postgres;Password=<mat_khau_cua_ban>" --project src/QTKD.Api
```

Không cần tạo database `QtkdDb` trước, bước 2 sẽ tự tạo.

### 2. Tạo bảng và dữ liệu mẫu

```powershell
dotnet ef database update --project src/QTKD.Api
```

Lệnh này tạo 4 bảng `Customers`, `Contracts`, `PriceLists`, `PriceItems` và thêm dữ liệu mẫu:

| Bảng | Dữ liệu |
|---|---|
| Customers | 1 Công ty Minh Phát, 2 Công ty An Khang |
| Contracts | 1 HD-2026-018 – khách hàng 1 – Draft |
| PriceLists | 1 Bảng giá 2026 (hiệu lực 2026-01-01) |
| PriceItems | SVC-01 1.500.000, SVC-02 2.500.000 |

### 3. Khởi động API

```powershell
dotnet run --project src/QTKD.Api
```

Mở Swagger: http://localhost:5004/swagger

### 4. Thử endpoint

| Request | Kết quả mong đợi |
|---|---|
| `GET /api/customers` | 200, 2 khách hàng |
| `GET /api/contracts/1` | 200, HD-2026-018, `customerName` = Công ty Minh Phát, `status` = Draft |
| `GET /api/contracts/999` | 404 |

Có thể dùng file `src/QTKD.Api/QTKD.Api.http` (VS Code REST Client hoặc Visual Studio) thay cho Swagger.

Đối chiếu với database:

```sql
SELECT * FROM "Customers";
SELECT * FROM "Contracts" WHERE "Id" = 1;
```

## Giả định

- Phase 1 chưa có thiết kế PriceList/PriceItem; các trường đang dùng theo gợi ý của đề (Id, Name, EffectiveDate; PriceListId, ServiceCode, UnitPrice). Cần xác nhận lại.
- `Customer.Status` tạm dùng chuỗi `Active`; danh sách trạng thái khách hàng chưa chốt.
- `ContractStatus` đã có sẵn `RevisionRequested`, `PendingApproval`, `Approved`, `Rejected` cho tuần 06, tuần 05 chỉ dùng `Draft`.
