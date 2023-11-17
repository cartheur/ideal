namespace Cartheur.Ideal.Coupling
{
    /// <summary>
    /// An experiment that can be chosen by the agent.
    /// </summary>
    public class Experiment
    {
        private readonly string _label;
        /// <summary>
        /// Initializes a new instance of the <see cref="Experiment"/> class.
        /// </summary>
        /// <param name="label">The label.</param>
        public Experiment(string label)
        {
            _label = label;
        }
        /// <summary>
        /// Gets the label.
        /// </summary>
        /// <returns>The experience's label.</returns>
        public string GetLabel()
        {
            return _label;
        }
    }
}
