using Microsoft.EntityFrameworkCore;

using OnlineLearning.Models;

using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace OnlineLearning.EntitiesValidators
{
    public class MeetingValidator : IMeetingValidator
    {
        private readonly DbContextOptions<AppDbContext> dbContextOptions;

        public MeetingValidator(DbContextOptions<AppDbContext> dbContextOptions)
        {
            this.dbContextOptions=dbContextOptions;
        }
        public async Task<bool> IsMeetingExist( int meetingId, CancellationToken cancellationToken = default)
        {
            using(AppDbContext context = new AppDbContext(dbContextOptions))
            return await context.RoomMeetings.AnyAsync(x => x.Id == meetingId, cancellationToken);
        }

    }
}
