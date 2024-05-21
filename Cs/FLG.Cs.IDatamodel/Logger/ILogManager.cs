namespace FLG.Cs.IDatamodel {
    public interface ILogManager : IServiceInstance {
        public void Error(string msg);
        public void Warn(string msg);
        public void Info(string msg);
        public void Debug(string msg);

        public void Log(IResult result);
    }
}
