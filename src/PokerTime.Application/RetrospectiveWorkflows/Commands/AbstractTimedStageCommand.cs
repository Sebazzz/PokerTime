// ******************************************************************************
//  ©  Sebastiaan Dammann | damsteen.nl
// 
//  File:           : AbstractTimedStageCommand.cs
//  Project         : PokerTime.Application
// ******************************************************************************

namespace PokerTime.Application.RetrospectiveWorkflows.Commands {
#nullable disable

    public abstract class AbstractStageCommand {
        public string RetroId { get; set; }

    }

    public abstract class AbstractTimedStageCommand : AbstractStageCommand {
        public int TimeInMinutes { get; set; }
    }
}
