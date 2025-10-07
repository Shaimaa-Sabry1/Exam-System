using Exam_System.Domain.Entities;
using Exam_System.Feature.Exam.Queries.GetExamById;
using Exam_System.Infrastructure.Repositories;
using Exam_System.Shared.Interface;
using MediatR;

namespace Exam_System.Feature.StartExam.CreateAttembt.Command
{
    public record CreateAttembtCommand(int examId,int userId) :IRequest<int>;

    public class StartExamCommandHandler : IRequestHandler<CreateAttembtCommand, int>
    {
        private readonly GenaricRepository<attembt> _attembtRepository;
        private readonly GenaricRepository<Domain.Entities.Exam> _examRepository;
        private readonly IUnitOfWork _unitOfWork;

        //private readonly IMediator _mediator;

        public StartExamCommandHandler(GenaricRepository<attembt>AttembtRepository, GenaricRepository<Domain.Entities.Exam > examRepository,IUnitOfWork unitOfWork)
        {
            _attembtRepository = AttembtRepository;
            _examRepository = examRepository;
            _unitOfWork = unitOfWork;
            //_mediator = mediator;
        }
        public async Task<int> Handle(CreateAttembtCommand request, CancellationToken cancellationToken)
        {
            //ask i want 5 prob from exam it is correct to get all exam with query or inject new rebo of exam 
            //var exam = await _mediator.Send(new GetExamByIdQuery(request.examId));
            var today = DateTime.Today;
            //  var exam =_attembtRepository.GetAll().Where(e=>e.Exam.Id==request.examId&&e.Exam.StartDate<=today&&e.Exam)
            var exam = await _examRepository.GetByIdAsync(request.examId);
            if (exam == null && exam.StartDate > today && exam.EndDate < today)
            {
                throw new Exception("Exam not found or not active");
            }
            
            var attembt = new attembt() 
            {
              UserId=request.userId,
              startTime=DateTime.Now,
              ExamId=exam.Id
            };  

           await  _attembtRepository.AddAsync(attembt);
           await _unitOfWork.SaveChangesAsync();
                return attembt.Id;



        }
    }
}
