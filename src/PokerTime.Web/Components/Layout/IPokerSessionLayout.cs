// ******************************************************************************
//  © 2019 Sebastiaan Dammann | damsteen.nl
// 
//  File:           : IPokerSessionLayout.cs
//  Project         : PokerTime.Web
// ******************************************************************************

namespace PokerTime.Web.Components.Layout {
    using System;
    using Domain.Entities;

    public interface IPokerSessionLayout {
        void Update(in PokerSessionLayoutInfo layoutInfo);
    }

    public readonly struct PokerSessionLayoutInfo : IEquatable<PokerSessionLayoutInfo> {
        public SessionStage? Stage { get; }

        public string Title { get; }

        public PokerSessionLayoutInfo(string title) : this() {
            this.Title = title;
        }

        public PokerSessionLayoutInfo(string title, SessionStage? stage) {
            this.Stage = stage;
            this.Title = title;
        }

        public bool Equals(PokerSessionLayoutInfo other) => this.Stage == other.Stage && this.Title == other.Title;

        public override bool Equals(object? obj) => obj is PokerSessionLayoutInfo other && this.Equals(other);

        public override int GetHashCode() {
            unchecked {
                return (this.Stage.GetHashCode() * 397) ^ (this.Title != null ? this.Title.GetHashCode(StringComparison.InvariantCulture) : 0);
            }
        }

        public static bool operator ==(PokerSessionLayoutInfo left, PokerSessionLayoutInfo right) => left.Equals(right);

        public static bool operator !=(PokerSessionLayoutInfo left, PokerSessionLayoutInfo right) => !left.Equals(right);
    }
}
