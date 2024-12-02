namespace Ideal.Existence
{
    /// <summary>
    /// An Existence is an Object that simulates a "stream of intelligence" when it is run step by step. Each call to the Step() method generates an "event of intelligence" that can be traced.
    /// </summary>
    public interface IExistence
    {
        /// <summary>
        /// Perform one step of a "stream of intelligence".
        /// </summary>
        /// <returns>S string representing the "event of intelligence" that was performed. </returns>
        public string Step();
    }
}
