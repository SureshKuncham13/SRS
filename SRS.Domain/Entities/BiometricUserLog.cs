namespace SRS.Domain.Entities
{
    public class BiometricUserLog
    {
        public string IndRegID { get; set; } = string.Empty;
        public DateTime DateTimeRecord { get; set; }
        public string SerialNumber { get; set; } = string.Empty;
        public string dwInOutMode { get; set; } = string.Empty;
        public bool bio_hide { get; set; }
    }
}