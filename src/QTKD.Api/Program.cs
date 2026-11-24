using Microsoft.EntityFrameworkCore;
using QTKD.Api.Data;
using QTKD.Api.Services;

var builder = WebApplication.CreateBuilder(args);

// Lớp API
builder.Services.AddControllers();
builder.Services.AddSwaggerGen();

// Lớp truy cập dữ liệu: chuỗi kết nối đọc từ appsettings + user-secrets (mật khẩu)
builder.Services.AddDbContext<QtkdDbContext>(opt =>
    opt.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

// Lớp nghiệp vụ
builder.Services.AddScoped<QtkdQueryService>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapControllers();
app.Run();
