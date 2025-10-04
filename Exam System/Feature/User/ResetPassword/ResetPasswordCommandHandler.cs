using Exam_System.Domain.Entities;
using Exam_System.Feature.User.ChangePassword;
using Exam_System.Shared.Interface;
using Exam_System.Shared.Response;
using Exam_System.Shared.Specification;
using MediatR;

namespace Exam_System.Feature.User.ResetPassword
{
    public class ResetPasswordCommandHandler : IRequestHandler<ResetPasswordCommand, ResponseResult<string>>
    {
        private readonly IMediator _mediator;
        private readonly IGenericRepository<UserToken> _repository;

        public ResetPasswordCommandHandler(IMediator mediator, IGenericRepository<UserToken> repository)
        {
            _mediator = mediator;
            _repository = repository;
        }
        public async Task<ResponseResult<string>> Handle(ResetPasswordCommand request, CancellationToken cancellationToken)
        {
            var userToken = await _repository.GetByCretireaAsync(new TokenSpecification(request.PasswordToken));
            if (userToken is null || userToken.ExpiredDate < DateTime.UtcNow)
            {
                return ResponseResult<string>.FailResponse("Invalid or expired token.");
            }
            await _repository.DeleteAsync(userToken);
            await _mediator.Send(new ChangePasswordCommand(userToken.UserId, request.NewPassword));
            return ResponseResult<string>.SuccessResponse("Password reset successful.");
        }
    }
}
