namespace FLG.Cs.IDatamodel {
    public interface ISerializerManager : ISerializer, IServiceInstance {
        public IEnumerable<ISaveFile> GetSaveFiles();
        public void AddSerializable(ISerializable serializable);
    }
}
