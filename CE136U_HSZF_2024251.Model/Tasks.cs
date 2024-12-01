using CE136U_HSZF_2024251.Model;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

public class Tasks
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    public virtual Hero? Hero { get; set; }

    public string Name { get; set; }

    public int Duration { get; set; }

    public int RequiredResourcesId { get; set; }
    [ForeignKey(nameof(RequiredResourcesId))]
    public virtual Resource RequiredResources { get; set; }

    public int? RewardId { get; set; }
    [ForeignKey(nameof(RewardId))]
    public virtual Resource? Reward { get; set; }

    public int AffectedStatusId { get; set; }
    [ForeignKey(nameof(AffectedStatusId))]
    public virtual AffectedStatues AffectedStatus { get; set; }
}