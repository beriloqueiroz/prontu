using System.ComponentModel.DataAnnotations.Schema;
using domain;
using Microsoft.EntityFrameworkCore;

namespace infrastructure.repository;

[Index(nameof(Email), nameof(Document))]
public class Professional : Model
{
  public Professional()
  {

  }
  public required string ProfessionalDocument { get; set; }
  public required string Name { get; set; }
  public required string Email { get; set; }
  public required string Document { get; set; }
  public required IList<ProfessionalPatient> ProfessionalPatients { get; set; }

  [NotMapped]
  public required IList<Patient> Patients
  {
    set
    {
      Patients = value;
    }
    get
    {
      if (ProfessionalPatients == null) return new List<Patient>();
      return ProfessionalPatients.Select(pp => pp.Patient).ToList();
    }
  }

  public void AddPatient(Patient patient)
  {
    ProfessionalPatients ??= new List<ProfessionalPatient>();
    Patients ??= new List<Patient>();
    Patients.Add(patient);
    ProfessionalPatients.Add(new()
    {
      Id = Guid.NewGuid(),
      PatientId = patient.Id,
      ProfessionalId = Id,
      CreatedAt = DateTime.Now,
      UpdateAt = DateTime.Now,
      Patient = patient,
      Professional = this
    });
  }

  public void FromEntity(domain.Professional entity)
  {
    Id = entity.Id;
    Document = entity.Document.Value;
    Email = entity.Email;
    Name = entity.Name;
    ProfessionalDocument = entity.ProfessionalDocument;
    entity.Patients.ForEach(pe =>
    {
      var patient = Patients.ToList().Find(p => p.Id.Equals(pe.Id));
      if (patient == null)
      {
        AddPatient(new Patient()
        {
          Id = Guid.NewGuid(),
          Document = pe.Document.Value,
          Name = pe.Name,
          Email = pe.Email,
          CreatedAt = DateTime.Now,
          UpdateAt = DateTime.Now,
          Active = pe.IsActive(),
          ProfessionalPatients = new List<ProfessionalPatient>()
        });
      }
      else
      {
        patient.FromEntity(pe);
      }
    });
  }

  public static Professional From(domain.Professional entity)
  {
    var patients = entity.Patients.Select(Patient.From).ToList();
    return new Professional()
    {
      Id = entity.Id,
      Document = entity.Document.Value,
      Email = entity.Email,
      Name = entity.Name,
      ProfessionalDocument = entity.ProfessionalDocument,
      Patients = patients,
      ProfessionalPatients = new List<ProfessionalPatient>()
    };
  }

  public domain.Professional ToEntity()
  {
    return new domain.Professional(
      ProfessionalDocument,
      Name,
      Email,
      new Cpf(Document),
      Patients.Where(p => p != null).Select(p => p.ToEntity()).ToList(),
      Id.ToString());
  }

}