using System;
using System.Diagnostics;

namespace ШПЛБ2
{
	public interface IPurchase
	{
		double GetPrice();
		string GetDescription();
	}

	public class BasicPurchase : IPurchase
	{
		public string ProductName { get; set; }
		public string Category { get; set; }
		public string Producer { get; set; }
		public double Price { get; set; }
		public string Description { get; set; }
		public DateTime PurchaseTime { get; set; }
		public string PaymentMethod { get; set; }
		public string Buyer { get; set; }

		public BasicPurchase(string productName, string category, string producer, double price, string description, DateTime purchaseTime, string paymentMethod, string buyer)
		{
			ProductName = productName;
			Category = category;
            Producer = producer;
            Price = price;
            Description = description;
            PurchaseTime = purchaseTime;
            PaymentMethod = paymentMethod;
            Buyer = buyer;
        }

		public double GetPrice()
		{
			return Price;
		}
		public string GetDescription()
		{
			return ($"Назва товару: {ProductName}, категорія: {Category}, виробник: {Producer}, ціна: {Price}, опис: {Description}, час покупки: {PurchaseTime}, спосіб оплати: {PaymentMethod}, покупець: {Buyer}.");
		}


		public abstract class DiscountDecorator : IPurchase
        {
            protected IPurchase _purchase;

            public DiscountDecorator(IPurchase purchase)
            {
                _purchase = purchase;
            }

            public virtual double GetPrice()
            {
                return _purchase.GetPrice();
            }

            public virtual string GetDescription()
            {
                return _purchase.GetDescription();
            }

            public IPurchase GetBasePurchase()
            {
                if (_purchase is DiscountDecorator)
                {
                    return ((DiscountDecorator)_purchase).GetBasePurchase();
                }
                return _purchase;
            }
        }

        public class NightTimeDiscount : DiscountDecorator
        {
            private double _discountPercentage;

            public NightTimeDiscount(IPurchase purchase, double discountPercentage) : base(purchase)
            {
                _discountPercentage = discountPercentage;
            }

            public override double GetPrice()
            {
                double price = base.GetPrice();
                DateTime purchaseTime = ((BasicPurchase)_purchase).PurchaseTime;
                if (purchaseTime.Hour >= 0 && purchaseTime.Hour < 6)
                {
                    price -= price * (_discountPercentage / 100);
                }
                return price;
            }

            public override string GetDescription()
            {
                return base.GetDescription() + $" + Нічна знижка {_discountPercentage}%";
            }
        }

        public class CategoryDiscount : DiscountDecorator
        {
            private double _discountPercentage;
            private string _category;

            public CategoryDiscount(IPurchase purchase, string category, double discountPercentage) : base(purchase)
            {
                _discountPercentage = discountPercentage;
                _category = category;
            }

            public override double GetPrice()
            {
                double price = base.GetPrice();

                BasicPurchase basePurchase = (BasicPurchase)((DiscountDecorator)_purchase).GetBasePurchase();
                string purchaseCategory = basePurchase.Category;
                if (_category == purchaseCategory)
                {
                    price -= price * (_discountPercentage / 100);
                }
                return price;
            }

            public override string GetDescription()
            {
                return base.GetDescription() + $" + Знижка на катенорію товарів {_discountPercentage}%";
            }
        }

        public class CardPaymentDiscount : DiscountDecorator
        {
            private double _discountPercentage;

            public CardPaymentDiscount(IPurchase purchase, double discountPercentage) : base(purchase)
            {
                _discountPercentage = discountPercentage;
            }

            public override double GetPrice()
            {
                double price = base.GetPrice();

                BasicPurchase basePurchase = (BasicPurchase)((DiscountDecorator)_purchase).GetBasePurchase();
                string paymentMethod = basePurchase.PaymentMethod;
                if (paymentMethod == "Card")
                {
                    price -= price * (_discountPercentage / 100);
                }
                return price;
            }

            public override string GetDescription()
            {
                return base.GetDescription() + $" + Знижка при оплаті картою {_discountPercentage}%";
            }
        }

        public class PersonalDiscount : DiscountDecorator
        {
            private double _discountPercentage;

            public PersonalDiscount(IPurchase purchase, double discountPercentage) : base(purchase)
            {
                _discountPercentage = discountPercentage;
            }

            public override double GetPrice()
            {
                double price = base.GetPrice();
                price -= price * (_discountPercentage / 100);
                return price;
            }

            public override string GetDescription()
            {
                return base.GetDescription() + $" + Персональна знижка {_discountPercentage}%";
            }
        }
    }
}

