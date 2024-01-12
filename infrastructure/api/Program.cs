using application.professional;
using infrastructure.repository;
using repository;

var builder = WebApplication.CreateBuilder(args);
var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";

builder.Services.AddControllers();
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
builder.Services.AddScoped<IChangeProfessionalEmailUsecase, ChangeProfessionalEmailUseCase>();
builder.Services.AddScoped<IUpdatePatientFinancialInfoUseCase, UpdatePatientFinancialInfoUseCase>();
builder.Services.AddScoped<IUpdatePatientPersonalFormUseCase, UpdatePatientPersonalFormUseCase>();
builder.Services.InjectDbContext(builder.Configuration);

builder.Services.AddCors(options =>
{
    options.AddPolicy(name: MyAllowSpecificOrigins,
                      policy =>
                      {
                          policy.WithOrigins("*");
                      });
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

if (app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/error-development");
}
else
{
    app.UseExceptionHandler("/error");
}

app.UseAuthorization();

app.MapControllers();

app.UseCors(MyAllowSpecificOrigins);

app.Run();
