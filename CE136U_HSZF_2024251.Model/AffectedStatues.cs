using CE136U_HSZF_2024251.Model;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

public class AffectedStatues
{
    [Key,DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int AffectedStatuesId { get; set; }

    [ForeignKey("TaskId")]
    public int TaskID { get; set; }
    public virtual Tasks? Task { get; set; }

    public int? Health { get; set; }
    public string? Hunger { get; set; }
    public string? Thirst { get; set; }
    public string? Fatigue { get; set; }

    public override string ToString()
    {
        return ($"Id{AffectedStatuesId}, Health: {Health} \n" +
            $"Hunger {Hunger},Thrist{Thirst}\n" +
            $" Fatigue {Fatigue}");
    }
}