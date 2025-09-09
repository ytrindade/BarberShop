using BarberShop.Application.AutoMapper;
using BarberShop.Application.UseCases.Revenues.Delete;
using BarberShop.Application.UseCases.Revenues.GetAll;
using BarberShop.Application.UseCases.Revenues.GetById;
using BarberShop.Application.UseCases.Revenues.Register;
using BarberShop.Application.UseCases.Revenues.Reports.Excel;
using BarberShop.Application.UseCases.Revenues.Reports.Pdf;
using BarberShop.Application.UseCases.Revenues.Update;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BarberShop.Application;
public static class DependencyInjection
{
    public static void AddApplication(this IServiceCollection services)
    {
        AddRepositories(services);
        AddAutoMapper(services);
    }

    private static void AddRepositories(IServiceCollection services)
    {
        services.AddScoped<IRegisterRevenueUseCase, RegisterRevenueUseCase>();
        services.AddScoped<IGetRevenuesUseCase, GetRevenuesUseCase>();
        services.AddScoped<IGetRevenueByIdUseCase, GetRevenueByIdUseCase>();
        services.AddScoped<IDeleteRevenueUseCase, DeleteRevenueUseCase>();
        services.AddScoped<IUpdateRevenueUseCase, UpdateRevenueUseCase>();
        services.AddScoped<IGenerateRevenuesReportExcelUseCase, GenerateRevenuesReportExcelUseCase>();
        services.AddScoped<IGenerateRevenuesReportPdfUseCase, GenerateRevenuesReportPdfUseCase>();
    }

    private static void AddAutoMapper(IServiceCollection services)
    {
        services.AddAutoMapper(typeof(AutoMapping));
    }
}
