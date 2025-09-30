using Exam_System.Domain.Entities;
using Exam_System.Domain.Enums;
using Exam_System.Feature.User.CheckUserExists;
using Exam_System.Shared.Interface;
using MediatR;
using System.Security.Claims;

namespace Exam_System.Feature.User.RegisterUser
{
    public class RegisterCommandHandler : IRequestHandler<RegisterCommand>
    {
        private readonly IUserRepository _userRepository;
        private readonly IMediator _mediator;

        public RegisterCommandHandler(IUserRepository userRepository, IMediator mediator)
        {
            this._userRepository = userRepository;
            this._mediator = mediator;
        }
        public async Task<Unit> Handle(RegisterCommand request, CancellationToken cancellationToken)
        {
            if ((await _mediator.Send(new CheckUserExistsByEmailQuery(request.Email))).Item1)
                return Result.Failure("Email already exists");

            bool isUserNameExists = (await _mediator.Send(new CheckUserExistsByEmailQuery(request.UserName))).Item1;

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

            return Unit.Value;
        }
    }
}
