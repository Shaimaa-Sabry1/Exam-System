//using Exam_System.Feature.Exams.Commands.Validations;
using FluentValidation; // Add this using directive at the top of the file
using Exam_System.Infrastructure.Persistance;
using Exam_System.Infrastructure.Persistance.Data;
using Exam_System.Infrastructure.Repositories;
using Exam_System.Shared.Interface;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Exam_System.Feature.Exams.Commands.Validations;
using Exam_System.Feature.Exam.UpdateExam;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


builder.Services.AddDbContext<ExamDbContext>(options =>  options.UseSqlServer( builder.Configuration.GetConnectionString("DefaultConnection"))
 );
builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenaricRepository<>));
builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
builder.Services.AddScoped<IExamRepository, ExamRepository>();
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddValidatorsFromAssembly(typeof(CreateExamCommandValidator).Assembly);
builder.Services.AddValidatorsFromAssembly(typeof(UpdateExamCommandValidator).Assembly);



builder.Services.AddMediatR(typeof(Program).Assembly);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


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
