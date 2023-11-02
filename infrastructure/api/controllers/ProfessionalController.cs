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
    private readonly IFindPatientUseCase findPatientUseCase;
    private readonly ICreateProfessionalUseCase createProfessionalUseCase;
    private readonly IAddPatientUseCase addPatientUseCase;
    private readonly IUpdateProfessionalUseCase updateProfessionalUseCase;
    private readonly IUpdatePatientUseCase updatePatientUseCase;
    public ProfessionalController(
        IListProfessionalUseCase listProfessionalUseCase,
        IFindProfessionalUseCase findProfessionalUseCase,
        IFindPatientUseCase findPatientUseCase,
        ICreateProfessionalUseCase createProfessionalUseCase,
        IAddPatientUseCase addPatientUseCase,
        IUpdateProfessionalUseCase updateProfessionalUseCase,
        IUpdatePatientUseCase updatePatientUseCase
        )
    {
        this.listProfessionalUseCase = listProfessionalUseCase;
        this.findProfessionalUseCase = findProfessionalUseCase;
        this.createProfessionalUseCase = createProfessionalUseCase;
        this.addPatientUseCase = addPatientUseCase;
        this.updateProfessionalUseCase = updateProfessionalUseCase;
        this.findPatientUseCase = findPatientUseCase;
        this.updatePatientUseCase = updatePatientUseCase;
    }

    [HttpGet]
    public IEnumerable<ProfessionalDefaultDto> List(int PageSize = 20, int PageIndex = 1) //tomei a decisão de não fazer um dto para o controller
    {
        return listProfessionalUseCase.Execute(new(PageSize, PageIndex));
    }

    [HttpGet("{id}")]
    public ProfessionalDefaultDto Find(string id) //tomei a decisão de não fazer um dto para o controller
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
        return new(
            outputDto.Id,
            outputDto.Name,
            outputDto.Email,
            outputDto.Document,
            outputDto.ProfessionalDocument,
            outputDto.Patients ?? Array.Empty<PatientDefaultDto>());
    }

    [HttpPut("{id}")]
    public UpdateProfessionalControllerOutputDto Update(UpdateProfessionalControllerInputDto input, string professionalId)
    {
        var outputDto = updateProfessionalUseCase.Execute(new(professionalId, input.Name, input.Email, input.ProfessionalDocument));
        return new(outputDto.Id, outputDto.Name, outputDto.Email, outputDto.Document, outputDto.ProfessionalDocument);
    }

    [HttpGet("{professionalId}/{patientId}")]
    public PatientDefaultDto FindPatient(string professionalId, string patientId)
    {
        return findPatientUseCase.Execute(new(patientId, professionalId));
    }

    [HttpPut("{professionalId}/{patientId}")]
    public PatientDefaultDto UpdatePatient(UpdatePatientControllerInputDto input, string professionalId, string patientId)
    {
        return updatePatientUseCase.Execute(new(
            professionalId,
            patientId,
            input.Name,
            input.Email,
            input.Document,
            input.IsActive,
            input.FinancialInfo,
            input.PersonalForm
        ));
    }
}
