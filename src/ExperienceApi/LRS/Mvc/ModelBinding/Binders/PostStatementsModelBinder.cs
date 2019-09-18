﻿using Doctrina.ExperienceApi.Client;
using Doctrina.ExperienceApi.LRS.Exceptions;
using Doctrina.ExperienceApi.LRS.Models;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace Doctrina.ExperienceApi.LRS.Mvc.ModelBinding
{
    public class PostStatementsModelBinder : IModelBinder
    {
        public async Task BindModelAsync(ModelBindingContext bindingContext)
        {
            if (bindingContext.ModelType != typeof(PostStatementContent))
            {
                return;
            }

            var model = new PostStatementContent();

            var request = bindingContext.ActionContext.HttpContext.Request;

            var contentType = MediaTypeHeaderValue.Parse(request.ContentType);

            try
            {
                var jsonModelReader = new JsonModelReader(contentType, request.Body);
                model.Statements = await jsonModelReader.ReadAs<StatementCollection>();
            }
            catch (JsonModelReaderException ex)
            {
                throw new BadRequestException(ex.Message);
            }

            if(model.Statements == null)
            {
                bindingContext.Result = ModelBindingResult.Failed();
            }
            else
            {
                bindingContext.Result = ModelBindingResult.Success(model);
            }
        }
    }
}
