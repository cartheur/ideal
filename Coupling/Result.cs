namespace Ideal.Coupling
{
    /// <summary>
    /// A result of an experience.
    /// </summary>
    public class Result
    {
        private readonly string _label;
        /// <summary>
        /// Initializes a new instance of the <see cref="Result"/> class.
        /// </summary>
        /// <param name="label">The result's label.</param>
        public Result(string label)
        {
            _label = label;
        }
        /// <summary>
        /// Gets the label.
        /// </summary>
        /// <returns>The result's label.</returns>
        public string GetLabel()
        {
            return _label;
        }
    }
}
