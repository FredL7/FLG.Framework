using System.Text;

using FLG.Cs.Datamodel;


namespace FLG.Cs.Model {
    public class Command<T> : ICommand where T : IServiceInstance {
        private readonly CommandData _data;

        public Command(string medthodname)
        {
            _data = new()
            {
                type = typeof(T),
                methodName = medthodname,
                args = new()
            };
        }

        public CommandData GetCommandData() => _data;

        public string ToMessageString()
        {
            StringBuilder sb = new();

            sb.Append(_data.type);
            sb.Append(CommandConstants.REFLECTION_CLASSMETHOD_SEPARATOR);
            sb.Append(_data.methodName);
            sb.Append(CommandConstants.REFLECTION_CLASSMETHOD_SEPARATOR);
            sb.Append(string.Join(CommandConstants.REFLECTION_PARAM_SEPARATOR, _data.args.Select(x => x.ToString())));

            return sb.ToString();
        }

        public new Type GetType() => _data.type;
        public string GetMethodName() => _data.methodName;
        public List<CommandArgument> GetArgs() => _data.args;

        public void AddParam(bool value) { AddParam(ECommandArgumentType.BOOL, value); }
        public void AddParam(int value) { AddParam(ECommandArgumentType.INT, value); }
        public void AddParam(float value) { AddParam(ECommandArgumentType.FLOAT, value); }
        public void AddParam(string value) { AddParam(ECommandArgumentType.STRING, value); }
        private void AddParam(ECommandArgumentType type, object value) { _data.args.Add(new CommandArgument() { type = type, value = value }); }
    }
}
