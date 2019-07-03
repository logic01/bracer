using PR.Models;
using System.Collections.Generic;

namespace PR.Business.Interfaces
{
    public interface IAgentBusiness
    {
        List<AgentModel> Get(int[] ids);

        List<AgentModel> Get();

        AgentModel Get(int id);

        AgentModel Create(AgentModel agentModel);

        AgentModel Update(AgentModel agentModel);
    }
}
