namespace KapitalMedyaBooking.AppService.AppServiceModels.Request
{
    public class BaseSearchRequest
    {
        public int? ID { get; set; }
        public int Page { get; set; } = 1;
        public int PageSize { get; set; } = 10; //default 10
    }
}