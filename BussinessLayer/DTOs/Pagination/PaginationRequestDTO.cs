namespace BussinessLayer.DTOs.Pagination
{
    public class PaginationRequestDTO
    {
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 10;
    }
}