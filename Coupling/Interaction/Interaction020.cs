namespace Ideal.Coupling.Interaction
{
    /// <summary>
    /// An Interaction020 is an Interaction010 with a valence.
    /// </summary>
    public class Interaction020 : Interaction010
    {
        private int _valence;

        public Interaction020(string label) : base(label)
        {
            _label = label;
        }

        public void SetValence(int valence)
        {
            _valence = valence;
        }

        public int GetValence()
        {
            return _valence;
        }

        public override string ToString()
        {
            return GetLabel() + "," + GetValence();
        }
    }
}
