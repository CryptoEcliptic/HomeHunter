using HomeHunter.Data;
using HomeHunter.Domain;
using HomeHunter.Services.Contracts;
using System.Linq;
using System.Threading.Tasks;

namespace HomeHunter.Services
{
    public class VisitorSessionServices : IVisitorSessionServices
    {
        private readonly HomeHunterDbContext context;

        public VisitorSessionServices(HomeHunterDbContext context)
        {
            this.context = context;
        }

        public async Task AddSessionInTheDb(string ipAddress, string visitorId)
        {
            if (ipAddress == null || visitorId == null)
            {
                return;
            }

            if (visitorId != null && !this.context.VisitorsSessions.Any(x => x.VisitorId == visitorId))
            {
                var visitorSession = new VisitorSession
                {
                    IpAddress = ipAddress,
                    VisitorId = visitorId,
                };

                this.context.VisitorsSessions.Add(visitorSession);
                await this.context.SaveChangesAsync();
            }  
        }

        public async Task<long> UniqueVisitorsCount()
        {
            var visitorsCount = await Task.Run(() => this.context.VisitorsSessions.Count());

            return visitorsCount;
        }
    }
}
