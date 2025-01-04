using System.Text;

using FLG.Cs.Datamodel.Logger;
using FLG.Cs.Datamodel.ServiceLocator;


namespace FLG.Cs.Datamodel.Commands {
    public class Command<T> : ICommand where T : IServiceInstance {
        private readonly CommandData _data;

        public Command(string medthodname)
        {
            _data = new()
            {
                type = typeof(T),
                methodName = medthodname,
                args = []
            };
        }

        public CommandData GetCommandData() => _data;

        public string ToPacketString()
        {
            StringBuilder sb = new();

            sb.Append(_data.type);
            sb.Append(CommandConstants.REFLECTION_CLASSMETHOD_SEPARATOR);
            sb.Append(_data.methodName);
            sb.Append(CommandConstants.REFLECTION_CLASSMETHOD_SEPARATOR);
            sb.Append(string.Join(CommandConstants.REFLECTION_PARAM_SEPARATOR, _data.args.Select(x => x.Type.ToTypeAndSeparatorString() + x.ToPrettyString())));

            return sb.ToString();
        }

        public new Type GetType() => _data.type;
        public string GetMethodName() => _data.methodName;
        public List<ICommandArgument> GetArgs() => _data.args;

        private void AddParam(ICommandArgument arg) { _data.args.Add(arg); }

        // Primitives
        public void AddParam(bool value) { AddParam(ICommandArgumentExtension.GetCommandArgument(value)); }
        public void AddParam(int value) { AddParam(ICommandArgumentExtension.GetCommandArgument(value)); }
        public void AddParam(float value) { AddParam(ICommandArgumentExtension.GetCommandArgument(value)); }
        public void AddParam(string value) { AddParam(ICommandArgumentExtension.GetCommandArgument(value)); }

        // Structs
        public void AddParam(LogEntry value) { AddParam(ICommandArgumentExtension.GetCommandArgument(value)); }
    }
}
