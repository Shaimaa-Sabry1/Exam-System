using Exam_System.Shared.Interface;
using Exam_System.Shared.Specification;
using MediatR;

namespace Exam_System.Feature.User.CheckUserExists
{
    public class CheckUserExistsHandler : IRequestHandler<CheckUserExistsQuery, (bool, Exam_System.Domain.Entities.User?)>
    {
        private readonly IUserRepository _userRepository;

        public CheckUserExistsHandler(IUserRepository userRepository)
        {
            this._userRepository = userRepository;
        }


        public async Task<(bool, Domain.Entities.User?)> Handle(CheckUserExistsQuery request, CancellationToken cancellationToken)
        => await _userRepository.CheckUserExistAsync(request.specification);


    }
}
