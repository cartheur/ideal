namespace Ideal.Coupling.Interaction
{
    /// <summary>
    /// An Interaction032 is an Interaction031 that has a list of alternate interactions.
    /// </summary>
    /// <seealso cref="Ideal.Coupling.Interaction.Interaction031" />
    public class Interaction032 : Interaction031
    {
        private List<Interaction> _alternateInteractions = new List<Interaction>();

        public Interaction032(string label) : base(label) {  }

        public void AddAlternateInteraction(Interaction interaction)
        {
            if (!_alternateInteractions.Contains(interaction))
                _alternateInteractions.Add(interaction);
        }

        public List<Interaction> GetAlternateInteractions()
        {
            return _alternateInteractions;
        }
    }
}
