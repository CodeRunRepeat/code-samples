namespace GrpcEchoService.Protos;
public static class Extensions
{
    public static string ToDataModel(this EchoRequest request) => request.Content;
    public static EchoRequest ToProtobuf(this string request) => new EchoRequest { Content = request };
    public static DataModels.EchoResponse ToDataModel(this EchoResponse response) =>
        new DataModels.EchoResponse(response.Content, response.Count);
    public static EchoResponse ToProtobuf(this DataModels.EchoResponse response) =>
        new EchoResponse { Content = response.content, Count = response.count };
}

public partial class EchoRequest
{
    public static implicit operator string(EchoRequest request) => request.ToDataModel();
    public static implicit operator EchoRequest(string request) => request.ToProtobuf();
}
public partial class EchoResponse
{
    public static implicit operator DataModels.EchoResponse(EchoResponse response) => response.ToDataModel();
    public static implicit operator EchoResponse(DataModels.EchoResponse response) => response.ToProtobuf();
}

