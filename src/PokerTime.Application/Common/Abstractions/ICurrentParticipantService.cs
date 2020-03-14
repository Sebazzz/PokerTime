// ******************************************************************************
//  © 2019 Sebastiaan Dammann | damsteen.nl
// 
//  File:           : ICurrentParticipantService.cs
//  Project         : PokerTime.Application
// ******************************************************************************

namespace PokerTime.Application.Common.Abstractions {
    using System.Threading.Tasks;
    using Models;

    public interface ICurrentParticipantService {
        ValueTask<CurrentParticipantModel> GetParticipant();
        void SetParticipant(CurrentParticipantModel participant);
    }
}
