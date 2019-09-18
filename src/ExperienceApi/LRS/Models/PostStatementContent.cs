using Doctrina.ExperienceApi.LRS.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace Doctrina.ExperienceApi.LRS.Models
{
    [ModelBinder(BinderType = typeof(PostStatementsModelBinder))]
    public class PostStatementContent
    {
        [Required]
        public StatementCollection Statements { get; set; }
    }
}
