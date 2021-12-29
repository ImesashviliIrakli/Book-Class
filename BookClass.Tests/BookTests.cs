using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using NUnit.Framework;

#pragma warning disable SA1600 // ElementsMustBeDocumented

namespace BookClass.Tests
{
    [TestFixture]
    public sealed class BookTests
    {
        [TestCase(5, ExpectedResult = 5)]
        [TestCase(350, ExpectedResult = 350)]
        [TestCase(500, ExpectedResult = 500)]
        [TestCase(2500, ExpectedResult = 2500)]
        [TestCase(3788, ExpectedResult = 3788)]
        public int PropertyPagesTest(int value)
        {
            Book test = new Book()
            {
                Pages = value,
            };

            return test.Pages;
        }

        [TestCase(-2)]
        [TestCase(-8)]
        [TestCase(0)]
        [TestCase(-200)]
        public void PropertyPagesExceptionHandlingTest(int value)
        {
            // throws Exception when pages value is less then or equel to 0
            Assert.Throws<ArgumentException>(() => { new Book { Pages = value }; });
        }

        [TestCase("FRANK HERBERT", "Dune", "Prospero")]
        [TestCase("MARCUS ZUSAK", "THE BOOK THIEF", "InterntaionalBooks")]
        [TestCase("J. K. ROWLING", "HARRY POTTER", "PalitraL")]
        public void FirstConstructorTest(string author, string title, string publisher)
        {
            Book test = new Book(author, title, publisher);
            Assert.AreEqual(author, test.Author);
            Assert.AreEqual(title, test.Title);
            Assert.AreEqual(publisher, test.Publisher);
        }

        [TestCase("FRANK HERBERT", "Dune", "Prospero", "12345678910")]
        [TestCase("MARCUS ZUSAK", "THE BOOK THIEF", "InterntaionalBooks", "12345678910")]
        [TestCase("J. K. ROWLING", "HARRY POTTER", "PalitraL", "12345678910")]
        public void SecondConstructorTest(string author, string title, string publisher, string isbn)
        {
            Book test = new Book(author, title, publisher, isbn);
            Assert.AreEqual(author, test.Author);
            Assert.AreEqual(title, test.Title);
            Assert.AreEqual(publisher, test.Publisher);
            Assert.AreEqual(isbn, test.ISBN);
        }

        [TestCase(2009, 10, 11)]
        [TestCase(2001, 5, 9)]
        [TestCase(1998, 12, 1)]
        [TestCase(1990, 9, 5)]
        public void PublishMethodTest(int year, int month, int day)
        {
            DateTime date = new DateTime(year, month, day);
            Book test = new Book();
            test.Publish(date);
            Assert.AreEqual(date.ToString(CultureInfo.InvariantCulture), test.GetPublicationDate());
        }

        [TestCase(false, ExpectedResult = "NYP")]
        [TestCase(true, ExpectedResult = "works correctly")]
        public string GetPublicationDataMethodTest(bool published)
        {
            Book test = new Book();

            if (published)
            {
                DateTime date = DateTime.Now;
                test.Publish(date);
                Assert.AreEqual(date.ToString(CultureInfo.InvariantCulture), test.GetPublicationDate());
                return "works correctly";
            }
            else
            {
                Assert.AreEqual("NYP", test.GetPublicationDate());
                return test.GetPublicationDate();
            }
        }

        [TestCase(0.5, "USD")]
        [TestCase(1, "GEL")]
        [TestCase(523.5, "EUR")]
        [TestCase(523.5, "EUR")]
        [TestCase(32.5, "AED")]
        [TestCase(10000, "AMD")]
        public void SetPriceMethodTest(double price, string currency)
        {
            Book test = new Book();
            test.SetPrice(price, currency);
            Assert.AreEqual(price, test.Price);
            Assert.AreEqual(currency, test.Currency);
        }

        [TestCase(-1, "USD")] // Price is invalid
        [TestCase(0, "GEL")] // Price is invalid
        [TestCase(3.5, "USA")] // currency doesn't exist
        [TestCase(315.20, "GALL")] // currency doesn't exist
        public void SetPriceMethodExceptionHandling(double price, string currency) // throws if price is negative, Currency is lower case  or contains more that 3 character or non letter characters  
        {
            Book test = new Book();
            Assert.Throws<ArgumentException>(() => { test.SetPrice(price, currency); });
        }

        [TestCase("Dune", "FRANK HERBERT", ExpectedResult = "Dune by FRANK HERBERT")]
        [TestCase("THE BOOK THIEF", "MARCUS ZUSAK", ExpectedResult = "THE BOOK THIEF by MARCUS ZUSAK")]
        [TestCase("HARRY POTTER", "J. K. ROWLING", ExpectedResult = "HARRY POTTER by J. K. ROWLING")]
        public string ToStringMethodTest(string title, string author)
        {
            Book test = new Book(author, title, " ");
            return test.ToString();
        }
    }
}
