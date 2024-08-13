namespace SupplierPriceTracker.Models
{
    public class MeasureUnit
    {
        public int Id { get; set; }
        public string Name { get; set; } = String.Empty;
        public string ShortName { get; set; } = String.Empty;
        public bool IsDeleted { get; set; } = false;
    }
}
