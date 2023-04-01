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
            var titleFont = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 18);
            var itemFont = FontFactory.GetFont(FontFactory.HELVETICA, 12);

            var title = new Paragraph($"Receipt for order #{receipt.OrderId}", titleFont);
            title.Alignment = Element.ALIGN_CENTER;

            var orderDate = new Paragraph($"Order date: {receipt.OrderDate.ToShortDateString()}", itemFont);
            var buyerName = new Paragraph($"Buyer name: {receipt.BuyerName}", itemFont);
            var buyerEmail = new Paragraph($"Buyer email: {receipt.BuyerEmail}", itemFont);
            var totalAmount = new Paragraph($"Total amount: {receipt.TotalAmount:C}", itemFont);

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

            document.Add(title);
            document.Add(orderDate);
            document.Add(buyerName);
            document.Add(buyerEmail);
            document.Add(totalAmount);
            document.Add(itemSection);
            document.Add(itemListSection);

            writer.CloseStream = false;
            // Close the document
            document.Close();

            // Reset the stream position
            stream.Position = 0;

            // Return the stream
            return stream;
        }


    }
}
