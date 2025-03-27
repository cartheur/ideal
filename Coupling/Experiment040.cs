using Ideal.Coupling.Interaction;

namespace Ideal.Coupling
{
    /// <summary>
    /// An Experiment040 is an Experiment that can be primitive or abstract. An abstract Experiment has an intendedInteraction which is the sensorimotor pattern to try to enact if this experiment is selected.
    /// </summary>
    /// <seealso cref="Ideal.Coupling.Experiment" />
    public class Experiment040 : Experiment
    {
        // The experience's interaction.
        private Interaction040 _intendedInteraction;
        private bool _isAbstract = true;

        public Experiment040(string label) : base(label) { }

        public bool IsAbstract()
        {
            return _isAbstract;
        }

        public void ResetAbstract()
        {
            _isAbstract = false;
        }

        public void SetIntendedInteraction(Interaction040 intendedInteraction)
        {
            _intendedInteraction = intendedInteraction;
        }

        public Interaction040 GetIntendedInteraction()
        {
            return _intendedInteraction;
        }
    }
}
