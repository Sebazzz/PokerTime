// ******************************************************************************
//  © 2019 Sebastiaan Dammann | damsteen.nl
// 
//  File:           : CurrentParticipantModel.cs
//  Project         : PokerTime.Application
// ******************************************************************************

namespace PokerTime.Application.Common.Models {
    using System;

    public readonly struct CurrentParticipantModel : IEquatable<CurrentParticipantModel> {
        public int Id { get; }
        public string? Name { get; }
        public string? HexColorString { get; }
        public bool IsFacilitator { get; }
        public bool IsAuthenticated => this.Id != 0;

        public CurrentParticipantModel(int id, string? name, string? color, bool isFacilitator) {
            this.Id = id;
            this.Name = name;
            this.HexColorString = color;
            this.IsFacilitator = isFacilitator;
        }

        public bool Equals(CurrentParticipantModel other) => this.Id == other.Id;

        public override bool Equals(object? obj) => obj is CurrentParticipantModel other && this.Equals(other);

        public override int GetHashCode() => this.Id;

        public static bool operator ==(CurrentParticipantModel left, CurrentParticipantModel right) => left.Equals(right);

        public static bool operator !=(CurrentParticipantModel left, CurrentParticipantModel right) => !left.Equals(right);

        public void Deconstruct(out int participantId, out string? name, out string? color, out bool isFacilitator) {
            participantId = this.Id;
            name = this.Name;
            color = this.HexColorString;
            isFacilitator = this.IsFacilitator;
        }

        public override string ToString() => $"[{this.Id}|{(this.IsFacilitator ? "M" : "P")}|{this.Name}]";
    }
}
