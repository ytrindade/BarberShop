namespace BarberShop.Application.UseCases.Revenues.Reports.Excel;
public interface IGenerateRevenuesReportExcelUseCase
{
    Task<byte[]> Execute(DateOnly month);
}
