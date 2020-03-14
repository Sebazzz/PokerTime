// ******************************************************************************
//  © 2019 Sebastiaan Dammann | damsteen.nl
// 
//  File:           : IDatabaseOptions.cs
//  Project         : PokerTime.Persistence
// ******************************************************************************

namespace PokerTime.Persistence {
    public interface IDatabaseOptions {
        string CreateConnectionString();
        DatabaseProvider DatabaseProvider { get; }
    }


    public enum DatabaseProvider {
        SqlServer,
        Sqlite
    }
}
