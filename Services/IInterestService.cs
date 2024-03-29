﻿using OnlineLearning.Common;
using OnlineLearning.DTOs;
using OnlineLearning.Models;

using System.Collections.Generic;
using System.Threading.Tasks;

namespace OnlineLearning.Services
{
    public interface IInterestService
    {
        Task<OperationResult<string>> AddInterest(AppDbContext context,string newInterest, bool ignoreSimilarity = false);
        Task<OperationResult<int>> AddUserInterest(AppDbContext context,string userId, string interestId);
        Task<OperationResult<List<InterestDto>>> GetInterests();
        Task<OperationResult<List<Interest>>> GetSimilarInterests(string normalizedInterest);
        Task<OperationResult<List<Interest>>> GetUserIntersts(string userId);
    }
}