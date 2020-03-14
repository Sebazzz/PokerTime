// ******************************************************************************
//  © 2019 Sebastiaan Dammann | damsteen.nl
// 
//  File:           : IUrlGenerator.cs
//  Project         : PokerTime.Application
// ******************************************************************************

namespace PokerTime.Application.Services {
    using System;
    using Domain.ValueObjects;

    public interface IUrlGenerator {
        Uri GenerateUrlToRetrospectiveLobby(RetroIdentifier urlId);
    }
}
