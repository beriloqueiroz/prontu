using System.ComponentModel.DataAnnotations;

namespace infrastructure.repository;

public abstract class Model
{

  [Key]
  public Guid Id { get; set; }

  public DateTime CreatedAt { get; set; }

  [Timestamp]
  public DateTime UpdateAt { get; set; }

}