using application.professional;

namespace application;

public interface IAddPatientUseCase : IUsecase<AddPatientInputDto, AddPatientOutputDto> { };
public interface IListProfessionalUseCase : IUsecase<ListProfessionalInputDto, PaginatedList<ListProfessionalOutputDto>> { };
public interface IFindProfessionalUseCase : IUsecase<FindProfessionalInputDto, FindProfessionalOutputDto> { };
public interface ICreateProfessionalUseCase : IUsecase<CreateProfessionalInputDto, CreateProfessionalOutputDto> { };
public interface IUpdateProfessionalUseCase : IUsecase<UpdateProfessionalInputDto, UpdateProfessionalOutputDto> { };