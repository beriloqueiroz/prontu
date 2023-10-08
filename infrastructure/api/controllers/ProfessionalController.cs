using application.professional;
using infrastructure.controller;
using Microsoft.AspNetCore.Mvc;

namespace api.controllers;

[ApiController]
[Route("api/professional/")]
public class ProfessionalController : ControllerBase
{
    private readonly IListProfessionalUseCase listProfessionalUseCase;
    private readonly IFindProfessionalUseCase findProfessionalUseCase;
    private readonly ICreateProfessionalUseCase createProfessionalUseCase;
    private readonly IAddPatientUseCase addPatientUseCase;
    private readonly IUpdateProfessionalUseCase updateProfessionalUseCase;
    public ProfessionalController(
        IListProfessionalUseCase listProfessionalUseCase,
        IFindProfessionalUseCase findProfessionalUseCase,
        ICreateProfessionalUseCase createProfessionalUseCase,
        IAddPatientUseCase addPatientUseCase,
        IUpdateProfessionalUseCase updateProfessionalUseCase
        )
    {
        this.listProfessionalUseCase = listProfessionalUseCase;
        this.findProfessionalUseCase = findProfessionalUseCase;
        this.createProfessionalUseCase = createProfessionalUseCase;
        this.addPatientUseCase = addPatientUseCase;
        this.updateProfessionalUseCase = updateProfessionalUseCase;
    }

    [HttpGet]
    public IEnumerable<ListProfessionalOutputDto> List(int PageSize = 20, int PageIndex = 1) //tomei a decisão de não fazer um dto para o controller
    {
        return listProfessionalUseCase.Execute(new(PageSize, PageIndex));
    }

    [HttpGet("{id}")]
    public FindProfessionalOutputDto Find(string id) //tomei a decisão de não fazer um dto para o controller
    {
        return findProfessionalUseCase.Execute(new(id));
    }

    [HttpPost]
    public CreateProfessionalOutputDto Create(CreateProfessionalInputDto input) //tomei a decisão de não fazer um dto para o controller
    {
        return createProfessionalUseCase.Execute(input);
    }

    [HttpPost("{professionalId}")]
    public AddPatientControllerOutputDto AddPatient(AddPatientControllerInputDto input, string professionalId)
    {
        var outputDto = addPatientUseCase.Execute(new(
            professionalId,
            input.Name,
            input.Email,
            input.Document
        ));
        return new(outputDto.Id, outputDto.Name, outputDto.Email, outputDto.Document, outputDto.ProfessionalDocument, outputDto.Patients);
    }

    [HttpPut(Name = "{id}")]
    public UpdateProfessionalControllerOutputDto Update(UpdateProfessionalControllerInputDto input, string id)
    {
        var outputDto = updateProfessionalUseCase.Execute(new(id, input.Name, input.Email, input.ProfessionalDocument, input.Patients));
        return new(outputDto.Id, outputDto.Name, outputDto.Email, outputDto.Document, outputDto.ProfessionalDocument, outputDto.Patients);
    }
}
