namespace Ideal.Coupling
{
    public class Experiment050 : Experiment040
    {
        private List<Interaction> enactedInteractions = new List<Interaction>();

        public Experiment050(string label) : base(label) {  }

        public void AddEnactedInteraction(Interaction enactedInteraction)
        {
            if (!this.enactedInteractions.Contains(enactedInteraction))
                this.enactedInteractions.Add(enactedInteraction);
        }

        public List<Interaction> GetEnactedInteractions()
        {
            return this.enactedInteractions;
        }
    }
}
