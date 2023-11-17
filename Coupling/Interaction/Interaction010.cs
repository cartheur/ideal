namespace Cartheur.Ideal.Coupling.Interaction
{
    /// <summary>
    /// An interaction010 is the association of an experience with a result.
    /// </summary>
    /// <seealso cref="Cartheur.Ideal.Coupling.Interaction.Interaction" />
    public class Interaction010 : Interaction
    {
        private readonly string _label;
        protected Experiment experience;
        protected Result result;

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
            return experience;
        }

        public void SetExperience(Experiment experience)
        {
            this.experience = experience;
        }

        public Result GetResult()
        {
            return result;
        }

        public void SetResult(Result result)
        {
            this.result = result;
        }

        public string ToString()
        {
            return experience.GetLabel() + result.GetLabel();
        }
    }
}
