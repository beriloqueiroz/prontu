using System.Text.Json;
using domain;
using Microsoft.EntityFrameworkCore;

namespace infrastructure.repository;

[Index(nameof(Email), nameof(Document))]
public class Patient : Model
{
  public Patient()
  {
  }
  public required string Name { get; set; }
  public required string Email { get; set; }
  public required string Document { get; set; }
  public string? Avatar { get; set; }
  public string? Phones { get; set; }
  public bool Active { get; set; }

  public List<Phone> GetPhones()
  {
    if (Phones == null) return new List<Phone>();
    var options = new JsonSerializerOptions
    {
      PropertyNameCaseInsensitive = true,
    };
    return JsonSerializer.Deserialize<List<Phone>>(Phones, options) ?? new List<Phone>();
  }

  public List<ProfessionalPatient> ProfessionalPatients { get; } = new();

  public PersonalForm? PersonalForm { get; set; }

  public IList<Professional> Professionals
  {
    get;
    set;
  } = new List<Professional>();

  public void FromEntity(domain.Patient entity)
  {
    Id = entity.Id;
    Document = entity.Document.Value;
    Email = entity.Email;
    Name = entity.Name;
    Avatar = entity.Avatar?.Value;
    if (entity.Phones != null)
      Phones = JsonSerializer.Serialize(entity.Phones);
    if (entity.PersonalForm != null)
    {
      PersonalForm ??= new()
      {
        Id = Guid.NewGuid()
      };
      PersonalForm.Street = entity.PersonalForm.Street;
      PersonalForm.Neighborhood = entity.PersonalForm.Neighborhood;
      PersonalForm.City = entity.PersonalForm.City;
      PersonalForm.Number = entity.PersonalForm.Number;
      PersonalForm.Country = entity.PersonalForm.Country;
      PersonalForm.ZipCode = entity.PersonalForm.ZipCode;
      PersonalForm.Region = entity.PersonalForm.Region;
      PersonalForm.Contact = entity.PersonalForm.Contact;
      PersonalForm.Phones = entity.PersonalForm.Phones;
      PersonalForm.OthersInfos = entity.PersonalForm.OthersInfos;
      PersonalForm.Observations = entity.PersonalForm.Observations;
    }
  }

  public static Patient From(domain.Patient entity)
  {
    var patient = new Patient()
    {
      Id = entity.Id,
      Document = entity.Document.Value,
      Email = entity.Email,
      Name = entity.Name,
      Avatar = entity.Avatar?.Value,
      Phones = JsonSerializer.Serialize(entity.Phones)
    };
    if (entity.PersonalForm != null)
    {
      patient.PersonalForm ??= new()
      {
        Id = Guid.NewGuid()
      };
      patient.PersonalForm.Street = entity.PersonalForm.Street;
      patient.PersonalForm.Neighborhood = entity.PersonalForm.Neighborhood;
      patient.PersonalForm.City = entity.PersonalForm.City;
      patient.PersonalForm.Number = entity.PersonalForm.Number;
      patient.PersonalForm.Country = entity.PersonalForm.Country;
      patient.PersonalForm.ZipCode = entity.PersonalForm.ZipCode;
      patient.PersonalForm.Region = entity.PersonalForm.Region;
      patient.PersonalForm.Contact = entity.PersonalForm.Contact;
      patient.PersonalForm.Phones = entity.PersonalForm.Phones;
      patient.PersonalForm.OthersInfos = entity.PersonalForm.OthersInfos;
      patient.PersonalForm.Observations = entity.PersonalForm.Observations;
    }
    return patient;
  }

  public domain.Patient ToEntity()
  {
    var patient = new domain.Patient(Name, Email, new Cpf(Document), Id.ToString());
    if (PersonalForm != null)
      patient.ChangePersonalForm(new()
      {
        Street = PersonalForm.Street,
        Neighborhood = PersonalForm.Neighborhood,
        City = PersonalForm.City,
        Number = PersonalForm.Number,
        Country = PersonalForm.Country,
        ZipCode = PersonalForm.ZipCode,
        Region = PersonalForm.Region,
        Contact = PersonalForm.Contact,
        Phones = PersonalForm.Phones,
        OthersInfos = PersonalForm.OthersInfos,
        Observations = PersonalForm.Observations
      });

    if (Avatar != null)
      patient.ChangeAvatar(new(Avatar));
    patient.ChangePhones(GetPhones());
    return patient;
  }

}