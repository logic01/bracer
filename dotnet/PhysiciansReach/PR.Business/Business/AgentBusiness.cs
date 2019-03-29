using Microsoft.EntityFrameworkCore;
using PR.Models;
using PR.Business.Interfaces;
using PR.Business.Mappings;
using PR.Data.Models;
using System.Collections.Generic;
using System.Linq;

namespace PR.Business
{
    public class AgentBusiness : IAgentBusiness
    {
        private DataContext _context;

        public AgentBusiness(DataContext context)
        {
            _context = context;
        }

        public List<AgentModel> Get()
        {
            return _context.Agent
                    .Include(p => p.UserAccount)
                    .Select(i => i.ToModel())
                    .ToList();
        }

        public List<AgentModel> Get(int[] userAccountIds)
        {
            IQueryable<Agent> agents = _context.Agent.Where(a => userAccountIds.Contains(a.UserAccountId));

            return agents.Select(i => i.ToModel()).ToList();
        }

        public AgentModel Get(int userAccountId)
        {
            var agent = _context.Agent
                .Include(a => a.UserAccount)
                .FirstOrDefault(u => u.UserAccountId == userAccountId);

            return agent.ToModel();
        }

        public AgentModel Create(AgentModel agentModel)
        {
            var agent = agentModel.ToEntity();

            _context.Agent.Add(agent);
            _context.SaveChanges();

            return agent.ToModel();
        }

        public AgentModel Update(AgentModel agentModel)
        {
            var agent = _context.Agent
                .Include(a => a.UserAccount)
                .FirstOrDefault(u => u.UserAccountId == agentModel.UserAccount.UserAccountId);

            agent = agentModel.ToEntity();
            _context.Agent.Add(agent);
            _context.SaveChanges();

            return agent.ToModel();
        }


    }
}
