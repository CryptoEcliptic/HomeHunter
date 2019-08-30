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

        public async Task<bool> AddSessionInTheDb(string ipAddress, string visitorId)
        {
            if (string.IsNullOrEmpty(ipAddress) || string.IsNullOrEmpty(visitorId))
            {
                return false;
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

                return true;
            }

            return false;
        }

        public async Task<long> UniqueVisitorsCount()
        {
            var visitorsCount = await Task.Run(() => this.context.VisitorsSessions.Count());

            return visitorsCount;
        }
    }
}
