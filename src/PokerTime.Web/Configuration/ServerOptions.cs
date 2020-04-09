// ******************************************************************************
//  © 2019 Sebastiaan Dammann | damsteen.nl
// 
//  File:           : ServerOptions.cs
//  Project         : PokerTime.Web
// ******************************************************************************

namespace PokerTime.Web.Configuration {
    using System.Diagnostics.CodeAnalysis;

    [ExcludeFromCodeCoverage] // Configuration does not need to be automated tested
    public class ServerOptions {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Design", "CA1056:Uri properties should not be strings", Justification = "User configurable")]
        public string? BaseUrl { get; set; }
    }
}
