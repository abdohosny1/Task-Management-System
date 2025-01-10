


namespace MyTask_Management_System.Core
{
    public interface IUnitOfWork : IDisposable
    {
        IBaseRepository<TaskModel> TaskModels { get; }

        int Complete();
    }
}
