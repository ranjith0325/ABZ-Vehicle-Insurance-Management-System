﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ABZCustomerQueryLibrary.Models;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace ABZCustomerQueryLibrary.Repo
{
    public interface IQueryResponseRepoAsync
    {
        Task InsertQueryResponseAsync(QueryResponse queryresponse);
        Task DeleteQueryResponseAsync(string queryID, string srNo);
        Task UpdateQueryResponseAsync(string queryID, string srNo, QueryResponse queryresponse);
        Task<List<QueryResponse>> GetAllQueryResponseAsync();
        Task<QueryResponse> GetQueryResponseAsync(string queryID, string srNo);
        Task<List<QueryResponse>> GetQueryResponseByCustomerQuery(string queryID);
        Task<List<QueryResponse>> GetQueryResponseByAgentAsync(string agentID);
        Task InsertAgentAsync(Agent agent);
    }
}
