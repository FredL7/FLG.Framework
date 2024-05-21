namespace FLG.Cs.Datamodel {
    public interface ISerializerManager : ISerializer, IServiceInstance {
        public IEnumerable<ISaveFile> GetSaveFiles();
        public void AddSerializable(ISerializable serializable);
    }
}
