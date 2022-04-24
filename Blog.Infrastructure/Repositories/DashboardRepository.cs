using Blog.Domain.Entities;
using Blog.Domain.Interfaces.Repositories;
using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System;
using System.Threading.Tasks;

namespace Blog.Infrastructure.Repositories
{
    public class DashboardRepository : IDashboardRepository
    {
        private readonly IConfiguration _configuration;
        private readonly string _connectionString;

        public DashboardRepository(IConfiguration configuration)
        {
            _configuration = configuration;
            _connectionString = _configuration.GetConnectionString("Blog");
        }

        public async Task<Dashboard> GetDashboardData()
        {
            var dashboard = new Dashboard();

            using (var conn = new SqlConnection(_connectionString))
            {
                dashboard.UsersMostCommentsInMonth = await conn.QueryAsync<Chart>(@"
                        SELECT TOP 3 usu.Name AS Name, COUNT(*) AS Value FROM [User] usu
                        INNER JOIN [Comment] cmt ON (usu.UserId = cmt.UserId)
                        WHERE MONTH(cmt.CreatedAt) = @CurrentMonth
                        GROUP BY usu.Name
                        order by COUNT(*) desc
                    ",
                    new
                    {
                        CurrentMonth = DateTime.Now.Month.ToString()
                    });

                dashboard.PostsMostCommentsInMonth = await conn.QueryAsync<Chart>(@"
                        SELECT TOP 3 pos.Title AS Name, COUNT(*) AS Value FROM [Post] pos
                        INNER JOIN [Comment] cmt ON (pos.PostId = cmt.PostId)
                        WHERE MONTH(cmt.CreatedAt) = @CurrentMonth
                        GROUP BY pos.Title
                        order by COUNT(*) desc
                    ",
                    new
                    {
                        CurrentMonth = DateTime.Now.Month.ToString()
                    });

                dashboard.PostsMostViewsInMonth = await conn.QueryAsync<Chart>(@"
                        SELECT TOP 3 pos.Title AS Name, COUNT(*) AS Value FROM [Post] pos
                        INNER JOIN [Visualization] vis ON (pos.PostId = vis.PostId)
                        WHERE MONTH(vis.CreatedAt) = @CurrentMonth
                        GROUP BY pos.Title
                        order by COUNT(*) desc
                    ",
                    new
                    {
                        CurrentMonth = DateTime.Now.Month.ToString()
                    });

                dashboard.PostsMostSharesInMonth = await conn.QueryAsync<Chart>(@"
                        SELECT TOP 3 pos.Title AS Name, COUNT(*) AS Value FROM [Post] pos
                        INNER JOIN [Sharing] sha ON (pos.PostId = sha.PostId)
                        WHERE MONTH(sha.CreatedAt) = @CurrentMonth
                        GROUP BY pos.Title
                        order by COUNT(*) desc
                    ",
                    new
                    {
                        CurrentMonth = DateTime.Now.Month.ToString()
                    });

                dashboard.PostsMostLikesInMonth = await conn.QueryAsync<Chart>(@"
                        SELECT TOP 3 pos.Title AS Name, COUNT(*) AS Value FROM [Post] pos
                        INNER JOIN [Like] lik ON (pos.PostId = lik.PostId)
                        WHERE MONTH(lik.CreatedAt) = @CurrentMonth
                        GROUP BY pos.Title
                        order by COUNT(*) desc
                    ",
                    new
                    {
                        CurrentMonth = DateTime.Now.Month.ToString()
                    });
            }

            return dashboard;
        }
    }
}
