namespace lafe.ServiceBase.Interface
{
    public interface IService
    {
        void OnContinue();
        void OnPause();

        void OnStart(string[] args);

        void OnStop();
    }
}
