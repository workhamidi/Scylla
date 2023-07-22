namespace Scylla.BLL.Dtos.Models
{
    public class DimEmployeeDto
    {
        
        public int EmployeeKey { get; set; }

        public string FirstName { get; set; } = null!;
        
        public string? LastName { get; set; } = null!;

        public DateTime? BirthDate { get; set; }

        public string? EmailAddress { get; set; }
        
        public string? Phone { get; set; }

        // navigation
        public ICollection<FactSalesQuotumDto>? FactSalesQuotum { get; set; }

        }
}
