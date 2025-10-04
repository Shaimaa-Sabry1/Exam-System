using Exam_System.Shared.Interface;
using MediatR;

namespace Exam_System.Feature.User.ChangePassword
{
    public class ChangePasswordCommandHandler : IRequestHandler<ChangePasswordCommand>
    {
        private readonly IUserRepository _userRepository;

        public ChangePasswordCommandHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        public async Task<Unit> Handle(ChangePasswordCommand request, CancellationToken cancellationToken)
        {
            await _userRepository.ChangePasswordAsync(request.UserId, request.NewPassword);
            return Unit.Value;
        }
    }
}
