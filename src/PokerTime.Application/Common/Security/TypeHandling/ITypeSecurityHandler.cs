// ******************************************************************************
//  © 2019 Sebastiaan Dammann | damsteen.nl
// 
//  File:           : ITypeSecurityHandler.cs
//  Project         : PokerTime.Application
// ******************************************************************************

namespace PokerTime.Application.Common.Security.TypeHandling {
    using Domain.Entities;
    using Models;

    internal interface ITypeSecurityHandler {
        void HandleOperation(SecurityOperation operation, Session session, object entity, in CurrentParticipantModel currentParticipant);
    }
}
