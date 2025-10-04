using Exam_System.Feature.User.CheckUserExists;
using Exam_System.Feature.User.CreateUserToken;
using Exam_System.Feature.User.ForgetPassword;
using Exam_System.Shared.Response;
using Exam_System.Shared.Services;
using Exam_System.Shared.Specification;
using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;

namespace Exam_System.Feature.User.ResetPassword
{
    public class ForgetPasswordCommandOrchestrator : IRequestHandler<ForgetPasswordCommand, ResponseResult<ForgetPasswordResponse>>
    {
        private readonly IMediator _mediator;
        private readonly EmailVerificationService _emailService;

        public ForgetPasswordCommandOrchestrator(IMediator mediator, EmailVerificationService emailService)
        {
            _mediator = mediator;
            _emailService = emailService;
        }

        public async Task<ResponseResult<ForgetPasswordResponse>> Handle(ForgetPasswordCommand request, CancellationToken cancellationToken)
        {
            var userTuble = await _mediator.Send(new CheckUserExistsQuery(new UserByEmailSpecification(request.Email)));

            if (!userTuble.isExist)
                return ResponseResult<ForgetPasswordResponse>.FailResponse("invalid Email");

            var user = userTuble.user;
            var code = _emailService.GenerateVerificationCode();
            
            await _mediator.Send(new CreateUserTokenCommand(user.Id, code));

            await _emailService.SendVerificationEmailAsync(user.Email, code);

            return ResponseResult<ForgetPasswordResponse>.SuccessResponse(new ForgetPasswordResponse
            {
                Id= user.Id,
                TokenPassword = code
            }, "we sent email");

        }
    }
}
