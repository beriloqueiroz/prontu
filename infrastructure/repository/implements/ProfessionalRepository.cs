using application;
using application.professional;
using infrastructure.repository;
using Microsoft.EntityFrameworkCore;

namespace repository;

public class ProfessionalRepository : IProfessionalGateway
{
  private readonly ProntuDbContext Context;

  public ProfessionalRepository(ProntuDbContext context)
  {
    Context = context;
  }

  public void AddPatient(domain.Patient entity, string professionalId)
  {
    var transaction = Context.Database.BeginTransaction();
    var patient = Patient.From(entity);
    Context.Add(patient);
    patient.ProfessionalPatients?.ToList().ForEach(pp =>
    {
      pp.ProfessionalId = new Guid(professionalId);
      pp.UpdateAt = DateTime.Now;
      Context.Add(pp);
    });
    Context.SaveChanges();
    transaction.Commit();
  }

  public void Create(domain.Professional entity)
  {
    var professional = Professional.From(entity);
    professional.CreatedAt = DateTime.Now;
    professional.UpdateAt = DateTime.Now;
    Context.Professionals?.Add(professional);
    Context.SaveChanges();
  }

  public domain.Professional? Find(string id)
  {
    return Context.Professionals?.Include("ProfessionalPatients.Patient").First(p => p.Id.Equals(new Guid(id)))?.ToEntity();
  }

  public domain.Patient? FindPatient(string id, string professionalId)
  {
    return Context.Patients?
    .Include("ProfessionalPatients.Professionals")
    .First(p => p.Id.Equals(new Guid(id)) && p.Professionals.Any(pp => pp.Id.Equals(new Guid(professionalId))))?
    .ToEntity();
  }

  public bool IsExists(string document, string email)
  {
    var result = Context.Professionals?.Any(p => p.Document.Equals(document) || p.Email.Equals(email));
    return result != null && (bool)result;
  }

  public bool IsExists(string id)
  {
    var result = Context.Professionals?.Any(p => p.Id.ToString().Equals(id));
    return result != null && (bool)result;
  }

  public PaginatedList<domain.Professional> List(PageAble pageAble)
  {
    if (Context.Professionals == null)
    {
      return PaginatedList<domain.Professional>.Empty();
    }
    var list = Context.Professionals
      .OrderBy(p => p.CreatedAt)
      .Skip(pageAble.PageIndex - 1)
      .Take(pageAble.PageSize)
      .Include("ProfessionalPatients.Patient")
      .ToList();
    return new(list.Select(p => p.ToEntity()), pageAble);
  }

  public void Update(domain.Professional entity)
  {
    var professional = (Context.Professionals?.Find(entity.Id)) ?? throw new Exception("ProfessionalRepository: Profissional não encontrado");
    professional.FromEntity(entity);
    professional.UpdateAt = DateTime.Now;
    Context.SaveChanges();
  }

  public void UpdatePatient(domain.Patient entity, string professionalId)
  {
    var patient = (Context.Patients?.Find(entity.Id)) ?? throw new Exception("ProfessionalRepository: Patient não encontrado");
    patient.FromEntity(entity);
    patient.UpdateAt = DateTime.Now;
    Context.SaveChanges();
  }
}