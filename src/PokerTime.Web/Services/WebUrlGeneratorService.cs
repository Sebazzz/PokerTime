// ******************************************************************************
//  © 2019 Sebastiaan Dammann | damsteen.nl
// 
//  File:           : WebUrlGeneratorService.cs
//  Project         : PokerTime.Web
// ******************************************************************************

namespace PokerTime.Web.Services {
    using System;
    using Application.Services;
    using Domain.ValueObjects;

    public class WebUrlGenerator : IUrlGenerator {
        private readonly ISiteUrlDetectionService _siteUrlDetectionService;

        public WebUrlGenerator(ISiteUrlDetectionService siteUrlDetectionService) {
            this._siteUrlDetectionService = siteUrlDetectionService;
        }

        public Uri GenerateUrlToPokerSessionLobby(SessionIdentifier urlId) {
            if (urlId == null) throw new ArgumentNullException(nameof(urlId));

            var uriBuilder = new UriBuilder(this._siteUrlDetectionService.GetSiteUrl()) {
                Path = $"/pokertime-session/{urlId.StringId}/join"
            };

            return uriBuilder.Uri;
        }
    }
}
