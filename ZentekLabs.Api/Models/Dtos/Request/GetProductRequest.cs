namespace ZentekLabs.Models.Dtos.Request
{
    public class GetProductRequest
    {
        public string? Color { get; set; }

        public string? Search { get; set; }

        public bool? IsActive { get; set; }


        public int? Page { get; set; } = 1;
        public int? PageSize { get; set; } = 10;

        public  DateTime? FromDate { get; set; }

        public DateTime? ToDate { get; set; }





    }
}
