using Ideal.Coupling;

namespace Ideal.Agent
{
    /// <summary>
    /// An Anticipation030 is created for each proposed primitive interaction. An Anticipation030 is greater than another if its interaction has a greater valence than the other's.
    /// </summary>
    /// <seealso cref="Ideal.Agent.Anticipation" />
    public class Anticipation031 : IAnticipation, IComparable<IAnticipation>
    {
        int _proclivity;
        static Experiment Experience { get; set; }

        public Anticipation031(Experiment experience, int proclivity)
        {
            Experience = experience;
            _proclivity = proclivity;
        }
        public int CompareTo(IAnticipation anticipation)
        {
            return Convert.ToInt32(((Anticipation031)anticipation).GetProclivity().CompareTo(_proclivity));
        }

        public Experiment GetExperience()
        {
            return Experience;
        }

        public override int GetHashCode()
        {
            return Experience.GetHashCode() ^ _proclivity.GetHashCode();
        }

        public override bool Equals(object otherProposition)
        {
            return ((Anticipation031)otherProposition).GetExperience() == Experience;
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
