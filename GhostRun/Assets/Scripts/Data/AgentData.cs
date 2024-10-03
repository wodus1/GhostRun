
namespace Core
{
    public class AgentData
    {
        public AgentTemplate template { get; private set; }
        public int id { get; private set; }
        public float normalSpeed { get; private set; }
        public float normalTimeLimit { get; private set; }
        public Enums.AgentType agentType { get; private set; }
        
        public AgentData(AgentTemplate template)
        {
            this.template = template;
            this.id = template.id;
            this.normalSpeed = template.speed;
            this.normalTimeLimit = template.timeLimit;
            this.agentType = template.agentType;
        }
    }
}