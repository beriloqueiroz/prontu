using domain;

namespace application.professional;

public class ListProfessionalUseCase : IListProfessionalUseCase
{
  private readonly IProfessionalGateway ProfessionalGateway;
  public ListProfessionalUseCase(IProfessionalGateway professionalGateway)
  {
    ProfessionalGateway = professionalGateway;
  }

  public PaginatedList<ProfessionalDefaultDto> Execute(ListProfessionalInputDto input)
  {
    PageAble pageAble = new(input.PageSize, input.PageIndex);
    PaginatedList<Professional>? professionals;

    try
    {
      professionals = ProfessionalGateway.List(pageAble);
    }
    catch (Exception e)
    {
      throw new ApplicationException("ListProfessionalUseCase: Erro ao listar profissionais", e);
    }

    var professionalList = professionals.Select(professional =>
        new ProfessionalDefaultDto(
          professional.Id.ToString(),
          professional.Name,
          professional.Email,
          professional.Document.Value,
          professional.ProfessionalDocument,
          professional.Patients?.Select(pat =>
            new PatientDefaultDto(pat.Id.ToString(), pat.Name, pat.Email, pat.Document.Value, pat.Active, null, null)).ToArray() ?? Array.Empty<PatientDefaultDto>()));

    return new PaginatedList<ProfessionalDefaultDto>(professionalList, pageAble);
  }
}

public record ListProfessionalInputDto(
  int PageSize,
  int PageIndex
);