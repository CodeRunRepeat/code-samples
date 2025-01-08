# How to run this sample

Open the solution in Visual Studio.

Before the first build, run the `CreateSelfSignedCertificate.ps1` script. 
This script uses the `New-SelfSignedCertificate` cmdlet to generate a self-signed certificate
that is compatible as an ASP.NET self-signed certificate (see the `TextExtension` properties)
and then export it and copy it into the directories of the services that will use it
(`aspnet-echo-service` as pfx, `grpc-echo-service` as pfx, `test-client` as pem).

`docker-compose.override.yml` sets the Kestrel certificate to the pfx for the two services,
and includes `/app` to `SSL_CERT_PATH` in the client.

Set `docker-compose` as the startup project and run or debug the solution. The file does
not include a healthcheck as a prerequisite to run the client, so it may run before the two 
services are up. You can set a breakpoint in the client to wait for the services to be up.

## References

- [Run an ASP.NET Core app in Docker containers](https://learn.microsoft.com/en-us/aspnet/core/host-and-deploy/docker/building-net-docker-images)
- [ASP.NET Core certificate validation](https://github.com/dotnet/aspnetcore/blob/c4d043dc86e52e8b83f8c0ef3500f2985f90c23c/src/Shared/CertificateGeneration/CertificateManager.cs#L66)
- [Create a self-signed certificate with PowerShell](https://learn.microsoft.com/en-us/powershell/module/pki/new-selfsignedcertificate)
- [Export a .pem file from a certificate in PowerShell](https://craigwilson.blog/post/2024/2024-01-12-export-certs/)
- [Docker compose with HTTPS](https://learn.microsoft.com/en-us/aspnet/core/security/docker-compose-https)
