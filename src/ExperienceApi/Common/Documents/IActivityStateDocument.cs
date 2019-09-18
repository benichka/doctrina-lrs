using System;

namespace Doctrina.ExperienceApi.Documents
{
    public interface IActivityStateDocument : IDocument
    {
        Activity Activity { get; set; }
        Agent Agent { get; set; }
        Guid? Registration { get; set; }
    }
}