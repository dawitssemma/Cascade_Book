using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Cascade_Book.Models
{
    public class Book
    {
        public string Id { get; set; }
        public string Publisher { get; set; }
        public string Title { get; set; }
        public string AuthorLastName { get; set; }
        public string AuthorFirstName { get; set; }
        public decimal Price { get; set; }
        public int PublicationYear { get; set; }
        public string Edition { get; set; }
        public string CityOfPublication { get; set; }
        public string PublisherName { get; set; }
        public string ISBN { get; set; }

        public string ChicagoCitation
        {
            get
            {
                return $"{AuthorLastName}, {AuthorFirstName}. {Title}. {CityOfPublication}: {PublisherName}, {PublicationYear}.";
            }
        }
        public string MLACitation
        {
            get
            {
                return $"{AuthorLastName}, {AuthorFirstName}. {Title}. {PublisherName}, {PublicationYear}.";
            }
        }
    }
}