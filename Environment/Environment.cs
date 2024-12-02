using Ideal.Coupling.Interaction;

namespace Ideal.Environment
{
    public interface IEnvironment
    {
        public Interaction Enact(Interaction intendedInteraction);
    }
}
