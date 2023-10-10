using System.ComponentModel.DataAnnotations;

namespace infrastructure.repository;

public abstract class Model
{

  [Key]
  public required Guid Id { get; set; }

  public DateTime CreatedAt { get; set; }

  public DateTime UpdateAt { get; set; }

}