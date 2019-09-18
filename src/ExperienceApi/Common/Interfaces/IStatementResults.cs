using System;

namespace Doctrina.ExperienceApi
{
    public interface IStatementsResult
    {
        Uri More { get; set; }
        StatementCollection Statements { get; set; }
    }
}