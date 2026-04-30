namespace ZentekLabs.Models.Dtos.Response
{
    public class ResponseData<T>
    {
        public bool Sucesss { get; set; }
        public string? Message { get; set; }
        public T? Data { get; set; }
    }
}
