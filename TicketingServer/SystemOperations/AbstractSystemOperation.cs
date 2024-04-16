using Repository;
using System;
using System.Threading.Tasks;
namespace TicketingServer.SystemOperations
{
    public abstract class AbstractSystemOperation
    {
        protected Broker broker;

        public AbstractSystemOperation()
        {

        }

        public async Task ExecuteOperation()
        {
            using (broker = new Broker())
            {
                try
                {
                    await Preconditions();
                    await broker.OpenConnectionAsync();
                    broker.BeginTransaction();

                    await ExecuteSpecification();

                    broker.Commit();
                }
                catch (Exception)
                {
                    broker.Rollback();
                    throw;
                }
            }

        }
        protected abstract Task ExecuteSpecification();
        protected abstract Task Preconditions();
    }
}
