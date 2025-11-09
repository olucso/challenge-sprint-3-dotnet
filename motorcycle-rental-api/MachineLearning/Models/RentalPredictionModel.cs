namespace motorcycle_rental_api.MachineLearning.Models
{
    public class RentalInputModel
    {
        public float Days { get; set; }
        public float DailyValue { get; set; }
        public float ClientFidelity { get; set; }

        public float TotalValue { get; set; }
    }

    public class RentalPrediction
    {
        public float Score { get; set; }
    }
}
