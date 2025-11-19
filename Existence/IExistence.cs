namespace Cartheur.Ideal.Existence
{
    /// <summary>
    /// IExistence is an interface that yields the capability of a "stream of intelligence" to be run step-by-step.
    /// </summary>
    public interface IExistence
    {
        /// <summary>
        /// Perform one step of a "stream of intelligence".
        /// </summary>
        /// <returns>A string representing the "event of intelligence" that was performed.</returns>
        /// <remarks>Each "event of intelligence" can be have tracing enabled.</remarks>
        public string Step();
    }
}
