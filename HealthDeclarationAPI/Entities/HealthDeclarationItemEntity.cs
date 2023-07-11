using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace HealthDeclarationAPI.Entities {
    [Table("HealthDeclarationItem")]
    public class HealthDeclarationItemEntity {
        [Key]
        public string Id { get; set; }
        public bool FullPowerMode { get; set; }
        public bool ActivePowerControl { get; set; }
        public int FirmwareVersion { get; set; }
        public int Temperature { get; set; }
        public int Humidity { get; set; }
        public int Version { get; set; }
        public string? MessageType { get; set; }
        public bool Occupancy { get; set; }
        public int StateChanged { get; set; }
        public string HeaderId { get; set; }
    }
}
