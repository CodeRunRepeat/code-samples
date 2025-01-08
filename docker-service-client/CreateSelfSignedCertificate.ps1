# Function to export certificate to PEM format
# From https://craigwilson.blog/post/2024/2024-01-12-export-certs/
function Export-ToPEM {
    param (
        [Parameter(Mandatory=$true)][System.Security.Cryptography.X509Certificates.X509Certificate2]$cert,
        [Parameter(Mandatory=$true)][System.String]$certOutputPath
    )

    # Export the certificate to PEM format
    $certBytes = [System.Security.Cryptography.X509Certificates.X509ContentType]::Cert
    $certEncoded = [System.Convert]::ToBase64String($cert.Export($certBytes), [System.Base64FormattingOptions]::InsertLineBreaks)
    $certPem = "-----BEGIN CERTIFICATE-----`n$certEncoded`n-----END CERTIFICATE-----"
    [System.IO.File]::WriteAllText($certOutputPath, $certPem)
}

$params = @{
    DnsName = 'localhost', 'aspnet-echo-service', 'grpc-echo-service'
    CertStoreLocation = 'Cert:\CurrentUser\My'
    FriendlyName = 'ASP.NET Core HTTPS development certificate'
    TextExtension = @(
        "2.5.29.37={text}1.3.6.1.5.5.7.3.1",
        "1.3.6.1.4.1.311.84.1.1={hex}02")
}

$cert = New-SelfSignedCertificate @params

$certKeyPath = ".\localhost.pfx"
$certPemPath = ".\localhost.pem"

$password = ConvertTo-SecureString '123' -AsPlainText -Force
$cert | Export-PfxCertificate -FilePath $certKeyPath -Password $password
$rootCert = $(Import-PfxCertificate -FilePath $certKeyPath -CertStoreLocation 'Cert:\CurrentUser\Root' -Password $password)

Export-ToPEM $cert $certPemPath

copy $certKeyPath .\aspnet-echo-service\
copy $certKeyPath .\grpc-echo-service\
copy $certPemPath .\test-client\
