using Ideal.Coupling.Interaction;

namespace Ideal.Agent
{
    public class Anticipation032 : Anticipation030
    {
        int _proclivity;

        public Anticipation032(Interaction032 interaction, int proclivity) : base(interaction)
        {
            _proclivity = proclivity;
        }

        public override int CompareTo(IAnticipation anticipation)
        {
            return ((Anticipation032)anticipation).GetProclivity().CompareTo(_proclivity);
        }

        public override bool Equals(object otherProposition)
        {
            return ((Anticipation032)otherProposition).GetInteraction() == GetInteraction();
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
            return GetInteraction().GetLabel() + " proclivity " + this.GetProclivity();
        }

        public override int GetHashCode()
        {
            throw new NotImplementedException();
        }
    }
}
