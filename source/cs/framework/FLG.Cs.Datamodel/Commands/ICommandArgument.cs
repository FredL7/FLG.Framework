using FLG.Cs.Datamodel.Commands;

namespace FLG.Cs.Datamodel.Commands {
    public interface ICommandArgument {
        public ECommandArgumentType Type { get; }
        public object Value { get; }

        public string ToPrettyString();
    }

    public static partial class ICommandArgumentExtension {
        public static ICommandArgument GetCommandArgument(ECommandArgumentType pType, string pValueRaw)
        {
            return pType switch
            {
                // Primitives
                ECommandArgumentType.BOOL => GetCommandArgument(Boolean.Parse(pValueRaw)),
                ECommandArgumentType.INT => GetCommandArgument(Int32.Parse(pValueRaw)),
                ECommandArgumentType.FLOAT => GetCommandArgument(float.Parse(pValueRaw)),
                ECommandArgumentType.STRING => GetCommandArgument(pValueRaw),

                // Structs
                ECommandArgumentType.LOG_ENTRY => CommandArgumentLogEntry.FromRawString(pValueRaw),
                _ => throw new ArgumentException($"Unknown type {pType}"),
            };
        }
    }
}
