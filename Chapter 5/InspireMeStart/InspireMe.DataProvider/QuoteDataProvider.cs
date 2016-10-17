using InspireMe.DataProvider.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

namespace InspireMe.DataProvider
{
    public class QuoteDataProvider
    {
        private const string quoteXML = "Quotes.xml";

        public QuoteDataProvider()
        {
            LoadQuoteList();
        }

        /// <summary>
        /// Gets a random quote from the list of available quotes.
        /// </summary>
        public string GetRandomQuote()
        {
            Random random = new Random();
            int minId = this.QuoteList.Min(q => q.QuoteId);
            int maxId = this.QuoteList.Max(q => q.QuoteId);

            //get a random quote Id between the range of available quote Ids
            int nextQuoteId = random.Next(minId, maxId);

            //retrieve the first quote from the collection that
            //is equivalent to the random quote Id
            QuoteItem quoteItem = this.QuoteList.Where(q => q.QuoteId == nextQuoteId).FirstOrDefault();

            return quoteItem.Quote;
        }

        /// <summary>
        /// Loads the list of quotes from the Quotes.xml file.
        /// </summary>
        private void LoadQuoteList()
        {
            this.QuoteXmlDoc = XDocument.Load(quoteXML);

            if (this.QuoteXmlDoc != null)
            {
                this.QuoteList = new List<QuoteItem>(
                                from quote in this.QuoteXmlDoc.Descendants("Quote")
                                select new QuoteItem
                                {
                                    QuoteId = int.Parse(quote.Attribute("Id").Value),
                                    Quote = quote.Value.Replace(System.Environment.NewLine, "").Trim(),
                                    Author = quote.Attribute("Author").Value
                                });
            }
        }

        public XDocument QuoteXmlDoc
        {
            get;
            private set;
        }

        public List<QuoteItem> QuoteList
        {
            get;
            private set;
        }
    }
}
