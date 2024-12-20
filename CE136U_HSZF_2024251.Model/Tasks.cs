using CE136U_HSZF_2024251.Model;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

public class Tasks
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int TaskId { get; set; }


    public virtual Hero? Hero { get; set; }

    [Required]
    public string? Name { get; set; }

    public int Duration { get; set; }

    [Required]
    public virtual Resource? Required_resources { get; set; }

    public virtual Resource? Reward { get; set; }

    public virtual AffectedStatues? Affected_status { get; set; }
}