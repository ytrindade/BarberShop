
using BarberShop.Domain.Extensions;
using BarberShop.Domain.Reports;
using BarberShop.Domain.Repositories.Revenues;
using ClosedXML.Excel;
using System.Globalization;

namespace BarberShop.Application.UseCases.Revenues.Reports.Excel;
public class GenerateRevenuesReportExcelUseCase : IGenerateRevenuesReportExcelUseCase
{

    public const string CURRENCY_SYMBOL = "R$";
    private readonly IRevenuesReadOnlyRepository _repository;

    public GenerateRevenuesReportExcelUseCase(IRevenuesReadOnlyRepository repository)
    {
        _repository = repository;
    }

    public async Task<byte[]> Execute(DateOnly month)
    {

        var revenues = await _repository.FilterByMonth(month);

        if (revenues.Count is 0) return [];

        var workbook = new XLWorkbook();

        workbook.Author = "Eu";
        workbook.Style.Font.FontSize = 12;
        workbook.Style.Font.FontName = "Times New Roman";

        var worksheet = workbook.Worksheets.Add(month.ToString("Y"));

        InsertHeader(worksheet);

        var raw = 2;
        revenues.ForEach(revenue =>
        {
            worksheet.Cell($"A{raw}").Value = revenue.Title;
            worksheet.Cell($"B{raw}").Value = revenue.Date;
            worksheet.Cell($"B{raw}").Style.DateFormat.Format = "dd/MM/yyyy";

            worksheet.Cell($"C{raw}").Value = revenue.PaymentType.PaymentTypeToString();

            worksheet.Cell($"D{raw}").Value = revenue.Amount;
            worksheet.Cell($"D{raw}").Style.NumberFormat.Format = $"{CURRENCY_SYMBOL} #,##0.00";

            worksheet.Cell($"E{raw}").Value = revenue.Description;
            raw++;
        });

        worksheet.Column(1).Width = 20;
        worksheet.Column(2).Width = 12;
        worksheet.Column(3).Width = 20;
        worksheet.Column(4).Width = 12;
        worksheet.Column(5).Width = 30;

        var file = new MemoryStream();
        workbook.SaveAs(file);

        return file.ToArray();

    }


    private void InsertHeader(IXLWorksheet worksheet)
    {
        worksheet.Cell("A1").Value = ResourceReportGenerationMessages.TITLE;
        worksheet.Cell("B1").Value = ResourceReportGenerationMessages.DATE;
        worksheet.Cell("C1").Value = ResourceReportGenerationMessages.PAYMENT_TYPE;
        worksheet.Cell("D1").Value = ResourceReportGenerationMessages.AMOUNT;
        worksheet.Cell("E1").Value = ResourceReportGenerationMessages.DESCRIPTION;

        worksheet.Cells("A1:E1").Style.Font.Bold = true;
        worksheet.Cells("A1:E1").Style.Fill.BackgroundColor = XLColor.FromHtml("#205858");
        worksheet.Cells("A1:E1").Style.Font.FontColor = XLColor.FromHtml("#FFFFFF");

        worksheet.Cell("A1").Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
        worksheet.Cell("B1").Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
        worksheet.Cell("C1").Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
        worksheet.Cell("D1").Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
        worksheet.Cell("E1").Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
    }
}
