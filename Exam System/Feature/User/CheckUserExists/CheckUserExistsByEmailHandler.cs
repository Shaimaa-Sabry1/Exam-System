using Exam_System.Shared.Interface;
using Exam_System.Shared.Specification;
using MediatR;

namespace Exam_System.Feature.User.CheckUserExists
{
    public class CheckUserExistsByEmailHandler : IRequestHandler<CheckUserExistsByEmailQuery, (bool, Exam_System.Domain.Entities.User?)>
    {
        private readonly IUserRepository _userRepository;

        public CheckUserExistsByEmailHandler(IUserRepository userRepository)
        {
            this._userRepository = userRepository;
        }


        public async Task<(bool, Domain.Entities.User?)> Handle(CheckUserExistsByEmailQuery request, CancellationToken cancellationToken)
        => await _userRepository.CheckUserExistAsync(new UserByEmailSpecification(request.Email));


    }
}
