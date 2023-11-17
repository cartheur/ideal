namespace Ideal.Coupling.Interaction
{
    public interface Interaction
    {
        /// <summary>
        /// Gets the label.
        /// </summary>
        /// <returns>The interaction's label.</returns>
        public string GetLabel();
        /// <summary>
        /// Gets the experience.
        /// </summary>
        /// <returns>The interaction's experience.</returns>
        public Experiment GetExperience();
        /// <summary>
        /// Gets the result.
        /// </summary>
        /// <returns>The interaction's result</returns>
        public Result GetResult();
        /// <summary>
        /// Sets the experience.
        /// </summary>
        /// <param name="experience">The interaction's experience.</param>
        public void SetExperience(Experiment experience);
        /// <summary>
        /// Sets the result.
        /// </summary>
        /// <param name="result">The interaction's result.</param>
        public void SetResult(Result result);
    }
}