using iTextSharp.text;
using iTextSharp.text.pdf;
using Online_Marketplace.Shared.DTOs;

namespace Online_Marketplace.BLL.Extension
{

    public class ReceiptGenerator
    {
        public MemoryStream GenerateReceipt(ReceiptDto receipt)
        {
            // Create a new PDF document
            var document = new Document(PageSize.A4, 50, 50, 25, 25);
            var stream = new MemoryStream();
            var writer = PdfWriter.GetInstance(document, stream);

            // Open the document
            document.Open();

            // Add content to the document
            var headerFont = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 20);
            var titleFont = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 18);
            var itemFont = FontFactory.GetFont(FontFactory.HELVETICA, 12);

            // Header
            var logoPath = "https://zenithexchange.ltd/assets/img/log.png";
            var logo = Image.GetInstance(logoPath);
            logo.ScaleAbsolute(70f, 70f);
            logo.Alignment = Element.ALIGN_LEFT;

            var businessName = new Paragraph("MOTION STORE", headerFont);
            businessName.Alignment = Element.ALIGN_CENTER;

            var headerTable = new PdfPTable(2);
            headerTable.WidthPercentage = 100;
            headerTable.SpacingBefore = 10f;
            headerTable.SpacingAfter = 10f;
            headerTable.HorizontalAlignment = Element.ALIGN_CENTER;

            var cell = new PdfPCell(logo);
            cell.Border = Rectangle.NO_BORDER;
            headerTable.AddCell(cell);

            cell = new PdfPCell(businessName);
            cell.Border = Rectangle.NO_BORDER;
            headerTable.AddCell(cell);

            document.Add(headerTable);

            // Title
            var title = new Paragraph($"Receipt for Order #{receipt.OrderId}", titleFont);
            title.Alignment = Element.ALIGN_CENTER;

            document.Add(title);

            // Order details
            var orderDate = new Paragraph($"Order date: {receipt.OrderDate.ToShortDateString()}", itemFont);
            var buyerName = new Paragraph($"Buyer name: {receipt.BuyerName}", itemFont);
            var buyerEmail = new Paragraph($"Buyer email: {receipt.BuyerEmail}", itemFont);
            var totalAmount = new Paragraph($"Total amount: {receipt.TotalAmount:C}", itemFont);

            document.Add(orderDate);
            document.Add(buyerName);
            document.Add(buyerEmail);
            document.Add(totalAmount);

            // Order items
            var itemList = new List<ListItem>();
            foreach (var item in receipt.Items)
            {
                var itemString = $"{item.ProductName} ({item.Quantity} x {item.Price:C})";
                itemList.Add(new ListItem(itemString));
            }

            var itemSection = new Paragraph("Order items:", itemFont);
            var itemListSection = new List();
            foreach (var item in itemList)
            {
                itemListSection.Add(item);
            }

            document.Add(itemSection);
            document.Add(itemListSection);

            // Footer
            var footer = new Paragraph($"Exclusively from motion stores", itemFont);
            footer.Alignment = Element.ALIGN_CENTER;

            var footerBorder = new Rectangle(0, 0, 0, 1);
            footerBorder.BorderWidth = 1;
            footerBorder.BorderColor = BaseColor.BLACK;

            var footerTable = new PdfPTable(1);
            footerTable.WidthPercentage = 100;
            footerTable.SpacingBefore = 10f;
            footerTable.SpacingAfter = 10f;
            footerTable.HorizontalAlignment = Element.ALIGN_CENTER;

            cell = new PdfPCell(footer);
            cell.Border = Rectangle.NO_BORDER;
            footerTable.AddCell(cell);

            cell = new PdfPCell();
            cell.Border = Rectangle.BOTTOM_BORDER;
            cell.BorderWidth = 1;
            cell.BorderColor = BaseColor.BLACK;
            cell.AddElement(footerTable);

            document.Add(cell);

            // Close the document
            writer.CloseStream = false;
            writer.Close();

            // Reset the stream position
            stream.Position = 0;

            // Return the stream
            return stream;
        }


    }
}
