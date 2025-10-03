using Exam_System.Domain.Entities;
using Exam_System.Domain.Enums;
using Exam_System.Feature.User.CheckUserExists;
using Exam_System.Shared.Interface;
using Exam_System.Shared.Response;
using Exam_System.Shared.Specification;
using MediatR;
using System.Security.Claims;

namespace Exam_System.Feature.User.RegisterUser
{
    public class RegisterCommandHandler : IRequestHandler<RegisterCommand, ResponseResult<string>>
    {
        private readonly IUserRepository _userRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMediator _mediator;

        public RegisterCommandHandler(IUserRepository userRepository, IUnitOfWork unitOfWork, IMediator mediator)
        {
            this._userRepository = userRepository;
            this._unitOfWork = unitOfWork;
            this._mediator = mediator;
        }
        public async Task<ResponseResult<string>> Handle(RegisterCommand request, CancellationToken cancellationToken)
        {
            if ((await _mediator.Send(new CheckUserExistsQuery(new UserByEmailSpecification(request.Email)))).Item1)
                return ResponseResult<string>.FailResponse("Email already taken");

            if ((await _mediator.Send(new CheckUserExistsQuery(new UserByUserNameSpecification(request.UserName)))).Item1)
                return ResponseResult<string>.FailResponse("UserName already taken");

            var user = new Exam_System.Domain.Entities.User
            {
                FirstName = request.FirstName,
                LastName = request.LastName,
                Email = request.Email,
                UserName = request.UserName,
                Password = request.Password,
                PhoneNumber = request.PhoneNumber,
                RoleId = (int)RoleType.Student,
            };
            await _userRepository.AddAsync(user);

            var Claims = new List<UserClaim>();
            Claims.Add(new UserClaim(user, ClaimTypes.NameIdentifier, user.Id.ToString()));
            Claims.Add(new UserClaim(user, ClaimTypes.Email, user.Email));
            Claims.Add(new UserClaim(user, ClaimTypes.Role, "Student"));

            await _userRepository.AddClaimsAsync(Claims);
            await _unitOfWork.SaveChangesAsync();
            return ResponseResult<string>.SuccessResponse("Email created successfully");
        }
    }
}
