using PR.Models;
using System.Collections.Generic;

namespace PR.Business.Interfaces
{
    public interface IAgentBusiness
    {
        AgentModel Get(int id);

        List<AgentModel> Get(int[] ids);

        List<AgentModel> GetAll();

        AgentModel Create(AgentModel agentModel);

        AgentModel Update(AgentModel agentModel);
    }
}
