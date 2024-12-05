namespace HMS.Classes
{
    public class CartMedicine
    {
        public int ID { get; set; }
        public int PatientId { get; set; }
        public int ExaminationId { get; set; }
        public string Name { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public decimal TotalPrice => Quantity * Price;
    }

}
