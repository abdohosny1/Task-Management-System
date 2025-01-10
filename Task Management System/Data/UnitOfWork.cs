


namespace MyTask_Management_System
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDBContext _context;
        public IBaseRepository<TaskModel> TaskModels { get; private set; }



        public UnitOfWork(ApplicationDBContext context)
        {
            _context = context;

            TaskModels = new BaseRepository<TaskModel>(_context);


        }
        public int Complete()
        {
            return _context.SaveChanges();

        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
