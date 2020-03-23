// ******************************************************************************
//  © 2019 Sebastiaan Dammann | damsteen.nl
// 
//  File:           : CreatePokerSessionCommandHandler.cs
//  Project         : PokerTime.Application
// ******************************************************************************

namespace PokerTime.Application.Sessions.Commands.CreatePokerSession {
    using System;
    using System.Threading;
    using System.Threading.Tasks;
    using Common;
    using Common.Abstractions;
    using Domain.Entities;
    using Domain.Services;
    using MediatR;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Logging;
    using QRCoder;
    using PokerTime.Common;
    using Services;

    public sealed class CreatePokerSessionCommandHandler : IRequestHandler<CreatePokerSessionCommand, CreatePokerSessionCommandResponse> {
        private readonly IPokerTimeDbContext _pokerTimeDbContext;
        private readonly ISystemClock _systemClock;
        private readonly IUrlGenerator _urlGenerator;
        private readonly IPassphraseService _passphraseService;
        private readonly ILogger<CreatePokerSessionCommandHandler> _logger;

        public CreatePokerSessionCommandHandler(IPokerTimeDbContext pokerTimeDbContext, IPassphraseService passphraseService, ISystemClock systemClock, IUrlGenerator urlGenerator, ILogger<CreatePokerSessionCommandHandler> logger) {
            this._pokerTimeDbContext = pokerTimeDbContext;
            this._passphraseService = passphraseService;
            this._systemClock = systemClock;
            this._urlGenerator = urlGenerator;
            this._logger = logger;
        }

        public async Task<CreatePokerSessionCommandResponse> Handle(CreatePokerSessionCommand request, CancellationToken cancellationToken) {
            if (request == null) throw new ArgumentNullException(nameof(request));

            SymbolSet? symbolSet = await this._pokerTimeDbContext.SymbolSets.FirstOrDefaultAsync(x => x.Id == request.SymbolSetId, cancellationToken);
            if (symbolSet == null) {
                throw new NotFoundException(nameof(SymbolSet), request.SymbolSetId);
            }

            string? HashOptionalPassphrase(string? plainText) {
                return !String.IsNullOrEmpty(plainText) ? this._passphraseService.CreateHashedPassphrase(plainText) : null;
            }

            using var qrCodeGenerator = new QRCodeGenerator();
            var retrospective = new Session {
                CreationTimestamp = this._systemClock.CurrentTimeOffset,
                Title = request.Title,
                HashedPassphrase = HashOptionalPassphrase(request.Passphrase),
                FacilitatorHashedPassphrase = HashOptionalPassphrase(request.FacilitatorPassphrase) ?? throw new InvalidOperationException("No facilitator passphrase given"),
                SymbolSet = symbolSet
            };

            this._logger.LogInformation($"Creating new retrospective with id {retrospective.UrlId}");

            string retroLocation = this._urlGenerator.GenerateUrlToPokerSessionLobby(retrospective.UrlId).ToString();
            var payload = new PayloadGenerator.Url(retroLocation);
            var result = new CreatePokerSessionCommandResponse(
                retrospective.UrlId,
                new QrCode(qrCodeGenerator.CreateQrCode(payload.ToString(), QRCodeGenerator.ECCLevel.L)),
                retroLocation);

            this._pokerTimeDbContext.Sessions.Add(retrospective);

            await this._pokerTimeDbContext.SaveChangesAsync(cancellationToken);

            return result;
        }
    }
}
