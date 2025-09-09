using BarberShop.Application.UseCases.Revenues.Reports.Pdf.Colors;
using BarberShop.Application.UseCases.Revenues.Reports.Pdf.Fonts;
using BarberShop.Domain.Extensions;
using BarberShop.Domain.Reports;
using BarberShop.Domain.Repositories.Revenues;
using MigraDoc.DocumentObjectModel;
using MigraDoc.DocumentObjectModel.Tables;
using MigraDoc.Rendering;
using PdfSharp.Fonts;
using System.Reflection;

namespace BarberShop.Application.UseCases.Revenues.Reports.Pdf;
public class GenerateRevenuesReportPdfUseCase : IGenerateRevenuesReportPdfUseCase
{
    public const string CURRENCY_SYMBOL = "R$";
    private const int HEIGHT_ROW_REVENUE_TABLE = 25;
    private readonly IRevenuesReadOnlyRepository _repository;

    public GenerateRevenuesReportPdfUseCase(IRevenuesReadOnlyRepository repository)
    {
        _repository = repository;

        GlobalFontSettings.FontResolver = new RevenuesReportFontResolver();
    }
 

    public async Task<byte[]> Execute(DateOnly month)
    {
        var revenues = await _repository.FilterByMonth(month);

        if (revenues.Count is 0)
            return [];

        var document = CreateDocument(month);
        var page = CreatePage(document);

        CreateHeader(page);

        var totalRevenues = revenues.Sum(e => e.Amount);

        CreateMonthlyRevenueSection(page, month, totalRevenues);

        revenues.ForEach(revenue =>
        {
            var table = CreateRevenueTable(page);

            var row = table.AddRow();
            row.Height = HEIGHT_ROW_REVENUE_TABLE;

            AddRevenueTable(row.Cells[0], revenue.Title);

            AddHeaderForAmount(row.Cells[3]);


            row = table.AddRow();
            row.Height = HEIGHT_ROW_REVENUE_TABLE;

            row.Cells[0].AddParagraph(revenue.Date.ToString("D"));
            SetStyleBaseForRevenueInformation(row.Cells[0]);
            row.Cells[0].Format.LeftIndent = 10;

            row.Cells[1].AddParagraph(revenue.Date.ToString("t"));
            SetStyleBaseForRevenueInformation(row.Cells[1]);

            row.Cells[2].AddParagraph(revenue.PaymentType.PaymentTypeToString());
            SetStyleBaseForRevenueInformation(row.Cells[2]);


            AddAmountForRevenue(row.Cells[3], revenue.Amount);

            if (!string.IsNullOrWhiteSpace(revenue.Description))
            {
                var descriptionRow = table.AddRow();
                descriptionRow.Height = HEIGHT_ROW_REVENUE_TABLE;

                descriptionRow.Cells[0].AddParagraph(revenue.Description);
                descriptionRow.Cells[0].Format.Font = new Font { Name = FontHelper.WORKSANS_REGULAR, Size = 10, Color = ColorsHelper.BLACK };
                descriptionRow.Cells[0].Shading.Color = ColorsHelper.GREEN_4;
                descriptionRow.Cells[0].VerticalAlignment = VerticalAlignment.Center;
                descriptionRow.Cells[0].MergeRight = 2;
                descriptionRow.Cells[0].Format.LeftIndent = 10;

                row.Cells[3].MergeDown = 1;
            }

            AddWhiteSpace(table);
        });


        return RenderDocument(document);
    }

    private Document CreateDocument(DateOnly month)
    {
        var document = new Document();
        document.Info.Title = $"{ResourceReportGenerationMessages.REVENUES_FOR} {month:Y}";
        document.Info.Author = "Eu";

        var style = document.Styles["Normal"];
        style!.Font.Name = FontHelper.RALEWAY_REGULAR;

        return document;
    }

    private Section CreatePage(Document document)
    {
        var section = document.AddSection();
        section.PageSetup = document.DefaultPageSetup.Clone();

        section.PageSetup.PageFormat = PageFormat.A4;
        section.PageSetup.RightMargin = 40;
        section.PageSetup.LeftMargin = 40;
        section.PageSetup.TopMargin = 40;
        section.PageSetup.BottomMargin = 40;

        return section;
    }

    private void CreateHeader(Section page)
    {
        var table = page.AddTable();

        table.AddColumn();
        table.AddColumn("300");

        var row = table.AddRow();

        var assembly = Assembly.GetExecutingAssembly();
        var directoryName = Path.GetDirectoryName(assembly.Location);
        var pathFile = Path.Combine(directoryName!, "UseCases", "Revenues", "Reports", "Pdf", "Logo", "BarberPhoto.png");
        row.Cells[0].AddImage(pathFile);
       
        row.Cells[1].AddParagraph("BARBERSHOP");
        row.Cells[1].Format.Font = new Font { Name = FontHelper.ROBOTO_BLACK, Size = 18, Color = ColorsHelper.GREEN_1 };
        row.Cells[1].VerticalAlignment = MigraDoc.DocumentObjectModel.Tables.VerticalAlignment.Center;
        row.Cells[1].Format.LeftIndent = 30;
    }

    private void CreateMonthlyRevenueSection(Section page, DateOnly month, decimal totalRevenues)
    {
        var paragraph = page.AddParagraph();
        paragraph.Format.SpaceBefore = "30";
        paragraph.Format.SpaceAfter = "30";

        var title = string.Format(ResourceReportGenerationMessages.MONTHLY_REVENUE, month.ToString("Y"));

        paragraph.AddFormattedText(title, new Font { Name = FontHelper.ROBOTO_BLACK, Size = 15, Color = ColorsHelper.GREEN_1 });

        paragraph.AddLineBreak();

        paragraph.AddFormattedText($"{CURRENCY_SYMBOL} {totalRevenues}", new Font { Name = FontHelper.ROBOTO_BLACK, Size = 50, Color = ColorsHelper.GREEN_1 });
    }

    private Table CreateRevenueTable(Section page)
    {
        var table = page.AddTable();

        table.AddColumn("195").Format.Alignment = ParagraphAlignment.Left;
        table.AddColumn("80").Format.Alignment = ParagraphAlignment.Center;
        table.AddColumn("120").Format.Alignment = ParagraphAlignment.Center;
        table.AddColumn("120").Format.Alignment = ParagraphAlignment.Right;
        return table;
    }

    private void AddRevenueTable(Cell cell, string revenueTitle)
    {
        cell.AddParagraph(revenueTitle);
        cell.Format.Font = new Font { Name = FontHelper.RALEWAY_BLACK, Size = 14, Color = ColorsHelper.WHITE };
        cell.Shading.Color = ColorsHelper.GREEN_1;
        cell.VerticalAlignment = VerticalAlignment.Center;
        cell.MergeRight = 2;
        cell.Format.LeftIndent = 10;
    }

    private void AddHeaderForAmount(Cell cell)
    {
        cell.AddParagraph(ResourceReportGenerationMessages.AMOUNT);
        cell.Format.Font = new Font { Name = FontHelper.RALEWAY_BLACK, Size = 14, Color = ColorsHelper.WHITE };
        cell.Shading.Color = ColorsHelper.GREEN_2;
        cell.VerticalAlignment = VerticalAlignment.Center;
    }

    private void SetStyleBaseForRevenueInformation(Cell cell)
    {
        cell.Format.Font = new Font { Name = FontHelper.WORKSANS_REGULAR, Size = 12, Color = ColorsHelper.BLACK };
        cell.Shading.Color = ColorsHelper.GREEN_3;
        cell.VerticalAlignment = VerticalAlignment.Center;
    }

    private void AddAmountForRevenue(Cell cell, decimal amount)
    {
        cell.AddParagraph($"{CURRENCY_SYMBOL} {amount}");
        cell.Format.Font = new Font { Name = FontHelper.WORKSANS_REGULAR, Size = 14, Color = ColorsHelper.BLACK };
        cell.Shading.Color = ColorsHelper.WHITE;
        cell.VerticalAlignment = VerticalAlignment.Center;
    }

    private void AddWhiteSpace(Table table)
    {
        var row = table.AddRow();
        row.Height = 30;
        row.Borders.Visible = false;
    }

    private byte[] RenderDocument(Document document)
    {
        var renderer = new PdfDocumentRenderer
        {
            Document = document,
        };

        renderer.RenderDocument();

        using var file = new MemoryStream();
        renderer.PdfDocument.Save(file);

        return file.ToArray();
    }
}
