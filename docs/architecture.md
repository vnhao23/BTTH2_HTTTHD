# Sơ đồ thành phần – QTKD.Api (tuần 05)

```mermaid
flowchart LR
    Client["Swagger / Postman"] -->|HTTP GET| C["Controllers<br/>CustomersController<br/>ContractsController"]
    C -->|gọi| S["Services<br/>QtkdQueryService"]
    S -->|LINQ| D["Data<br/>QtkdDbContext"]
    D -->|SQL qua Npgsql| DB[("PostgreSQL<br/>QtkdDb")]
    S -. trả DTO .-> C
```

| Lớp | Thư mục | Trách nhiệm |
|---|---|---|
| API | `Controllers/` | Nhận request, gọi service, trả 200/404 |
| Nghiệp vụ | `Services/` | Truy vấn và chuyển sang DTO; tuần 06 thêm kiểm tra CTR-01..03 |
| Truy cập dữ liệu | `Data/` | Ánh xạ entity ↔ bảng, quan hệ khóa ngoại, dữ liệu mẫu |

## Mô hình dữ liệu

```mermaid
erDiagram
    Customers ||--o{ Contracts : "CustomerId"
    PriceLists ||--o{ PriceItems : "PriceListId"
    Customers {
        int Id PK
        text Name
        text Status
    }
    Contracts {
        int Id PK
        text ContractNo UK
        int CustomerId FK
        date StartDate
        date EndDate
        text Status
    }
    PriceLists {
        int Id PK
        text Name
        date EffectiveDate
    }
    PriceItems {
        int Id PK
        int PriceListId FK
        text ServiceCode
        numeric UnitPrice
    }
```
