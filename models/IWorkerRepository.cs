public interface IWorkerRepository
{
    IReadOnlyList<Worker> GetAllWorkers();
    void AddWorker(Worker worker);
    void UpdateWorker(Worker worker);
    bool DeleteWorker(string workerId);
}
public class WorkerRepository : IWorkerRepository
{
    private readonly DataStore dataStore = DataStore.Instance;
    public WorkerRepository()
    {
    }
    public IReadOnlyList<Worker> GetAllWorkers()
    {
        return dataStore.workers;
    }

    public void AddWorker(Worker worker)
    {

        ITokenService realService = new TokenService();
        ITokenService proxy = new TokenServiceProxy(realService);
        worker.Id = proxy.CreateUniqueiId();


        string token1 = proxy.CreateToken(worker.Id);
        worker.Token = token1;
        dataStore.workers.Add(worker);
    }
    public void UpdateWorker(Worker worker)
    {
        var existingWorker = dataStore.workers.FirstOrDefault(w => w.Id == worker.Id);
        if (existingWorker != null)
        {
            existingWorker.Name = worker.Name;
            existingWorker.Contact = worker.Contact;
            existingWorker.Salary = worker.Salary;
            existingWorker.JobTitle = worker.JobTitle;
            existingWorker.email = worker.email;
            existingWorker.Password = worker.Password;
        }
    }

    public bool DeleteWorker(string workerId)
    {
        var worker = dataStore.workers.FirstOrDefault(w => w.Id == workerId);
        if (worker != null)
        {
            dataStore.workers.Remove(worker);
            return true;
        }
        return false;
    }

    
}
