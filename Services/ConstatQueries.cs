using System.Collections.Generic;
using System.Linq;

using Microsoft.EntityFrameworkCore;

using OnlineLearning.Constants;
using OnlineLearning.DTOs;
using OnlineLearning.Models;

namespace OnlineLearning.Services
{
    public static class ConstatQueries
    {
        #region GENERAL
        public static IQueryable<T> IsNotDeleted<T>(this IQueryable<T> query) where T : BaseEntity
        {
            return query.Where(x => !x.IsDeleted);
        }
        public static IQueryable<ApplicationUser> IsNotDeleted(this IQueryable<ApplicationUser> query) 
        {
            return query.Where(x => !x.IsDeleted);
        }
        public static IQueryable<T> OrderByCreatedAt<T>(this IQueryable<T> query, bool desc = false) where T : BaseEntity
        {
            return !desc ? query.OrderBy(x => x.CreatedAt) : query.OrderByDescending(x => x.CreatedAt);
        }
        public static IQueryable<ApplicationUser> OrderByCreatedAt(this IQueryable<ApplicationUser> query, bool desc = false) 
        {
            return !desc ? query.OrderBy(x => x.CreatedAt) : query.OrderByDescending(x => x.CreatedAt);
        }
        #endregion
        #region User
        public static IQueryable<T> GetCreatedRoomsForUser<T>(this IQueryable<T> query) where T : ApplicationUser
        {
            return query.Include(x=>x.CreatedRooms.Where(x=>!x.IsDeleted));
        }
        public static IQueryable<T> GetAllRoomsForUser<T>(this IQueryable<T> query) where T : ApplicationUser
        {
            return query.Include(x=>x.RequestedRooms.Where(x=>!x.IsDeleted)).ThenInclude(x=>x.Room);
        }
        public static IQueryable<T> GetRequestedRoomsForUser<T>(this IQueryable<T> query) where T : ApplicationUser
        {
            return query.Include(x=>x.RequestedRooms.Where(x=>!x.IsDeleted && x.StatusId == ConstantUserRoomStatus.PENDING)).ThenInclude(x=>x.Room);
        }
        public static IQueryable<T> GetJoinedRoomsForUser<T>(this IQueryable<T> query) where T : ApplicationUser
        {
            return query.Include(x=>x.RequestedRooms.Where(x=>!x.IsDeleted && x.StatusId == ConstantUserRoomStatus.JOINED)).ThenInclude(x=>x.Room);
        }
        public static IQueryable<T> GetUserStatus<T>(this IQueryable<T> query) where T : ApplicationUser
        {
            return query.Include(x=>x.Status);
        }
        public static IQueryable<T> GetUserInterests<T>(this IQueryable<T> query) where T : ApplicationUser
        {
            return query.Include(x=>x.UserInterests.Where(x=>!x.IsDeleted));
        }
        
        #endregion
        

    }
}