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

  public void AddPatient(domain.Patient patient, string professionalId)
  {
    var transaction = Context.Database.BeginTransaction();
    var patientModel = Patient.From(patient);
    var professional = Context.Professionals?.Find(new Guid(professionalId));
    if (professional == null) throw new ArgumentException("Profissional inválido");
    patientModel.Professionals.Add(professional);
    patientModel.CreatedAt = DateTime.Now;
    patientModel.UpdateAt = DateTime.Now;
    Context.Add(patientModel);
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
    return Context.Professionals?.Include(p => p.Patients).First(p => p.Id.Equals(new Guid(id)))?.ToEntity();
  }

  public domain.Patient? FindPatient(string id, string professionalId)
  {
    var patient = Context.Patients?
    .Include(p => p.PersonalForm)
    .Include(p => p.ProfessionalPatients)
    .First(p => p.Id.Equals(new Guid(id)));

    var patientToReturn = patient?.ToEntity();

    var financialInfos = patient?.ProfessionalPatients?.First(pp => pp.PatientId.ToString().Equals(id) && pp.ProfessionalId.ToString().Equals(professionalId));

    if (patientToReturn != null && financialInfos != null && financialInfos.IsFinancialInfoComplete())
    {
      patientToReturn.ChangeFinancialInfo(new()
      {
        DefaultSessionPrice = financialInfos.DefaultSessionPrice ?? 1000M,
        EstimatedSessionsByWeek = financialInfos.EstimatedSessionsByWeek ?? 0,
        EstimatedTimeSessionInMinutes = financialInfos.EstimatedTimeSessionInMinutes ?? 0,
        SessionType = financialInfos.SessionType ?? domain.SessionType.OFFLINE,
        PaymentType = financialInfos.PaymentType ?? domain.PaymentType.PER_SESSION,
        PaymentPeriodInDays = financialInfos.PaymentPeriodInDays,
        SessionQuantityPerPayment = financialInfos.SessionQuantityPerPayment
      });
    }
    return patientToReturn;
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
      .Include(p => p.Patients)
      .ToList();
    return new(list.Select(p => p.ToEntity()), pageAble);
  }

  public void Update(domain.Professional entity)
  {
    var professional = (Context.Professionals?.Find(entity.Id)) ?? throw new ArgumentException("ProfessionalRepository: Profissional não encontrado");
    professional.FromEntity(entity);
    professional.UpdateAt = DateTime.Now;
    Context.SaveChanges();
  }

  public void UpdatePatient(domain.Patient patient, string professionalId)
  {
    var transaction = Context.Database.BeginTransaction();
    var patientModel = (Context.Patients?.Include(p => p.ProfessionalPatients).First(p => p.Id.Equals(patient.Id)))
      ?? throw new ArgumentException("ProfessionalRepository: Patient não encontrado");
    patientModel.FromEntity(patient);
    patientModel.UpdateAt = DateTime.Now;
    if (patient.FinancialInfo != null)
    {
      var professionalPatients = patientModel.ProfessionalPatients.Find(pp => pp.ProfessionalId.ToString().Equals(professionalId));
      if (professionalPatients == null)
      {
        professionalPatients = new()
        {
          Id = Guid.NewGuid(),
          ProfessionalId = new Guid(professionalId),
          PatientId = patientModel.Id,
          CreatedAt = DateTime.Now,
          UpdateAt = DateTime.Now,
          DefaultSessionPrice = patient.FinancialInfo.DefaultSessionPrice,
          EstimatedSessionsByWeek = patient.FinancialInfo.EstimatedSessionsByWeek,
          EstimatedTimeSessionInMinutes = patient.FinancialInfo.EstimatedTimeSessionInMinutes,
          SessionType = patient.FinancialInfo.SessionType
        };
        Context.Add(professionalPatients);
      }
      else
      {
        professionalPatients.DefaultSessionPrice = patient.FinancialInfo.DefaultSessionPrice;
        professionalPatients.EstimatedSessionsByWeek = patient.FinancialInfo.EstimatedSessionsByWeek;
        professionalPatients.EstimatedTimeSessionInMinutes = patient.FinancialInfo.EstimatedTimeSessionInMinutes;
        professionalPatients.SessionType = patient.FinancialInfo.SessionType;
        professionalPatients.UpdateAt = DateTime.Now;
      }
    }
    Context.SaveChanges();
    transaction.Commit();
  }
}