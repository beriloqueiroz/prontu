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
    PageAble pageAble = new()
    {
      PageIndex = input.PageIndex,
      PageSize = input.PageSize
    };
    PaginatedList<Professional> professionals = ProfessionalGateway.List(pageAble);
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