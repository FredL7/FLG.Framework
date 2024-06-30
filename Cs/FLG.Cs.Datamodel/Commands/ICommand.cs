namespace FLG.Cs.Datamodel {
    public interface ICommand {
        public string ToMessageString();
        public CommandData GetCommandData();
    }

    public static class CommandConstants {
        public const string REFLECTION_CLASSMETHOD_SEPARATOR = "&&&";
        public const string REFLECTION_PARAM_SEPARATOR = "///";
        public const string REFLECTION_TYPE_SEPARATOR = ":::";

        public const string REFLECTION_TYPE_BOOL = "b";
        public const string REFLECTION_TYPEANDSEPARATOR_BOOL = REFLECTION_TYPE_BOOL + REFLECTION_TYPE_SEPARATOR;
        public const string REFLECTION_TYPE_INT = "i";
        public const string REFLECTION_TYPEANDSEPARATOR_INT = REFLECTION_TYPE_INT + REFLECTION_TYPE_SEPARATOR;
        public const string REFLECTION_TYPE_FLOAT = "f";
        public const string REFLECTION_TYPEANDSEPARATOR_FLOAT = REFLECTION_TYPE_FLOAT + REFLECTION_TYPE_SEPARATOR;
        public const string REFLECTION_TYPE_STRING = "s";
        public const string REFLECTION_TYPEANDSEPARATOR_STRING = REFLECTION_TYPE_STRING + REFLECTION_TYPE_SEPARATOR;
    }

    public enum ECommandArgumentType {
        BOOL, INT, FLOAT, STRING
    }
    public static class CommandArgumentTypeExtension {
        public static string ToTypeAndSeparatorString(this ECommandArgumentType commandArgType)
        {
            return commandArgType switch
            {
                ECommandArgumentType.BOOL => CommandConstants.REFLECTION_TYPEANDSEPARATOR_BOOL,
                ECommandArgumentType.INT => CommandConstants.REFLECTION_TYPEANDSEPARATOR_INT,
                ECommandArgumentType.FLOAT => CommandConstants.REFLECTION_TYPEANDSEPARATOR_FLOAT,
                ECommandArgumentType.STRING => CommandConstants.REFLECTION_TYPEANDSEPARATOR_STRING,
                _ => throw new ArgumentException($"Unknown ECommandArgumentType {commandArgType}"),
            };
        }

        public static ECommandArgumentType FromTypeString(this string typeName)
        {
            return typeName switch
            {
                CommandConstants.REFLECTION_TYPE_BOOL => ECommandArgumentType.BOOL,
                CommandConstants.REFLECTION_TYPE_INT => ECommandArgumentType.INT,
                CommandConstants.REFLECTION_TYPE_FLOAT => ECommandArgumentType.FLOAT,
                CommandConstants.REFLECTION_TYPE_STRING => ECommandArgumentType.STRING,
                _ => throw new ArgumentException($"Unknown typename {typeName}"),
            };
        }
    }

    public struct CommandArgument {
        public ECommandArgumentType type;
        public object value;

        public readonly override string ToString()
        {
            return type.ToTypeAndSeparatorString() + value.ToString();
        }
    }

    public struct CommandData {
        public Type type;
        public string methodName;
        public List<CommandArgument> args;
    }
}
