namespace DataModels.Interfaces;

public interface IEchoService
{
    public EchoResponse Echo(string request);
}