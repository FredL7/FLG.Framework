using FLG.Cs.Datamodel.Logger;

namespace FLG.Cs.Datamodel.Commands {
    public enum ECommandArgumentType {
        // Primitive
        BOOL, INT, FLOAT, STRING,

        // Structs
        LOG_ENTRY,
    }

    public struct CommandData {
        public Type type;
        public string methodName;
        public List<ICommandArgument> args;
    }

    public static class CommandArgumentTypeExtension {
        public static string ToTypeAndSeparatorString(this ECommandArgumentType commandArgType)
        {
            return commandArgType switch
            {
                // Primitives
                ECommandArgumentType.BOOL => CommandConstants.REFLECTION_TYPEANDSEPARATOR_BOOL,
                ECommandArgumentType.INT => CommandConstants.REFLECTION_TYPEANDSEPARATOR_INT,
                ECommandArgumentType.FLOAT => CommandConstants.REFLECTION_TYPEANDSEPARATOR_FLOAT,
                ECommandArgumentType.STRING => CommandConstants.REFLECTION_TYPEANDSEPARATOR_STRING,

                // Structs
                ECommandArgumentType.LOG_ENTRY => CommandConstants.REFLECTION_TYPEANDSEPARATOR_LOGENTRY,

                // Error
                _ => throw new ArgumentException($"Unknown ECommandArgumentType {commandArgType}"),
            };
        }

        public static ECommandArgumentType FromTypeString(this string typeName)
        {
            return typeName switch
            {
                // Primitives
                CommandConstants.REFLECTION_TYPE_BOOL => ECommandArgumentType.BOOL,
                CommandConstants.REFLECTION_TYPE_INT => ECommandArgumentType.INT,
                CommandConstants.REFLECTION_TYPE_FLOAT => ECommandArgumentType.FLOAT,
                CommandConstants.REFLECTION_TYPE_STRING => ECommandArgumentType.STRING,

                // Structs
                CommandConstants.REFLECTION_TYPE_LOGENTRY => ECommandArgumentType.LOG_ENTRY,

                // Error
                _ => throw new ArgumentException($"Unknown typename {typeName}"),
            };
        }

        // Primitives
        public static ECommandArgumentType FromType(bool _) => ECommandArgumentType.BOOL;
        public static ECommandArgumentType FromType(int _) => ECommandArgumentType.INT;
        public static ECommandArgumentType FromType(float _) => ECommandArgumentType.FLOAT;
        public static ECommandArgumentType FromType(string _) => ECommandArgumentType.STRING;

        // Structs
        public static ECommandArgumentType FromType(LogEntry _) => ECommandArgumentType.LOG_ENTRY;
    }
}
