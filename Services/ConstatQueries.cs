using Microsoft.EntityFrameworkCore;

using OnlineLearning.Common;
using OnlineLearning.Constants;
using OnlineLearning.Models;

using System.Linq;
using System.Threading.Tasks;

namespace OnlineLearning.Services
{
    public static class ConstatQueries
    {
        #region GENERAL

        public static IQueryable<T> IsNotDeleted<T>(this IQueryable<T> query) where T : BaseEntity
        {
            return query.Where(x => !x.IsDeleted);
        }

        public static IQueryable<T> Paginate<T>(this IQueryable<T> query, int pageNumber, int pageSize) where T : BaseEntity
        {
            return query.Skip(pageSize * (pageNumber - 1)).Take(pageSize);
        }
        public static async Task<PagedList<T>> ToPagedList<T>(this IQueryable<T> query, int pageNumber, int pageSize)
        {
            return await PagedList<T>.ToPagedList(query, pageNumber, pageSize);
        }

        public static IQueryable<T> OrderByCreatedAt<T>(this IQueryable<T> query, bool desc = false) where T : BaseEntity
        {
            return !desc ? query.OrderBy(x => x.CreatedAt) : query.OrderByDescending(x => x.CreatedAt);
        }

        #endregion GENERAL

        #region User

        public static IQueryable<ApplicationUser> OrderByCreatedAt(this IQueryable<ApplicationUser> query, bool desc = false)
        {
            return !desc ? query.OrderBy(x => x.CreatedAt) : query.OrderByDescending(x => x.CreatedAt);
        }

        public static IQueryable<ApplicationUser> IsNotDeleted(this IQueryable<ApplicationUser> query)
        {
            return query.Where(x => !x.IsDeleted);
        }

        public static IQueryable<T> GetCreatedRoomsForUser<T>(this IQueryable<T> query) where T : ApplicationUser
        {
            return query.Include(x => x.CreatedRooms.Where(x => !x.IsDeleted));
        }

        public static IQueryable<T> GetAllRoomsForUser<T>(this IQueryable<T> query) where T : ApplicationUser
        {
            return query.Include(x => x.RequestedRooms.Where(x => !x.IsDeleted)).ThenInclude(x => x.Room).Include(x=>x.Status);
        }

        public static IQueryable<T> GetRequestedRoomsForUser<T>(this IQueryable<T> query) where T : ApplicationUser
        {
            return query.Include(x => x.RequestedRooms.Where(x => !x.IsDeleted && x.StatusId == ConstantUserRoomStatus.PENDING)).ThenInclude(x => x.Room);
        }

        public static IQueryable<T> GetJoinedRoomsForUser<T>(this IQueryable<T> query) where T : ApplicationUser
        {
            return query.Include(x => x.RequestedRooms.Where(x => !x.IsDeleted && x.StatusId == ConstantUserRoomStatus.JOINED)).ThenInclude(x => x.Room);
        }

        public static IQueryable<T> GetUserStatus<T>(this IQueryable<T> query) where T : ApplicationUser
        {
            return query.Include(x => x.Status);
        }

        public static IQueryable<T> GetUserInterests<T>(this IQueryable<T> query) where T : ApplicationUser
        {
            return query.Include(x => x.UserInterests.Where(x => !x.IsDeleted));
        }

        #endregion User

        #region Room

        public static IQueryable<Room> IncludeOwner(this IQueryable<Room> query)
        {
            return query.Include(x => x.Owner);
        }

        public static IQueryable<Room> IncludeInterests(this IQueryable<Room> query)
        {
            return query.Include(x => x.RoomInterests).ThenInclude(x=>x.Interest);
        }

        public static IQueryable<Room> IncludeRequestedUsers(this IQueryable<Room> query)
        {
            return query.Include(x => x.RequestedUsers.Where(x => !x.IsDeleted)).ThenInclude(x => x.User);
        }

        public static IQueryable<Room> IncludeRequestedUsers(this IQueryable<Room> query, string status)
        {
            return query.Include(x => x.RequestedUsers.Where(x => x.StatusId == status && !x.IsDeleted)).ThenInclude(x => x.User);
        }

        public static IQueryable<Room> IncludeRequestedUsers(this IQueryable<Room> query, string[] statuses)
        {
            return query.Include(x => x.RequestedUsers.Where(x => statuses.Contains(x.StatusId) && !x.IsDeleted)).ThenInclude(x => x.User);
        }
        public static IQueryable<Room> IncludeStatus(this IQueryable<Room> query)
        {
            return query.Include(x => x.Status);
        }
        public static IQueryable<Room> IncludeUserRoomStatus(this IQueryable<Room> query,string userId)
        {
            return query.Include(x => x.RequestedUsers.Where(x=>x.UserId == userId)).ThenInclude(x=>x.Status);
        }

        #endregion Room
    }
}