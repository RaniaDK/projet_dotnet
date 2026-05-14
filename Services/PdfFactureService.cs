using facturationApp.Models;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;

namespace facturationApp.Services
{
    public class PdfFactureService
    {
        private readonly IEntrepriseService _entrepriseService;

        public PdfFactureService(IEntrepriseService entrepriseService)
        {
            _entrepriseService = entrepriseService;
        }

        public async Task<byte[]> GenererPdfAsync(Facture facture)
        {
            var entreprise = await _entrepriseService.GetAsync();
            QuestPDF.Settings.License = LicenseType.Community;

            return Document.Create(container =>
            {
                container.Page(page =>
                {
                    page.Size(PageSizes.A4);
                    page.Margin(2, Unit.Centimetre);
                    page.DefaultTextStyle(x => x.FontSize(10));

                    // ===== HEADER =====
                    page.Header().Column(col =>
                    {
                        col.Item().Row(row =>
                        {
                            row.RelativeItem().Column(c =>
                            {
                                c.Item().Text(entreprise.Nom)
                                    .Bold().FontSize(16).FontColor("#1a237e");
                                c.Item().PaddingTop(4)
                                    .Text($"Adresse : {entreprise.Adresse}" +
                                          (!string.IsNullOrEmpty(entreprise.CodePostal)
                                              ? $", {entreprise.CodePostal}" : "") +
                                          (!string.IsNullOrEmpty(entreprise.Ville)
                                              ? $" {entreprise.Ville}" : ""));
                                c.Item().Text($"Tél : {entreprise.Telephone}");
                                if (!string.IsNullOrEmpty(entreprise.Email))
                                    c.Item().Text($"Email : {entreprise.Email}");
                                if (!string.IsNullOrEmpty(entreprise.MatriculeFiscal))
                                    c.Item().Text($"MF : {entreprise.MatriculeFiscal}")
                                        .Bold();
                            });

                            row.RelativeItem().AlignRight().Column(c =>
                            {
                                c.Item().Background("#1a237e").Padding(10)
                                    .Text("FACTURE").Bold().FontSize(18)
                                    .FontColor(Colors.White);
                                c.Item().PaddingTop(5)
                                    .Text($"N° {facture.Numero}")
                                    .Bold().FontSize(12);
                                c.Item().Text(
                                    $"Date : {facture.DateFacture:dd/MM/yyyy}");
                            });
                        });

                        col.Item().PaddingTop(10)
                            .LineHorizontal(1).LineColor("#1a237e");
                    });

                    // ===== CONTENT =====
                    page.Content().PaddingVertical(10).Column(col =>
                    {
                        // Bloc client
                        col.Item().PaddingBottom(15).Row(row =>
                        {
                            row.RelativeItem();
                            row.RelativeItem()
                                .Border(1).BorderColor("#cccccc")
                                .Padding(10).Column(c =>
                                {
                                    c.Item().Text("FACTURÉ À :")
                                        .Bold().FontColor("#1a237e");

                                    c.Item().PaddingTop(5)
                                        .Text(facture.Client?.Nom ?? "")
                                        .Bold().FontSize(11);

                                    if (!string.IsNullOrWhiteSpace(
                                            facture.Client?.MatriculeFiscal))
                                        c.Item().PaddingTop(2)
                                            .Text($"MF : {facture.Client.MatriculeFiscal}")
                                            .Bold().FontColor("#c62828");

                                    c.Item().PaddingTop(2)
                                        .Text(facture.Client?.Email ?? "");
                                    c.Item().Text(facture.Client?.Telephone ?? "");
                                    c.Item().Text(facture.Client?.Adresse ?? "");
                                });
                        });

                        // Tableau des lignes
                        col.Item().Table(table =>
                        {
                            table.ColumnsDefinition(columns =>
                            {
                                columns.RelativeColumn(4);
                                columns.RelativeColumn(2);
                                columns.RelativeColumn(1);
                                columns.RelativeColumn(1);
                                columns.RelativeColumn(2);
                                columns.RelativeColumn(2);
                            });

                            static IContainer HeaderCell(IContainer c) =>
                                c.Background("#1a237e").Padding(5);

                            table.Header(header =>
                            {
                                header.Cell().Element(HeaderCell)
                                    .Text("Désignation").Bold()
                                    .FontColor(Colors.White);
                                header.Cell().Element(HeaderCell)
                                    .Text("Prix HT").Bold()
                                    .FontColor(Colors.White);
                                header.Cell().Element(HeaderCell)
                                    .Text("TVA %").Bold()
                                    .FontColor(Colors.White);
                                header.Cell().Element(HeaderCell)
                                    .Text("Qté").Bold()
                                    .FontColor(Colors.White);
                                header.Cell().Element(HeaderCell)
                                    .Text("Total HT").Bold()
                                    .FontColor(Colors.White);
                                header.Cell().Element(HeaderCell)
                                    .Text("Total TTC").Bold()
                                    .FontColor(Colors.White);
                            });

                            var lignes = facture.Lignes?.ToList()
                                         ?? new List<LigneFacture>();

                            for (int i = 0; i < lignes.Count; i++)
                            {
                                var ligne = lignes[i];
                                var bg = i % 2 == 0 ? "#ffffff" : "#f5f5f5";

                                IContainer DataCell(IContainer c) =>
                                    c.Background(bg)
                                     .BorderBottom(1).BorderColor("#eeeeee")
                                     .Padding(5);

                                table.Cell().Element(DataCell)
                                    .Text(ligne.Produit?.Designation ?? "");
                                table.Cell().Element(DataCell)
                                    .Text($"{ligne.PrixUnitaireHT:F3} TND");
                                table.Cell().Element(DataCell)
                                    .Text($"{ligne.TauxTVA} %");
                                table.Cell().Element(DataCell)
                                    .Text(ligne.Quantite.ToString());
                                table.Cell().Element(DataCell)
                                    .Text($"{ligne.TotalHT:F3} TND");
                                table.Cell().Element(DataCell)
                                    .Text($"{ligne.TotalTTC:F3} TND");
                            }
                        });

                        // Bloc totaux
                        col.Item().PaddingTop(15).AlignRight().Width(260)
                            .Table(table =>
                            {
                                table.ColumnsDefinition(c =>
                                {
                                    c.RelativeColumn(2);
                                    c.RelativeColumn(2);
                                });

                                static IContainer TotalCell(IContainer c) =>
                                    c.BorderBottom(1).BorderColor("#eeeeee").Padding(6);

                                static IContainer TTCCell(IContainer c) =>
                                    c.Background("#1a237e").Padding(6);

                                table.Cell().Element(TotalCell)
                                    .Text("Total HT").Bold();
                                table.Cell().Element(TotalCell)
                                    .Text($"{facture.TotalHT:F3} TND").Bold();

                                table.Cell().Element(TotalCell).Text("TVA");
                                table.Cell().Element(TotalCell)
                                    .Text($"{facture.TotalTVA:F3} TND");

                                table.Cell().Element(TotalCell)
                                    .Text("Timbre fiscal");
                                table.Cell().Element(TotalCell)
                                    .Text($"{facture.TimbreFiscal:F3} TND");

                                table.Cell().Element(TTCCell)
                                    .Text("TOTAL TTC").Bold()
                                    .FontColor(Colors.White);
                                table.Cell().Element(TTCCell)
                                    .Text($"{facture.TotalTTC:F3} TND")
                                    .Bold().FontColor(Colors.White);
                            });

                        // Note timbre
                        col.Item().PaddingTop(10)
                            .Text($"* Timbre fiscal inclus : " +
                                  $"{facture.TimbreFiscal:F3} TND")
                            .Italic().FontSize(8).FontColor("#666666");
                    });

                    // ===== FOOTER =====
                    page.Footer().AlignCenter().Column(col =>
                    {
                        col.Item().LineHorizontal(1).LineColor("#1a237e");
                        col.Item().PaddingTop(5).AlignCenter()
                            .Text("Merci pour votre confiance — " +
                                  "Conformément à la législation fiscale tunisienne")
                            .FontSize(8).FontColor("#666666");
                    });
                });
            }).GeneratePdf();
        }
    }
}