
using E_Commece.DA.Context;
using E_Commece.DA.Implementations.Base;
using E_Commece.DA.Implementations.Repositories;
using E_Commerce.API.Middleware;
using E_Commerce.BL.AppServices;
using E_Commerce.BL.Contracts;
using E_Commerce.BL.Features.Orders.Validators;
using E_Commerce.Core.Interfaces.Bases;
using E_Commerce.Core.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;

namespace E_Commerce.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddControllers();
            // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddScoped<IOrderServices, OrderServices>();
            builder.Services.AddScoped(typeof(IBaseRepository<,>), typeof(BaseRepository<,>));
            builder.Services.AddScoped<IOrderRepository, OrderRepository>();
            builder.Services.AddDbContext<AppDbContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("local")));

           // builder.Services.AddDbContext<AppDbContext>();
           // builder.Services.AddScoped<DbContext>(sp => sp.GetRequiredService<AppDbContext>());
            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }
           // app.UseMiddleware<RateLimitingMiddleware>();
            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
