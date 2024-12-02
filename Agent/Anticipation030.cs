using Ideal.Coupling.Interaction;

namespace Ideal.Agent
{
    /// <summary>
    /// An Anticipation030 is created for each proposed primitive interaction. An Anticipation030 is greater than another if its interaction has a greater valence than the other's.
    /// </summary>
    /// <seealso cref="Ideal.Agent.Anticipation" />
    public class Anticipation030 : IAnticipation
    {
        Interaction030 _interaction;

        public Anticipation030(Interaction030 interaction)
        {
            _interaction = interaction;
        }

        public Interaction030 GetInteraction()
        {
            return _interaction;
        }

        public virtual int CompareTo(IAnticipation anticipation)
        {
            return ((int)((Anticipation030)anticipation).GetInteraction().GetValence()).CompareTo(_interaction.GetValence());
        }
    }
}
