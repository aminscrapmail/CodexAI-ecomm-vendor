using EcommVendor.Api.Data;
using EcommVendor.Api.Models;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<AppDbContext>(options => options.UseInMemoryDatabase("EcommVendorDb"));
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAngular", policy =>
    {
        policy.WithOrigins("http://localhost:4200")
            .AllowAnyHeader()
            .AllowAnyMethod();
    });
});

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    SeedData(db);
}

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("AllowAngular");
app.UseHttpsRedirection();
app.MapControllers();

app.Run();

static void SeedData(AppDbContext db)
{
    if (db.Products.Any())
    {
        return;
    }

    var now = DateTime.UtcNow;
    db.Products.AddRange(
        new Product
        {
            Id = Guid.NewGuid(),
            Name = "Wireless Mouse",
            Category = "Accessories",
            Description = "Ergonomic wireless mouse",
            Stock = 57,
            Price = 29.99m,
            LastUpdated = now,
            IsDeleted = false,
            ModifiedBy = "system"
        },
        new Product
        {
            Id = Guid.NewGuid(),
            Name = "Mechanical Keyboard",
            Category = "Accessories",
            Description = "RGB mechanical keyboard",
            Stock = 31,
            Price = 89.49m,
            LastUpdated = now,
            IsDeleted = false,
            ModifiedBy = "system"
        }
    );

    db.SaveChanges();
}
