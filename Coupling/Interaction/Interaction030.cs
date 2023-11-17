using Ideal.Agent;

namespace Ideal.Coupling.Interaction
{
    /// <summary>
    /// An Interaction030 is an Interaction020 that can be primitive or composite. A composite interaction has a preInteraction and a postInteraction
    /// </summary>
    public class Interaction030 : Interaction020
    {
        private Interaction030 _preInteraction;
        private Interaction030 _postInteraction;

        public Interaction030(string label) : base(label)
        {
            _label = label;
        }

        public Interaction030 GetPreInteraction()
        {
            return _preInteraction;
        }

        public void SetPreInteraction(Interaction030 preInteraction)
        {
            _preInteraction = preInteraction;
        }

        public Interaction030 GetPostInteraction()
        {
            return (Interaction030)_postInteraction;
        }

        public void SetPostInteraction(Interaction030 postInteraction)
        {
            _postInteraction = postInteraction;
        }

        public bool IsPrimitive()
        {
            return GetPreInteraction() == null;
        }

        public static implicit operator Interaction030(Anticipation030 v)
        {
            throw new NotImplementedException();
        }
    }
}
