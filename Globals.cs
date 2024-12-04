using HMS.Classes;

namespace HMS
{
    public static class Globals
    {
        public static int CountUsers;
        public static int CountPatients;

        public static decimal TotalPrice;
        public static int CurrentExaminationId { get; set; }
        public static List<CartMedicine> CartMedicinesList = new List<CartMedicine>();

    }
}
