using KapitalMedyaBooking.AppService.AppServiceModels.Enum;

namespace KapitalMedyaBooking.AppService.AppServiceModels.Response
{
    public class ServiceResponse<T>
    {
        public string Message { get; set; }
        public MessageTypeEnum MessageType { get; set; }
        public T ReturnObject { get; set; }
    }
}
