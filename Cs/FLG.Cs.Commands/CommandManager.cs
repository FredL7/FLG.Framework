using FLG.Cs.Datamodel;
using FLG.Cs.ServiceLocator;
using System.Reflection;


namespace FLG.Cs.Commands {
    public class CommandManager : ICommandManager {
        #region IServiceInstance
        public void OnServiceRegisteredFail() { }
        public void OnServiceRegistered()
        {
            Locator.Instance.Get<ILogManager>().Debug("Command Manager Registered");
        }
        #endregion IServiceInstance

        public void ExecuteCommand(ICommand command)
        {
            ExecuteCommand(command.GetCommandData());
        }

        public void ExecuteCommand(string commandMessage)
        {
            Locator.Instance.Get<ILogManager>().Debug($"Attempting to parse command message {commandMessage}");
            try
            {
                var logger = Locator.Instance.Get<ILogManager>();
                string[] messagedata = commandMessage.Split(CommandConstants.REFLECTION_CLASSMETHOD_SEPARATOR);

                // Type
                var type = messagedata[0];
                int lastDot = type.LastIndexOf('.');
                var assemblyString = type[..lastDot];
                var typeString = type[(lastDot + 1)..];
                var fullType = Type.GetType($"{type}, {assemblyString}");
                if (fullType == null)
                {
                    throw new ArgumentException($"Type {fullType} (from {type}) not found");
                }

                // Method
                var medhotName = messagedata[1];

                // Args
                string[] argsData = messagedata[2].Split(CommandConstants.REFLECTION_PARAM_SEPARATOR);
                var pTypes = new ECommandArgumentType[argsData.Length];
                var pValues = new object[argsData.Length];
                for (int i = 0; i < argsData.Length; ++i)
                {
                    string[] arg = argsData[i].Split(CommandConstants.REFLECTION_TYPE_SEPARATOR);
                    ECommandArgumentType pType = arg[0].FromTypeString();
                    string pValueRaw = arg[1];
                    object pValue = pType switch
                    {
                        ECommandArgumentType.BOOL => Boolean.Parse(pValueRaw),
                        ECommandArgumentType.INT => Int32.Parse(pValueRaw),
                        ECommandArgumentType.FLOAT => float.Parse(pValueRaw),
                        ECommandArgumentType.STRING => pValueRaw,
                        _ => throw new ArgumentException($"Unknown type {pType}"),
                    };

                    pTypes[i] = pType;
                    pValues[i] = pValue;
                }
                List<CommandArgument> args = new(pValues.Length);
                for (int i = 0; i < pValues.Length; ++i)
                {
                    args.Add(new() { type = pTypes[i], value = pValues[i] });
                }

                // Ctor
                CommandData data = new()
                {
                    type = fullType,
                    methodName = medhotName,
                    args = args,
                };

                ExecuteCommand(data);
            }
            catch (TypeLoadException e)
            {
                Locator.Instance.Get<ILogManager>().Error($"Error parsing command message (invalid type): {e.Message}");
            }
            catch (Exception e)
            {
                Locator.Instance.Get<ILogManager>().Error($"Error parsing command message: {e.Message}");
            }
        }

        private void ExecuteCommand(CommandData commandData)
        {
            var logger = Locator.Instance.Get<ILogManager>();
            string args = string.Join(", ", commandData.args.Select(x => $"{x.type}: {x.value}"));
            logger.Debug($"Executing {commandData.methodName} from type {commandData.type} with {commandData.args.Count} arguments ({args}).");

            try
            {
                var locatorGetMethod = typeof(Locator).GetMethod("Get") ?? throw new Exception($"Could not find Locator.Instance.Get()");
                var getMethodGeneric = locatorGetMethod?.MakeGenericMethod(commandData.type) ?? throw new Exception($"Could not make Locator.Instance.Get<{commandData.type}>() Generic");
                var result = getMethodGeneric?.Invoke(Locator.Instance, null) ?? throw new Exception($"Could not invoke Locator.Instance.Get<{commandData.type}>()");

                var serviceInstance = result as IServiceInstance ?? throw new Exception($"Could not convert {commandData.type} to IServiceInstance");

                try
                {
                    var commandMethod = commandData.type.GetMethod(commandData.methodName) ?? throw new Exception($"Could not find method {commandData.methodName} in {commandData.type} with {commandData.args.Count} arguments ({args})");
                    commandMethod.Invoke(serviceInstance, commandData.args.Select(x => x.value).ToArray());
                }
                catch (Exception e)
                {
                    Locator.Instance.Get<ILogManager>().Error($"Error while executing command (invoking method): {e.Message}");
                }
            }
            catch (Exception e)
            {
                Locator.Instance.Get<ILogManager>().Error($"Error while executing command (invoking Locator): {e.Message}");
            }
        }
    }
}
