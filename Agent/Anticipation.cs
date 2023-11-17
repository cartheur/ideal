using Ideal.Coupling;
using Ideal.Coupling.Interaction;

namespace Ideal.Agent
{
    /// <summary>
    /// An Anticipation is created for each proposed primitive interaction. An Anticipation is greater than another if its interaction has a greater valence than the other's.
    /// </summary>
    /// <seealso cref="Ideal.Agent.IAnticipation" />
    public class Anticipation : IAnticipation
    {
        int _proclivity;
        static Experiment Experience { get; set; }
        Interaction030 Interaction { get; set; }

        public Anticipation(Experiment experience, int proclivity)
        {
            Experience = experience;
            _proclivity = proclivity;
        }
        public Anticipation(Interaction030 interaction, int proclivity)
        {
            Interaction = interaction;
            _proclivity = proclivity;
        }

        public Interaction030 GetInteraction()
        {
            return Interaction;
        }

        public int CompareTo(IAnticipation anticipation)
        {
            return Convert.ToInt32(((Anticipation)anticipation).GetProclivity().CompareTo(_proclivity));
        }
        public int CompareTo(Anticipation anticipation)
        {
            return Convert.ToInt32(((Anticipation)anticipation).GetInteraction().GetValence()).CompareTo(Interaction.GetValence());
        }

        public new bool Equals(object otherProposition)
        {
            return ((Anticipation)otherProposition).GetExperience() == Experience;
        }

        public Experiment GetExperience()
        {
            return Experience;
        }

        public void SetExperience(Experiment experience)
        {
            Experience = experience;
        }

        public int GetProclivity()
        {
            return _proclivity;
        }

        public void AddProclivity(int proclivity)
        {
            _proclivity += proclivity;
        }

        public override string ToString()
        {
            return Experience.GetLabel() + " proclivity " + _proclivity;
        }
    }
}
