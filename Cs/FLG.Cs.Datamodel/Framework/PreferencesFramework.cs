using FLG.Cs.Datamodel.Logger;
using FLG.Cs.Datamodel.Networking;
using FLG.Cs.Datamodel.Serialization;
using FLG.Cs.Datamodel.UI;


namespace FLG.Cs.Datamodel.Framework {
    public struct PreferencesFramework {
        public string identifier;
        public PreferencesLogs? logs;
        public PreferencesUI? ui;
        public PreferencesNetworking? networking;
        public PreferencesSerialization? serialization;
    }
}
