using Microsoft.EntityFrameworkCore;
using PR.Business.Interfaces;
using PR.Business.Mappings;
using PR.Data.Models;
using PR.Models;
using System;
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
            Agent agent = _context.Agent
                .Include(a => a.UserAccount)
                .FirstOrDefault(u => u.UserAccountId == userAccountId);

            return agent.ToModel();
        }

        public AgentModel Create(AgentModel agentModel)
        {
            var agent = new Agent();
            agent.MapFromModel(agentModel);

            _context.Agent.Add(agent);
            _context.SaveChanges();

            return agent.ToModel();
        }

        public AgentModel Update(AgentModel agentModel)
        {
            Agent agent = _context.Agent
                .Include(a => a.UserAccount)
                .FirstOrDefault(u => u.UserAccountId == agentModel.UserAccount.UserAccountId);

            agent.MapFromModel(agentModel);

            agent.UserAccount.ModifiedOn = DateTime.Now;
            agent.ModifiedOn = DateTime.Now;

            _context.SaveChanges();

            return agent.ToModel();
        }

    }
}
