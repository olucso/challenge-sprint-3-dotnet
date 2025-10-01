namespace motorcycle_rental_api.Models
{
    public class PageResultModel<T>
    {
        public T Data { get; set; }

        public int Displacement { get; set; }
        public int ReturnedRecords { get; set; }
        public int TotalRecords { get; set; }
    }
}
