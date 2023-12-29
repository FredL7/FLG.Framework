using FLG.Cs.ServiceLocator;


namespace FLG.Cs.Serialization {
    public interface ISerializerManager : ISerializer, IServiceInstance {
        public IEnumerable<ISaveFile> GetSaveFiles();
        public void AddSerializable(ISerializable serializable);
    }
}
