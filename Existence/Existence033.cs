using Ideal.Agent;
using Ideal.Coupling.Interaction;
using Ideal.Coupling;
using System.Diagnostics;

namespace Ideal.Existence
{
    /// <summary>
    /// Like Existence030, Existence032 can adapt to Environment010, Environment020, and Environment030. Again, it is PLEASED when the enacted Interaction has a positive or null Valence, and PAINED otherwise. Additionally, like Existence010, Existence032 is SELF-SATISFIED when it correctly anticipated the result, and FRUSTRATED otherwise. It is also BORED when it has been SELF-SATISFIED for too long. Try to change the Valences of interactions and the reality defined in Existence2.initExistence(), and observe that Existence032 tries to balance satisfaction and pleasure. (when the valence of interaction are all set to 0, then only satisfaction/frustration/boredom drives Existence032's choices) Existence032 illustrates the benefit of implementing different motivational dimensions.
    /// </summary>
    /// <seealso cref="Ideal.Existence.Existence032" />
    public class Existence033 : Existence032
    {
        public override string Step()
        {

            List<IAnticipation> anticipations = Anticipate();
            Interaction032 intendedInteraction = (Interaction032)SelectInteraction(anticipations);
            Experiment experience = intendedInteraction.GetExperience();

            //Result result = ReturnResult010(experience);
            //Result result = ReturnResult030(experience);
            Result result = ReturnResult031(experience);

            Interaction030 enactedInteraction = GetInteraction(experience.GetLabel() + result.GetLabel());
            Console.WriteLine("Enacted " + enactedInteraction.ToString());

            if (enactedInteraction != intendedInteraction)
            {
                intendedInteraction.AddAlternateInteraction(enactedInteraction);
                Console.WriteLine("Alternate " + enactedInteraction.GetLabel());
            }

            if (enactedInteraction.GetValence() >= 0)
                this.SetMood(Mood.Pleased);
            else
                this.SetMood(Mood.Pained);

            if (enactedInteraction == intendedInteraction)
            {
                this.SetMood(Mood.SelfSatisfied);
                IncrementSelfSatisfactionCounter();
            }
            else
            {
                this.SetMood(Mood.Frustrated);
                this.SetSelfSatisfactionCounter(0);
            }

            this.LearnCompositeInteraction(enactedInteraction);
            this.SetEnactedInteraction(enactedInteraction);

            return this.GetMood().ToString();
        }

        /// <summary>
        /// Compute the system's mood and choose the next experience based on the previous interaction.
        /// </summary>
        /// <param name="anticipations">The anticipations.</param>
        /// <returns>The next experience.</returns>
        public override Interaction032 SelectInteraction(List<IAnticipation> anticipations)
        {

            anticipations.Sort();
            Interaction032 intendedInteraction = (Interaction032)this.GetOtherInteraction(null);

            if (GetSelfSatisfactionCounter() < this.BoredomLevel)
            {
                if (anticipations.Count > 0)
                {
                    Interaction032 proposedInteraction = (Interaction032)((Anticipation032)anticipations[0]).GetInteraction();
                    if (proposedInteraction.GetValence() >= 0)
                        intendedInteraction = proposedInteraction;
                    else
                        intendedInteraction = (Interaction032)this.GetOtherInteraction(proposedInteraction);
                }
            }
            else
            {
                //Trace.AddEventElement("mood", "Bored");
                this.SetSelfSatisfactionCounter(0);
                if (anticipations.Count == 1)
                    intendedInteraction = (Interaction032)this.GetOtherInteraction(((Anticipation032)anticipations[0]).GetInteraction());
                else if (anticipations.Count > 1)
                    intendedInteraction = (Interaction032)((Anticipation032)anticipations[1]).GetInteraction();
            }
            return intendedInteraction;
        }
    }
}
