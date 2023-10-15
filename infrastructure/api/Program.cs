using application.professional;
using infrastructure.repository;
using repository;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddTransient<IProfessionalGateway, ProfessionalRepository>();
builder.Services.AddTransient<IFindProfessionalUseCase, FindProfessionalUseCase>();
builder.Services.AddTransient<IFindPatientUseCase, FindPatientUseCase>();
builder.Services.AddTransient<IUpdatePatientUseCase, UpdatePatientUseCase>();
builder.Services.AddTransient<IAddPatientUseCase, AddPatientUseCase>();
builder.Services.AddTransient<IListProfessionalUseCase, ListProfessionalUseCase>();
builder.Services.AddTransient<ICreateProfessionalUseCase, CreateProfessionalUseCase>();
builder.Services.AddTransient<IUpdateProfessionalUseCase, UpdateProfessionalUseCase>();
builder.Services.InjectDbContext();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
