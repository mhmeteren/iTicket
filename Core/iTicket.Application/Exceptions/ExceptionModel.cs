using Newtonsoft.Json;
namespace iTicket.Application.Exceptions
{
    public class ExceptionModel : ErrorStatusCode
    {
        public IEnumerable<string>? Errors { get; set; }
        public override string ToString() => JsonConvert.SerializeObject(this);
    }
}
