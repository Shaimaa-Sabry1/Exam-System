using Exam_System.Shared.Response;
using MediatR;

namespace Exam_System.Feature.Dashboard
{
    public class GetDashboardReportQuery:IRequest<ResponseResult<DashboardReportDto>>
    {
    }
}
