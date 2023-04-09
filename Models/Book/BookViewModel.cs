using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace AuthenticationSample.Models.Book
{
    public class BookViewModel
    {
        public int Id { get; set; }
        [Display(Name = "Book name")]
        public string BookName { get; set; }
        public double Price { get; set; }
        public string Publisher { get; set; }
        public string Author { get; set; }
    }
}
