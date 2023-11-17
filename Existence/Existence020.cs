using Ideal.Coupling;
using Ideal.Coupling.Interaction;

namespace Ideal.Existence
{
    /// <summary>
    ///  * An Existence020 is a sort of Existence010 in which each Interaction has a predefined Valence. When a given Experience is performed and a given Result is obtained, the corresponding Interaction is considered enacted.
    ///  The Existence020 is PLEASED when the enacted Interaction has a positive or null Valence, and PAINED otherwise.
    ///  An Existence020 is still a single entity rather than being split into an explicit Agent and Environment.
    ///  An Existence020 demonstrates a rudimentary decisional mechanism and a rudimentary learning mechanism. It learns to choose the Experience that induces an Interaction that has a positive valence.  Try to change the Valences of interactions and the method GiveResult(experience) and observe that the Existence020 still learns to enact interactions that have positive valences.
    /// </summary>
    public class Existence020 : Existence010
    {
        protected new void InitExistence()
        {
            Experiment e1 = AddOrGetExperience(LABEL_E1);
            Experiment e2 = AddOrGetExperience(LABEL_E2);
            Result r1 = CreateOrGetResult(LABEL_R1);
            Result r2 = CreateOrGetResult(LABEL_R2);
            // Change the valence of interactions to change the agent's motivation.
            AddOrGetPrimitiveInteraction(e1, r1, -1);
            AddOrGetPrimitiveInteraction(e1, r2, 1);
            AddOrGetPrimitiveInteraction(e2, r1, -1);
            AddOrGetPrimitiveInteraction(e2, r2, 1);
            SetPreviousExperience(e1);
        }

        public new string Step()
        {
            Experiment experience = GetPreviousExperience();
            if (GetMood() == Mood.PAINED)
                experience = GetOtherExperience(experience);

            Result result = ReturnResult010(experience);

            Interaction020 enactedInteraction = (Interaction020)AddOrGetPrimitiveInteraction(experience, result);

            if (enactedInteraction.GetValence() >= 0)
                SetMood(Mood.PLEASED);
            else
                SetMood(Mood.PAINED);

            SetPreviousExperience(experience);

            return experience.GetLabel() + result.GetLabel() + " " + GetMood();
        }
        /// <summary>
        /// Create an interaction as a tuple <experience, result>.
        /// </summary>
        /// <param name="experience">The experience.</param>
        /// <param name="result">The result.</param>
        /// <param name="valence">The interaction's valence.</param>
        /// <returns>The created interaction.</returns>
        protected Interaction020 AddOrGetPrimitiveInteraction(Experiment experience, Result result, int valence)
        {
            string label = experience.GetLabel() + result.GetLabel();
            if (!Interactions.ContainsKey(label))
            {
                Interaction020 interactions = CreateInteraction(label);
                interactions.SetExperience(experience);
                interactions.SetResult(result);
                interactions.SetValence(valence);
                Interactions.Add(label, interactions);
            }
            Interaction020 interaction = (Interaction020)Interactions[(label)];
            return interaction;
        }

        protected new Interaction020 CreateInteraction(string label)
        {
            return new Interaction020(label);
        }

        protected new Interaction020 GetInteraction(string label)
        {
            return (Interaction020)Interactions[(label)];
        }
    }
}