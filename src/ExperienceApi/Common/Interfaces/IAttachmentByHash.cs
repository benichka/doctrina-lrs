namespace Doctrina.ExperienceApi
{
    public interface IAttachmentByHash
    {
        Attachment GetAttachmentByHash(string sha2);
    }
}