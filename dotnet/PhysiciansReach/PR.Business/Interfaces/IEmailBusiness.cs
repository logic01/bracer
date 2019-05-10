namespace PR.Business.Interfaces
{
    public interface IEmailBusiness
    {
        bool SendEmail(int documentId, string emailAddress);
    }
}
