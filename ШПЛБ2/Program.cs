using System;
using System.Diagnostics;
using static ШПЛБ2.BasicPurchase;
//Варіант 1. Дано покупка (Назва товару, Категорія, Виробник, Ціна, Опис, Час покупки, Спосіб оплати, Покупець). Розробити систему знижок яка розраховує кінцеву ціну товару для покупця враховуючи такі види знижок:
//b.Знижка на покупку в нічний час X відсотків
//c. Знижка У відсотків на категорію товарів.
//d. Знижка Z відсотків при оплаті карткою.
//a. Персональна знижка покупця N відсотків
//Врахувати всі можливі комбінації знижки.


namespace ШПЛБ2
{
    class Program
    {
        static void Main(string[] args)
        {
            IPurchase purchase = new BasicPurchase(
                "Крем",
                "Косметика",
                "Україна",
                1199,
                "Зволожувачий крем",
                DateTime.Now,
                "Карта",
                "Катерина Шевченко"
                );

            purchase = new NightTimeDiscount(purchase, 5);
            purchase = new CategoryDiscount(purchase, "Косметика", 5);
            purchase = new CardPaymentDiscount(purchase, 3);
            purchase = new PersonalDiscount(purchase, 5);

            Console.WriteLine("Ціна з врахуванням знижок: " + purchase.GetPrice());
            Console.WriteLine("Опис: " + purchase.GetDescription());
        }
    }
}
