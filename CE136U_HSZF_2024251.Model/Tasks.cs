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

    public override string ToString()
    {
        return $"{Name ?? "No Name"} \n" +
               $" Duration : {Duration} Hours \n " +
               $"REQUIRED_RESOURCES: {(Required_resources != null ? Required_resources.ToString() : "None")} \n" +
               $"REWARD: {(Reward != null ? Reward.ToString() : "None")} \n";
    }


}