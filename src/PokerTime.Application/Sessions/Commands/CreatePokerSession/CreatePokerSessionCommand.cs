// ******************************************************************************
//  © 2019 Sebastiaan Dammann | damsteen.nl
// 
//  File:           : CreatePokerSessionCommand.cs
//  Project         : PokerTime.Application
// ******************************************************************************

namespace PokerTime.Application.Sessions.Commands.CreatePokerSession {
    using MediatR;

    public class CreatePokerSessionCommand : IRequest<CreatePokerSessionCommandResponse> {
#nullable disable
        public string Title { get; set; }

        public string FacilitatorPassphrase { get; set; }

#nullable enable
        public string? Passphrase { get; set; }


    }
}
