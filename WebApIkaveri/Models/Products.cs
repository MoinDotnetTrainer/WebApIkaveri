using System.ComponentModel.DataAnnotations;

namespace WebApIkaveri.Models
{
    public class Products
    {
        [Key]
        public int ProductID { get; set; }
        public string Pname { get; set; }
        public int Qty { get; set; }
        public int Price { get; set; }
    }
}
