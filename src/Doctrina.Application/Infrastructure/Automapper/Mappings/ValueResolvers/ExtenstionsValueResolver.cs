using AutoMapper;
using Doctrina.Domain.Entities.OwnedTypes;

namespace Doctrina.Application.Mappings.ValueResolvers
{
    public class ExtenstionsValueResolver :
        IMemberValueResolver<object, object, ExtensionsCollection, ExperienceApi.ExtensionsDictionary>,
        IMemberValueResolver<object, object, ExperienceApi.ExtensionsDictionary, ExtensionsCollection>
    {
        public ExtensionsCollection Resolve(object source, object destination, ExperienceApi.ExtensionsDictionary sourceMember, ExtensionsCollection destMember, ResolutionContext context)
        {
            if(sourceMember == null)
            {
                return null;
            }

            var collection = new ExtensionsCollection();

            foreach (var p in sourceMember)
            {
                collection.Add(p.Key, p.Value);
            }
            return collection;
        }

        public ExperienceApi.ExtensionsDictionary Resolve(object source, object destination, ExtensionsCollection sourceMember, ExperienceApi.ExtensionsDictionary destMember, ResolutionContext context)
        {
            var ext = new ExperienceApi.ExtensionsDictionary();
            foreach(var mem in sourceMember)
            {
                ext.Add(mem);
            }
            return ext;
        }
    }
}
