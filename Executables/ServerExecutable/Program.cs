using FLG.Cs.Datamodel.Framework;
using FLG.Cs.Datamodel.Networking;
using FLG.Cs.Framework;


Console.Title = "Server";

PreferencesFramework prefs = new()
{
    networking = new() { clientType = ENetworkingClientType.SERVER }
};

FrameworkManager.Instance.Initialize(prefs);
