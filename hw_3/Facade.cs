using System;

class Stock
{
	public string CheckStock(string orderId)
    {
		return (orderId != "") ? "Available" : "Not Available";
    }
}

class Card
{
	public string AddOrder(string orderId)
    {
		return "Order Added Successfully";
    }

	public string RemoveOrder(string orderId)
    {
		return "Order Removed Successfully";
    }

	public string CheckOutOrder(string orderId)
    {
		return "Order Checkout Successfully";
    }
}

class Payment
{
	public string CheckPayment(string paymentDetails)
    {
		return (paymentDetails != "") ? "Valid Payment" : "Not Valid Payment";
    }

	public string DeductPayment(string orderId)
    {
		return (orderId != "") ? "Payment Deducted Successfully" : "Payment Not Being. Check From Your Order";
    }
}

class OrderFacade
{
	private Stock stock;
	private Card card;
	private Payment payment;

	public OrderFacade()
	{
		stock = new Stock();
		card = new Card();
		payment = new Payment();
	}

	public string CheckStockAvailability(string orderId)
	{
		string checkStockAvailability = stock.CheckStock(orderId);

		return checkStockAvailability;
	}

	public string AddToCard(string orderId) => card.AddOrder(orderId);

	public string RemoveFromCard(string orderId) => card.RemoveOrder(orderId);

	public string CheckoutOrderFromCard(string orderId) => card.CheckOutOrder(orderId);
	
	public void PlaceOrder(string orderId)
	{
		string paymentValidationStatus = payment.CheckPayment(orderId);
		Console.WriteLine(string.Format("\nPayment Validation Status: {0}", paymentValidationStatus));
		string deductPaymentStatus = payment.DeductPayment(orderId);
		Console.WriteLine(string.Format("\nPayment Deduct Status: {0}", deductPaymentStatus));
	}
}

class Program
{
	static void Main()
	{
		OrderFacade order = new OrderFacade();
		string orderId = "order 1";
		string orderStatus = order.CheckStockAvailability(orderId);

		if (orderStatus == "Available")
		{
			Console.WriteLine(string.Format("\nStock Status: {0}", orderStatus));
			Console.WriteLine(string.Format("\nAdd Card Status: {0}", order.AddToCard(orderId)));
			Console.WriteLine(string.Format("\nCheckout Order From Card Status: {0}", order.CheckoutOrderFromCard(orderId)));
			order.PlaceOrder(orderId);
		}
		else
        {
			Console.WriteLine(string.Format("\nStock Status: {0}", orderStatus));
        }
	}
}