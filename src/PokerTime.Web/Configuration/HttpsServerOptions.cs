// ******************************************************************************
//  © 2019 Sebastiaan Dammann | damsteen.nl
// 
//  File:           : HttpsServerOptions.cs
//  Project         : PokerTime.Web
// ******************************************************************************

namespace PokerTime.Web.Configuration {
    using System.Diagnostics.CodeAnalysis;

    [ExcludeFromCodeCoverage] // Configuration does not need to be automated tested
    public sealed class HttpsServerOptions {
        public string? CertificatePath { get; set; }
        public string? CertificatePassword { get; set; }

        public bool EnableRedirect { get; set; }

        public bool UseStrongHttps { get; set; }
    }
}
