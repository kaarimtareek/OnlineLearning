using Microsoft.EntityFrameworkCore;

using OnlineLearning.Models;

using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace OnlineLearning.EntitiesValidators
{
    public class MeetingValidator
    {
        public async Task<bool> IsMeetingExist(AppDbContext context, int meetingId,CancellationToken cancellationToken = default)
        {
            return await context.RoomMeetings.AnyAsync(x=>x.Id == meetingId, cancellationToken);
        }
    
    }
}
