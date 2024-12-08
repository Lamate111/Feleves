using CE136U_HSZF_2024251.Model;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

public class Tasks
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int TaskId { get; set; }


    public virtual Hero? Hero { get; set; }

    public string Name { get; set; }

    public int Duration { get; set; }

    public virtual Resource? RequiredResources { get; set; }

    public virtual Resource? Reward { get; set; }

    public virtual AffectedStatues? AffectedStatus { get; set; }
}