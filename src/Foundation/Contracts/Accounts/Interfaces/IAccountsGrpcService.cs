namespace Microservices.Grpc.Accounts.Contracts.Interfaces
{
    [ServiceContract]
    public interface IAccountsGrpcService
    {
        [OperationContract]
        public Task<string> DemoTest(string request, CallContext context = default);

    }
}


