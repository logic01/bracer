namespace PR.Business.Interfaces
{
    public interface IUserAccountBusiness
    {
        bool Exists(string userName);
    }
}
