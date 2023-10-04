using domain;

namespace application.professional;

public class ListProfessionalUseCase : IUsecase<ListProfessionalInputDto, PaginatedList<ListProfessionalOutputDto>>
{
  private readonly IProfessionalGateway ProfessionalGateway;
  public ListProfessionalUseCase(IProfessionalGateway professionalGateway)
  {
    ProfessionalGateway = professionalGateway;
  }

  public PaginatedList<ListProfessionalOutputDto> Execute(ListProfessionalInputDto input)
  {
    PageAble pageAble = new(input.PageIndex, input.PageSize);
    PaginatedList<Professional>? professionals;
    try
    {
      professionals = ProfessionalGateway.List(pageAble);
    }
    catch (Exception e)
    {
      throw new ApplicationException("ListProfessionalUseCase: Erro ao listar profissionais", e);
    }
    return
      new PaginatedList<ListProfessionalOutputDto>(
      professionals.Select(professional =>
        new ListProfessionalOutputDto(
          professional.Id.ToString(),
          professional.Name,
          professional.Email,
          professional.Document.Value,
          professional.ProfessionalDocument,
          professional.Patients.Select(pat =>
            new ListProfessionalPatientOutputDto(pat.Id.ToString(), pat.Name, pat.Email, pat.Document.Value, pat.Active)).ToArray())).ToList(), professionals.Count, pageAble);
  }
}