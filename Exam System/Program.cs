//using Exam_System.Feature.Exams.Commands.Validations;
using Exam_System.Feature.Exam.UpdateExam;
using Exam_System.Feature.Exams.Commands.Validations;
using Exam_System.Feature.Exams.Commands.Validations;
using Exam_System.Feature.Questions.AddQuestions;
using Exam_System.Feature.Questions.EditQuestion;
using Exam_System.Feature.User.RegisterUser;
using Exam_System.Infrastructure.Persistance;
using Exam_System.Infrastructure.Persistance.Data;
using Exam_System.Infrastructure.Repositories;
using Exam_System.Service;
using Exam_System.Shared;
using Exam_System.Shared.Cofiguration;
using Exam_System.Shared.Extenstions;
using Exam_System.Shared.Helpers;
using Exam_System.Shared.Interface;
using Exam_System.Shared.Middlewares;
using Exam_System.Shared.Services;
using FluentValidation; // Add this using directive at the top of the file
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using StackExchange.Redis;




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
builder.Services.AddStackExchangeRedisCache(options =>
{
    options.Configuration = "redis-18828.c16.us-east-1-3.ec2.redns.redis-cloud.com:18828,password=sKd8WfR2OctEuTiPXH5iqeQjS75xFlkl";
    options.InstanceName = "ExamSystem_";
}); 


builder.Services.AddScoped(typeof(GenaricRepository<>));
builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenaricRepository<>));
builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IExamRepository, ExamRepository>();
builder.Services.AddScoped<IQuestionRepository, QuestionRepository>();
builder.Services.AddScoped<IAddQuestionOrchestrator, AddQuestionOrchestrator>();
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<IImageHelper, Exam_System.Service.ImageService>();
builder.Services.AddScoped<IShuffleService, ShuffleService>();
builder.Services.AddValidatorsFromAssembly(typeof(RegisterCommandValidator).Assembly);
builder.Services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));

builder.Services.Configure<EmailSettings>(
    builder.Configuration.GetSection("EmailSettings"));

builder.Services.AddTransient<EmailVerificationService>();
builder.Services.AddMemoryCache();
// Program.cs
builder.Services.AddDistributedMemoryCache();

builder.Services.AddMediatR(typeof(Program).Assembly);

//builder.Services.AddControllers();
//builder.Services.AddEndpointsApiExplorer();
//builder.Services.AddSwaggerGen();

builder.Services.AddJwtAuthentication(builder.Configuration);
 

var app = builder.Build();
app.UseMiddleware<ExceptionHandlingMiddleware>();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseStaticFiles();
app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
