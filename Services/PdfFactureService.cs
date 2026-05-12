using facturationApp.Models;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;

namespace facturationApp.Services
{
    public class PdfFactureService
    {
        public byte[] GenererPdf(Facture facture)
        {
            QuestPDF.Settings.License = LicenseType.Community;

            return Document.Create(container =>
            {
                container.Page(page =>
                {
                    page.Size(PageSizes.A4);
                    page.Margin(2, Unit.Centimetre);
                    page.DefaultTextStyle(x => x.FontSize(10));

                    // HEADER
                    page.Header().Column(col =>
                    {
                        col.Item().Row(row =>
                        {
                            row.RelativeItem().Column(c =>
                            {
                                c.Item().Text("MON ENTREPRISE")
                                    .Bold().FontSize(16).FontColor("#1a237e");
                                c.Item().Text("Adresse : Tunis, Tunisie");
                                c.Item().Text("Tél : +216 XX XXX XXX");
                                c.Item().Text("MF : XXXXXXX/A/M/000");
                            });

                            row.RelativeItem().AlignRight().Column(c =>
                            {
                                c.Item().Background("#1a237e").Padding(10)
                                    .Text("FACTURE").Bold().FontSize(18)
                                    .FontColor(Colors.White);
                                c.Item().PaddingTop(5).Text($"N° {facture.Numero}")
                                    .Bold().FontSize(12);
                                c.Item().Text($"Date : {facture.DateFacture:dd/MM/yyyy}");
                            });
                        });

                        col.Item().PaddingTop(10).LineHorizontal(1).LineColor("#1a237e");
                    });

                    // CONTENT
                    page.Content().PaddingVertical(10).Column(col =>
                    {
                        // Infos client
                        col.Item().PaddingBottom(15).Row(row =>
                        {
                            row.RelativeItem();
                            row.RelativeItem().Border(1).BorderColor("#cccccc")
                                .Padding(10).Column(c =>
                                {
                                    c.Item().Text("FACTURÉ À :").Bold()
                                        .FontColor("#1a237e");
                                    c.Item().PaddingTop(5)
                                        .Text(facture.Client?.Nom ?? "").Bold();
                                    c.Item().Text(facture.Client?.Email ?? "");
                                    c.Item().Text(facture.Client?.Telephone ?? "");
                                    c.Item().Text(facture.Client?.Adresse ?? "");
                                });
                        });

                        // Tableau lignes
                        col.Item().Table(table =>
                        {
                            table.ColumnsDefinition(columns =>
                            {
                                columns.RelativeColumn(4); // Désignation
                                columns.RelativeColumn(2); // Prix HT
                                columns.RelativeColumn(1); // TVA
                                columns.RelativeColumn(1); // Qté
                                columns.RelativeColumn(2); // Total HT
                                columns.RelativeColumn(2); // Total TTC
                            });

                            // En-tête tableau
                            static IContainer HeaderCell(IContainer c) =>
                                c.Background("#1a237e").Padding(5);

                            table.Header(header =>
                            {
                                header.Cell().Element(HeaderCell)
                                    .Text("Désignation").Bold().FontColor(Colors.White);
                                header.Cell().Element(HeaderCell)
                                    .Text("Prix HT").Bold().FontColor(Colors.White);
                                header.Cell().Element(HeaderCell)
                                    .Text("TVA %").Bold().FontColor(Colors.White);
                                header.Cell().Element(HeaderCell)
                                    .Text("Qté").Bold().FontColor(Colors.White);
                                header.Cell().Element(HeaderCell)
                                    .Text("Total HT").Bold().FontColor(Colors.White);
                                header.Cell().Element(HeaderCell)
                                    .Text("Total TTC").Bold().FontColor(Colors.White);
                            });

                            // Lignes
                            var lignes = facture.Lignes.ToList();
                            for (int i = 0; i < lignes.Count; i++)
                            {
                                var ligne = lignes[i];
                                var bg = i % 2 == 0 ? "#ffffff" : "#f5f5f5";

                                static IContainer DataCell(IContainer c, string bg) =>
                                    c.Background(bg).BorderBottom(1)
                                     .BorderColor("#eeeeee").Padding(5);

                                table.Cell().Element(c => DataCell(c, bg))
                                    .Text(ligne.Produit?.Designation ?? "");
                                table.Cell().Element(c => DataCell(c, bg))
                                    .Text($"{ligne.PrixUnitaireHT:F3} TND");
                                table.Cell().Element(c => DataCell(c, bg))
                                    .Text($"{ligne.TauxTVA} %");
                                table.Cell().Element(c => DataCell(c, bg))
                                    .Text(ligne.Quantite.ToString());
                                table.Cell().Element(c => DataCell(c, bg))
                                    .Text($"{ligne.TotalHT:F3} TND");
                                table.Cell().Element(c => DataCell(c, bg))
                                    .Text($"{ligne.TotalTTC:F3} TND");
                            }
                        });

                        // Totaux
                        col.Item().PaddingTop(15).AlignRight().Width(250)
                            .Table(table =>
                            {
                                table.ColumnsDefinition(c =>
                                {
                                    c.RelativeColumn(2);
                                    c.RelativeColumn(2);
                                });

                                static IContainer TotalCell(IContainer c) =>
                                    c.BorderBottom(1).BorderColor("#eeeeee").Padding(5);

                                table.Cell().Element(TotalCell).Text("Total HT").Bold();
                                table.Cell().Element(TotalCell)
                                    .Text($"{facture.TotalHT:F3} TND").Bold();

                                table.Cell().Element(TotalCell).Text("TVA");
                                table.Cell().Element(TotalCell)
                                    .Text($"{facture.TotalTVA:F3} TND");

                                table.Cell().Element(TotalCell).Text("Timbre fiscal");
                                table.Cell().Element(TotalCell)
                                    .Text($"{facture.TimbreFiscal:F3} TND");

                                static IContainer TotalTTCCell(IContainer c) =>
                                    c.Background("#1a237e").Padding(5);

                                table.Cell().Element(TotalTTCCell)
                                    .Text("TOTAL TTC").Bold().FontColor(Colors.White);
                                table.Cell().Element(TotalTTCCell)
                                    .Text($"{facture.TotalTTC:F3} TND")
                                    .Bold().FontColor(Colors.White);
                            });

                        // Note timbre
                        col.Item().PaddingTop(10)
                            .Text($"* Timbre fiscal inclus : {facture.TimbreFiscal:F3} TND")
                            .Italic().FontSize(8).FontColor("#666666");
                    });

                    // FOOTER
                    page.Footer().AlignCenter().Column(col =>
                    {
                        col.Item().LineHorizontal(1).LineColor("#1a237e");
                        col.Item().PaddingTop(5).AlignCenter()
                            .Text("Merci pour votre confiance — Conformément à la législation fiscale tunisienne")
                            .FontSize(8).FontColor("#666666");
                    });
                });
            }).GeneratePdf();
        }
    }
}