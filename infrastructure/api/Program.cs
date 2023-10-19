using application.professional;
using domain;
using infrastructure.repository;
using repository;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<IProfessionalGateway, ProfessionalRepository>();
builder.Services.AddScoped<IFindProfessionalUseCase, FindProfessionalUseCase>();
builder.Services.AddScoped<IFindPatientUseCase, FindPatientUseCase>();
builder.Services.AddScoped<IUpdatePatientUseCase, UpdatePatientUseCase>();
builder.Services.AddScoped<IAddPatientUseCase, AddPatientUseCase>();
builder.Services.AddScoped<IListProfessionalUseCase, ListProfessionalUseCase>();
builder.Services.AddScoped<ICreateProfessionalUseCase, CreateProfessionalUseCase>();
builder.Services.AddScoped<IUpdateProfessionalUseCase, UpdateProfessionalUseCase>();
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
