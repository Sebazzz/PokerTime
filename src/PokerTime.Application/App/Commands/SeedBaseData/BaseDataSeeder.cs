// ******************************************************************************
//  © 2019 Sebastiaan Dammann | damsteen.nl
// 
//  File:           : BaseDataSeeder.cs
//  Project         : PokerTime.Application
// ******************************************************************************

namespace PokerTime.Application.App.Commands.SeedBaseData {
    using System;
    using System.Drawing;
    using System.Globalization;
    using System.Threading;
    using System.Threading.Tasks;
    using Common.Abstractions;
    using Domain.Entities;
    using Microsoft.EntityFrameworkCore;

    internal sealed class BaseDataSeeder {
        private readonly IPokerTimeDbContext _pokerTimeDbContext;

        public BaseDataSeeder(IPokerTimeDbContext pokerTimeDbContext) {
            this._pokerTimeDbContext = pokerTimeDbContext;
        }

        public async Task SeedAllAsync(CancellationToken cancellationToken) {
            await this.SeedPredefinedParticipantColor(cancellationToken);
            await this.SeedPokerCardSymbols(cancellationToken);

            await this._pokerTimeDbContext.SaveChangesAsync(cancellationToken);
        }

        private async Task SeedPokerCardSymbols(CancellationToken cancellationToken) {
            int order = 1;

            async Task SeedSymbolSet(string name, Action<SymbolSet> callback) {
                SymbolSet symbolSet = await this._pokerTimeDbContext.SymbolSets.FirstOrDefaultAsync(x => x.Name == name, cancellationToken);

                if (symbolSet == null) {
                    // Reset order
                    order = 1;

                    // Callback to create new one
                    symbolSet = new SymbolSet { Name = name };
                    callback.Invoke(symbolSet);
                }
            }

            void IntSymbol(int num, SymbolSet symbolSet) {
                this._pokerTimeDbContext.Symbols.Add(new Symbol {
                    Type = SymbolType.Number,
                    SymbolSet = symbolSet,
                    ValueAsNumber = num,
                    ValueAsString = num.ToString(CultureInfo.InvariantCulture),
                    Order = order++
                });
            }

            void StringSymbol(string str, SymbolSet symbolSet, int? intValue = null) {
                this._pokerTimeDbContext.Symbols.Add(new Symbol {
                    Type = SymbolType.Characters,
                    SymbolSet = symbolSet,
                    ValueAsString = str,
                    ValueAsNumber = intValue,
                    Order = order++
                });
            }

            void TypeSymbol(SymbolType symbolType, SymbolSet symbolSet) {
                string symbolString = symbolType switch
                {
                    SymbolType.Break => "☕",
                    SymbolType.Infinite => "∞",
                    _ => throw new ArgumentOutOfRangeException(nameof(symbolType), symbolType, null)
                };

                this._pokerTimeDbContext.Symbols.Add(new Symbol {
                    Type = symbolType,
                    SymbolSet = symbolSet,
                    ValueAsString = symbolString,
                    Order = order++
                });
            }

            // Seed symbol sets - note: when changing the default names a migration is necessary to prevent reseeding
            // ... default
            await SeedSymbolSet("Default", symbolSet => {
                IntSymbol(0, symbolSet);
                IntSymbol(1, symbolSet);
                IntSymbol(2, symbolSet);
                IntSymbol(3, symbolSet);
                IntSymbol(5, symbolSet);
                IntSymbol(8, symbolSet);
                IntSymbol(13, symbolSet);
                IntSymbol(20, symbolSet);
                IntSymbol(40, symbolSet);
                IntSymbol(100, symbolSet);

                TypeSymbol(SymbolType.Break, symbolSet);
                TypeSymbol(SymbolType.Infinite, symbolSet);
                StringSymbol("?", symbolSet);
            });

            // ... t-shirt sizes
            await SeedSymbolSet("T-shirt sizes", symbolSet => {
                StringSymbol("XS", symbolSet, 1);
                StringSymbol("S", symbolSet, 2);
                StringSymbol("M", symbolSet, 3);
                StringSymbol("L", symbolSet, 4);
                StringSymbol("XL", symbolSet, 5);

                TypeSymbol(SymbolType.Break, symbolSet);
                StringSymbol("?", symbolSet);
            });

            // ... fibonacci
            await SeedSymbolSet("Fibonacci", symbolSet => {
                IntSymbol(0, symbolSet);
                IntSymbol(1, symbolSet);
                IntSymbol(2, symbolSet);
                IntSymbol(3, symbolSet);
                IntSymbol(5, symbolSet);
                IntSymbol(8, symbolSet);
                IntSymbol(13, symbolSet);
                IntSymbol(21, symbolSet);
                IntSymbol(34, symbolSet);
                IntSymbol(55, symbolSet);
                IntSymbol(89, symbolSet);

                TypeSymbol(SymbolType.Break, symbolSet);
                StringSymbol("?", symbolSet);
            });

            // ... p²
            await SeedSymbolSet("Powers of two", symbolSet => {
                IntSymbol(0, symbolSet);
                IntSymbol(1, symbolSet);
                IntSymbol(2, symbolSet);
                IntSymbol(4, symbolSet);
                IntSymbol(8, symbolSet);
                IntSymbol(16, symbolSet);
                IntSymbol(32, symbolSet);
                IntSymbol(64, symbolSet);

                TypeSymbol(SymbolType.Break, symbolSet);
                StringSymbol("?", symbolSet);
            });
        }

        private async Task SeedPredefinedParticipantColor(CancellationToken cancellationToken) {
            if (await this._pokerTimeDbContext.PredefinedParticipantColors.AnyAsync(cancellationToken)) {
                return;
            }

            // Seed note lanes
            this._pokerTimeDbContext.PredefinedParticipantColors.AddRange(
                new PredefinedParticipantColor("Driver red", Color.Red),
                new PredefinedParticipantColor("Analytic blue", Color.Blue),
                new PredefinedParticipantColor("Amiable green", Color.Green),
                new PredefinedParticipantColor("Expressive yellow", Color.Yellow),
                new PredefinedParticipantColor("Juicy orange", Color.DarkOrange),
                new PredefinedParticipantColor("Participator purple", Color.Purple),
                new PredefinedParticipantColor("Boring blue-gray", Color.DarkSlateGray),
                new PredefinedParticipantColor("Adapting aquatic", Color.DodgerBlue),
                new PredefinedParticipantColor("Fresh lime", Color.Lime),
                new PredefinedParticipantColor("Tomàto tomató", Color.Tomato),
                new PredefinedParticipantColor("Goldie the bird", Color.Gold),
                new PredefinedParticipantColor("Farmer wheat", Color.Wheat)
            );
        }
    }
}
