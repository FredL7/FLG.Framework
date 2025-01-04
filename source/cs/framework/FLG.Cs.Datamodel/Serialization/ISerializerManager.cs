using FLG.Cs.Datamodel.ServiceLocator;


namespace FLG.Cs.Datamodel.Serialization {
    public interface ISerializerManager : ISerializer, IServiceInstance {
        public IEnumerable<ISaveFile> GetSaveFiles();
        public void AddSerializable(ISerializable serializable);
    }
}
