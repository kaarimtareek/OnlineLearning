using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;

using OnlineLearning.Constants;
using OnlineLearning.Models;

using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace OnlineLearning.Workers
{
    public class RoomWorker : IHostedService, IDisposable
    {
        private readonly DbContextOptions<AppDbContext> dbContextOptions;

        public RoomWorker(DbContextOptions<AppDbContext> dbContextOptions)
        {
            this.dbContextOptions=dbContextOptions;
        }
        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
        //ignore for now , need a better solution
        public void DoWork(object? status)
        {
            using (AppDbContext context = new AppDbContext(dbContextOptions))
            {
                //var dateTime = DateTime.Now;
                //var rooms = context.Rooms.Where(x => x.StatusId == ConstantRoomStatus.PENDING && x.StartDate);

            }
        }
    }
}
