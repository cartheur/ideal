namespace Ideal.Coupling.Interaction
{
    public class Interaction031 : Interaction030
    {
        private int _weight = 0;

        public Interaction031(string label) : base(label) { }

        public new Interaction031 GetPreInteraction()
        {
            return (Interaction031)base.GetPreInteraction();
        }

        public new Interaction031 GetPostInteraction()
        {
            return (Interaction031)base.GetPostInteraction();
        }

        public int GetWeight()
        {
            return _weight;
        }

        public void IncrementWeight()
        {
            _weight++;
        }

        public override string ToString()
        {
            return $"{this.GetLabel()} valence {this.GetValence()} weight {this._weight}";
        }
    }
}
