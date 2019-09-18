
using Doctrina.ExperienceApi;
using MediatR;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Doctrina.Application.About.Queries
{
    public class GetAboutQuery : IRequest<ExperienceApi.About>
    {
        public class Handler : IRequestHandler<GetAboutQuery, ExperienceApi.About>
        {
            public Task<ExperienceApi.About> Handle(GetAboutQuery request, CancellationToken cancellationToken)
            {
                var about = new ExperienceApi.About()
                {
                    Version = ApiVersion.GetSupported().Select(x => x.Key)
                };

                return Task.FromResult(about);
            }
        }
    }
}
