using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HealthDeclarationAPI.Entities {
    [Table("HealthDeclaration")]
    public class HealthDeclarationEntity {
        [Key]
        public string Id { get; set; }
        public string? DeviceId { get; set; }
        public string? DeviceType { get; set; }
        public string? DeviceName { get; set; }
        public string? GroupId { get; set; }
        public string? DataType { get; set; }
        public long Timestamp { get; set; }
    }
}
