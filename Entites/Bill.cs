namespace HMS.Entites
{
    public class Bill
    {
        public int Id { get; set; }
        public int PatientId { get; set; }
        public Patient? Patient { get; set; }

        public decimal DoctorFee { get; set; }
        public decimal MedicineCost { get; set; }
        public decimal TestCost { get; set; }
        public decimal TotalAmount { get; set; }

        public DateTime Date { get; set; }
    }

}
