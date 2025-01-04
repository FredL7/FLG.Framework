using System.Text;

using FLG.Cs.Datamodel.Logger;


namespace FLG.Cs.Datamodel.Commands {
    public static partial class ICommandArgumentExtension {
        internal static ICommandArgument GetCommandArgument(LogEntry value) => new CommandArgumentLogEntry(value);
    }

    internal readonly struct CommandArgumentLogEntry(LogEntry value) : ICommandArgument {
        private readonly LogEntry _value = value;
        public object Value { get => _value; }
        public readonly ECommandArgumentType Type { get => CommandArgumentTypeExtension.FromType(_value); }

        public readonly string ToPrettyString()
        {
            StringBuilder sb = new();
            sb.Append(_value.identifier); sb.Append(CommandConstants.REFLECTION_ARG_SEPARATOR);
            sb.Append(_value.networkingIdentifier); sb.Append(CommandConstants.REFLECTION_ARG_SEPARATOR);
            sb.Append(_value.classname ?? ""); sb.Append(CommandConstants.REFLECTION_ARG_SEPARATOR);
            sb.Append(_value.methodname ?? ""); sb.Append(CommandConstants.REFLECTION_ARG_SEPARATOR);
            sb.Append(_value.date.Ticks); sb.Append(CommandConstants.REFLECTION_ARG_SEPARATOR);
            sb.Append((int)_value.severity); sb.Append(CommandConstants.REFLECTION_ARG_SEPARATOR);
            sb.Append(_value.message);
            return sb.ToString();
        }

        public static CommandArgumentLogEntry FromRawString(string rawString)
        {
            string[] values = rawString.Split(CommandConstants.REFLECTION_ARG_SEPARATOR);
            LogEntry value = new()
            {
                identifier = values[0],
                networkingIdentifier = values[1],
                classname = values[2],
                methodname = values[3],
                date = new DateTime(long.Parse(values[4])),
                severity = (ELogLevel)(int.Parse(values[5])),
                message = values[6]
            };

            return new CommandArgumentLogEntry(value);
        }
    }
}
