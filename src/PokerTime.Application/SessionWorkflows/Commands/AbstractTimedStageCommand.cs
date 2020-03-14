// ******************************************************************************
//  ©  Sebastiaan Dammann | damsteen.nl
// 
//  File:           : AbstractTimedStageCommand.cs
//  Project         : PokerTime.Application
// ******************************************************************************

namespace PokerTime.Application.SessionWorkflows.Commands {
#nullable disable

    public abstract class AbstractStageCommand {
        public string SessionId { get; set; }

    }

    public abstract class AbstractTimedStageCommand : AbstractStageCommand {
        public int TimeInMinutes { get; set; }
    }
}
