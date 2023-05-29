using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Prokompetence.Common.Configuration;

namespace Prokompetence.DAL.Postgres;

public sealed class PostgresProkompetenceDbContext : ProkompetenceDbContext
{
    public PostgresProkompetenceDbContext(ConnectionStrings connectionStrings)
        : this(Options(connectionStrings.PostgresProkompetence))
    {
    }

    public PostgresProkompetenceDbContext(DbContextOptions options)
        : base(options)
    {
    }

    private static DbContextOptions Options(string connectionString) =>
        new DbContextOptionsBuilder()
            .UseNpgsql(connectionString,
                b => b.RemoteCertificateValidationCallback(RemoteCertificateValidationCallbackInternal))
            .Options;

    private static bool RemoteCertificateValidationCallbackInternal(
        object sender,
        X509Certificate certificate,
        X509Chain defaultChain,
        SslPolicyErrors defaultErrors
    )
    {
        var text1 = File.ReadAllText("CA1.pem");
        var text2 = File.ReadAllText("CA2.pem");

        var ca1Cert = new X509Certificate2(Encoding.UTF8.GetBytes(text1));
        var ca2Cert = new X509Certificate2(Encoding.UTF8.GetBytes(text2));

        var caCertChain = new X509Chain
        {
            ChainPolicy = new X509ChainPolicy
            {
                RevocationMode = X509RevocationMode.NoCheck,
                RevocationFlag = X509RevocationFlag.EntireChain,
            }
        };

        caCertChain.ChainPolicy.ExtraStore.Add(ca1Cert);
        caCertChain.ChainPolicy.ExtraStore.Add(ca2Cert);

        var serverCert = new X509Certificate2(certificate);
        caCertChain.Build(serverCert);

        return caCertChain.ChainStatus.Length == 0 ||
               caCertChain.ChainStatus.All(status => status.Status == X509ChainStatusFlags.UntrustedRoot);
    }
}