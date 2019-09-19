using Doctrina.Application.Interfaces;
using Doctrina.ExperienceApi.Data;

namespace Doctrina.ExperienceApi.LRS.Authentication
{
    public class RequestAuthority : ICurrentAuthority
    {
        public Agent Authority { get; set; }
    }
}