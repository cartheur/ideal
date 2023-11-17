namespace Cartheur.Ideal.Coupling.Interaction
{
    /// <summary>
    /// An interaction010 is the association of an experience with a result.
    /// </summary>
    /// <seealso cref="Cartheur.Ideal.Coupling.Interaction.Interaction" />
    public class Interaction010 : Interaction
    {
        protected string _label;
        protected Experiment _experience;
        protected Result _result;

        public Interaction010(string label)
        {
            _label = label;
        }

        public string GetLabel()
        {
            return _label;
        }

        public Experiment GetExperience()
        {
            return _experience;
        }

        public void SetExperience(Experiment experience)
        {
            _experience = experience;
        }

        public Result GetResult()
        {
            return _result;
        }

        public void SetResult(Result result)
        {
            _result = result;
        }

        public override string ToString()
        {
            return _experience.GetLabel() + _result.GetLabel();
        }
    }
}
