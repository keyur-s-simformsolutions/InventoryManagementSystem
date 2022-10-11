using System.ComponentModel.DataAnnotations;

namespace InventoryManagementSystem.Data
{
    public class Item
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        [Required(ErrorMessage = "Please, Enter a Ctegory first")]
        public string  Category { get; set; }
        public string Description { get; set; }
        public Decimal Price { get; set; }
        public int StockItem { get; set; }
    }
}
