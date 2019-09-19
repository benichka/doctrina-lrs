﻿using Doctrina.ExperienceApi.Data;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.ModelBinding.Binders;

namespace Doctrina.ExperienceApi.LRS.Mvc.ModelBinding.Providers
{
    public class AgentModelBinderProvider : IModelBinderProvider
    {
        public IModelBinder GetBinder(ModelBinderProviderContext context)
        {
            if (!context.Metadata.IsComplexType) return null;

            var propName = context.Metadata.PropertyName;
            if (propName == null) return null;

            var modelType = context.Metadata.ModelType;
            if (modelType == null) return null;

            if (modelType != typeof(Agent)) return null;

            return new BinderTypeModelBinder(typeof(AgentModelBinder));
        }
    }
}
