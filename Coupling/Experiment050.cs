using Ideal.Coupling.Interaction;

namespace Ideal.Coupling
{
    public class Experiment050 : Experiment040
    {
        private List<Interaction> _enactedInteractions = new List<Interaction>();

        public Experiment050(string label) : base(label) {  }

        public void AddEnactedInteraction(Interaction enactedInteraction)
        {
            if (!_enactedInteractions.Contains(enactedInteraction))
                _enactedInteractions.Add(enactedInteraction);
        }

        public List<Interaction> GetEnactedInteractions()
        {
            return _enactedInteractions;
        }
    }
}
