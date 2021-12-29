using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace BookClass
{
    public sealed class Book
    {
        private bool published;
        private int totalPages;
        private DateTime datePublished;


        public string Title { get; }

        public int Pages
        {
            get { return totalPages; }
            set { totalPages = value > 0 ? value : throw new ArgumentException("Number of pages was invalid"); }
        }

        public string Publisher { get; }
        public string ISBN { get; }
        public string Author { get; }
        public double Price { get; private set; }
        public string Currency { get; private set; }

        public Book()
        { }
        public Book(string author, string title, string publisher)
        {
            this.Author = author ?? throw new ArgumentNullException(nameof(author));
            this.Title = title ?? throw new ArgumentNullException(nameof(title));
            this.Publisher = publisher ?? throw new ArgumentNullException(nameof(publisher));
        }
        public Book(string author, string title, string publisher, string isbn)
            : this(author, title, publisher)
        {
            if (string.IsNullOrEmpty(isbn))
            {
                throw new ArgumentException($"{isbn} => ISBN must have a value");
            }

            if (isbn.Length < 10 || isbn.Length > 13)
            {
                throw new ArgumentException("ISBN should contain 10 to 13 digits");
            }

            foreach (char c in isbn)
            {
                if (!char.IsDigit(c))
                {
                    throw new ArgumentException($"{isbn} => ISBN number should only contain numbers");
                }
            }

            this.ISBN = isbn;
        }
        public void Publish(DateTime date)
        {
            this.datePublished = date;
            this.published = true;
        }

        public string GetPublicationDate()
        {
            if (published)
            {
                return datePublished.ToString(CultureInfo.InvariantCulture);
            }
            else
            {
                return "NYP";
            }
        }

        public new string ToString()
        {
            return $"{Title} by {Author}";
        }

        public void SetPrice(double price, string currency)
        {
            CultureInfo[] cultures = CultureInfo.GetCultures(CultureTypes.AllCultures & ~CultureTypes.NeutralCultures);

            List<string> currencies = new List<string>();

            foreach (CultureInfo culture in cultures)
            {
                if (culture.LCID == 127 || culture.IsNeutralCulture) continue;
                RegionInfo region = new RegionInfo(culture.LCID);
                currencies.Add(region.ISOCurrencySymbol);
            }

            if (currencies.Contains(currency))
            {
                this.Currency = currency;
            }
            else
            {
                throw new ArgumentException($"{currency} => this currency does not exist");
            }

            if (price > 0)
            {
                this.Price = price;
            }
            else
            {
                throw new ArgumentException($"{price} => This price is invalid");
            }
        }
    }
}
