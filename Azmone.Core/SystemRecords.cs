namespace Azmon.Core
{
    public class SystemRecords
    {
        public int Id { get; set; }
        public string UserFullName { get; set; } = string.Empty;
        public string DeviceName { get; set; } = string.Empty;
        public string MachinId { get; set; } = string.Empty;
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public DateTime CreatedDate { get; set; }

    }
}
