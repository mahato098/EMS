namespace EMS.DTO.DepartmentDto
{
    public class UpdateDepartmentDto
    {
        public int DepartmentId { get; set; }
        public string ShortName { get; set; } = string.Empty;
        public string LongName { get; set; } = string.Empty;
    }
}
