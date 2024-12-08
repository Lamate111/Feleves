using CE136U_HSZF_2024251.Model;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

public class AffectedStatues
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int AffectedStatuesId { get; set; }

    [Required]
    public int TaskID_affct { get; set; }
    public virtual Tasks Task { get; set; }

    public int? Health { get; set; }
    public int? Hunger { get; set; }
    public int? Thirst { get; set; }
    public int? Fatigue { get; set; }
}