//using Exam_System.Feature.Exams.Commands.Validations;
using Exam_System.Feature.Exam.UpdateExam;
using Exam_System.Feature.Exams.Commands.Validations;
using Exam_System.Feature.Exams.Commands.Validations;
using Exam_System.Feature.User.RegisterUser;
using Exam_System.Infrastructure.Persistance;
using Exam_System.Infrastructure.Persistance.Data;
using Exam_System.Infrastructure.Repositories;
using Exam_System.Shared;
using Exam_System.Shared.Cofiguration;
using Exam_System.Shared.Extenstions;
using Exam_System.Shared.Interface;
using Exam_System.Shared.Middlewares;
using Exam_System.Shared.Services;
using FluentValidation; // Add this using directive at the top of the file
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;




var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers().ConfigureApiBehaviorOptions(
    option => option.SuppressModelStateInvalidFilter = true
);
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


builder.Services.AddDbContext<ExamDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"))
 );
builder.Services.AddScoped(typeof(GenaricRepository<>));
builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenaricRepository<>));
builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IExamRepository, ExamRepository>();
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddValidatorsFromAssembly(typeof(RegisterCommandValidator).Assembly);
builder.Services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));

builder.Services.Configure<EmailSettings>(
    builder.Configuration.GetSection("EmailSettings"));

builder.Services.AddTransient<EmailVerificationService>();
builder.Services.AddMemoryCache();


builder.Services.AddMediatR(typeof(Program).Assembly);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddJwtAuthentication(builder.Configuration);


var app = builder.Build();
app.UseMiddleware<ExceptionHandlingMiddleware>();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
