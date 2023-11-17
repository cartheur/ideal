using Ideal.Coupling.Interaction;

namespace Ideal.Agent
{
    /// <summary>
    /// The anticipation for the presence of its participant in an environment.
    /// </summary>
    /// <seealso cref="IComparable&lt;IAnticipation&gt;" />
    public interface IAnticipation : IComparable<IAnticipation>
    {
        void AddProclivity(int proclivity);
        Anticipation030 GetInteraction();
    }
}
