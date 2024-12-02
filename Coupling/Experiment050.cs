namespace Ideal.Coupling
{
    public class Experiment050 : Experiment040
    {
        private List<Interaction.Interaction> _enactedInteractions = new List<Interaction.Interaction>();

        public Experiment050(string label) : base(label) {  }

        public void AddEnactedInteraction(Interaction.Interaction enactedInteraction)
        {
            if (!_enactedInteractions.Contains(enactedInteraction))
                _enactedInteractions.Add(enactedInteraction);
        }

        public List<Interaction.Interaction> GetEnactedInteractions()
        {
            return _enactedInteractions;
        }
    }
}
