namespace SRS.Domain.Entities
{
    public class BiometricUserLog
    {
        public int MachineNumber { get; set; }
        public long IndRegID { get; set; }
        public DateTime DateTimeRecord { get; set; }
        public DateTime DateOnlyRecord { get; set; }
        public DateTime TimeOnlyRecord { get; set; }
        public int dwInOutMode { get; set; }
        public bool bio_hide { get; set; }
    }
}