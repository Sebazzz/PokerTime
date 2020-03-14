// ******************************************************************************
//  © 2019 Sebastiaan Dammann | damsteen.nl
// 
//  File:           : OperationSecurityException.cs
//  Project         : PokerTime.Application
// ******************************************************************************

namespace PokerTime.Application.Common.Security {
    using System;

    public sealed class OperationSecurityException : Exception {
        public OperationSecurityException() {
        }

        public OperationSecurityException(string message) : base(message) {
        }

        public OperationSecurityException(string message, Exception innerException) : base(message, innerException) {
        }
    }
}
