// ******************************************************************************
//  © 2019 Sebastiaan Dammann | damsteen.nl
// 
//  File:           : IIdPrimaryKey.cs
//  Project         : PokerTime.Domain
// ******************************************************************************

namespace PokerTime.Domain.Abstractions {
    public interface IIdPrimaryKey {
        int Id { get; }
    }
}
