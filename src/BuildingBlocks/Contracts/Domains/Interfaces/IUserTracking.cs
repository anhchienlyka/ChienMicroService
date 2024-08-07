namespace Contracts.Domains.Interfaces
{
    public interface IUserTracking
    {
        public string CreateBy { get; set; }

        public string LastModifiedBy { get; set; }
    }
}