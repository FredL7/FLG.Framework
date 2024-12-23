namespace FLG.Cs.Datamodel.Commands {
    public static partial class ICommandArgumentExtension {
        internal static ICommandArgument GetCommandArgument(bool value) => new CommandArgumentBool(value);
        internal static ICommandArgument GetCommandArgument(int value) => new CommandArgumentInt(value);
        internal static ICommandArgument GetCommandArgument(float value) => new CommandArgumentFloat(value);
        internal static ICommandArgument GetCommandArgument(string value) => new CommandArgumentString(value);
    }

    internal readonly struct CommandArgumentBool(bool value) : ICommandArgument {
        private readonly bool _value = value;
        public readonly object Value { get => _value; }
        public readonly ECommandArgumentType Type { get => CommandArgumentTypeExtension.FromType(_value); }

        public readonly string ToPrettyString() => _value.ToString();
    }

    internal readonly struct CommandArgumentInt(int value) : ICommandArgument {
        private readonly int _value = value;
        public readonly object Value { get => _value; }
        public readonly ECommandArgumentType Type { get => CommandArgumentTypeExtension.FromType(_value); }

        public readonly string ToPrettyString() => _value.ToString();
    }

    internal readonly struct CommandArgumentFloat(float value) : ICommandArgument {
        private readonly float _value = value;
        public object Value { get => _value; }
        public readonly ECommandArgumentType Type { get => CommandArgumentTypeExtension.FromType(_value); }

        public readonly string ToPrettyString() => _value.ToString();
    }

    internal readonly struct CommandArgumentString(string value) : ICommandArgument {
        private readonly string _value = value;
        public object Value { get => _value; }
        public readonly ECommandArgumentType Type { get => CommandArgumentTypeExtension.FromType(_value); }

        public readonly string ToPrettyString() => _value;
    }
}
