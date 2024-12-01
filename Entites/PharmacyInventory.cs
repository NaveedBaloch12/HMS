namespace HMS.Entites
{
    public class PharmacyInventory
    {
        public int Id { get; set; }
        public string? MedicineName { get; set; }
        public int StockQuantity { get; set; }
        public decimal PricePerUnit { get; set; }
        public DateTime ExpiryDate { get; set; }


        public ICollection<DispensedMedicine> DispensedMedicines { get; set; }
    }

}
