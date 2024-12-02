using Ideal.Agent;
using Ideal.Coupling;
using Ideal.Coupling.Interaction;

namespace Ideal.Existence
{
    /// <summary>
    /// Existence031 can adapt to Environment010 020 030 031. Like Existence030, Existence031 seeks to enact interactions that have positive valence. Existence031 illustrates the benefit of reinforcing the weight of composite interactions and of using the weight of activated interactions to balance the decision.
    /// </summary>
    public class Existence031 : Existence030
    {
        public override string Step()
        {
            List<IAnticipation> anticipations = Anticipate();
            Experiment experience = SelectExperience(anticipations);

            // Change the call to the function returnResult to change the environment.

            Result result = ReturnResult010(experience);
            //Result result = ReturnResult030(experience);
            //Result result = ReturnResult031(experience);

            Interaction031 enactedInteraction = (Interaction031)GetInteraction(experience.GetLabel() + result.GetLabel());
            Console.WriteLine("Enacted " + enactedInteraction.ToString());

            if (enactedInteraction.GetValence() >= 0)
                SetMood(Mood.Pleased);
            else
                SetMood(Mood.Pained);

            LearnCompositeInteraction(enactedInteraction);

            SetEnactedInteraction(enactedInteraction);

            return GetMood().ToString();

        }

        public new void LearnCompositeInteraction(Interaction030 enactedInteraction)
        {
            Interaction030 preInteraction = GetEnactedInteraction();
            Interaction030 postInteraction = enactedInteraction;
            if (preInteraction != null)
            {
                Interaction031 interaction = (Interaction031)AddOrGetCompositeInteraction(preInteraction, postInteraction);
                interaction.IncrementWeight();
            }
        }

        protected override Interaction031 CreateInteraction(string label)
        {
            return new Interaction031(label);
        }

        public override List<IAnticipation> Anticipate()
        {
            List<IAnticipation> anticipations = GetDefaultAnticipations();

            if (GetEnactedInteraction() != null)
            {
                foreach (Interaction activatedInteraction in GetActivatedInteractions())
                {
                    Anticipation031 proposition = new Anticipation031(((Interaction031)activatedInteraction).GetPostInteraction().GetExperience(), ((Interaction031)activatedInteraction).GetWeight() * ((Interaction031)activatedInteraction).GetPostInteraction().GetValence());
                    int index = anticipations.IndexOf(proposition);
                    if (index < 0)
                        anticipations.Add(proposition);
                    else
                        ((Anticipation031)anticipations[index]).AddProclivity(((Interaction031)activatedInteraction).GetWeight() * ((Interaction031)activatedInteraction).GetPostInteraction().GetValence());
                }
            }
            return anticipations;
        }

        protected virtual List<IAnticipation> GetDefaultAnticipations()
        {
            List<IAnticipation> anticipations = new List<IAnticipation>();
            foreach (Experiment experience in Experiences.Values)
            {
                Anticipation031 anticipation = new Anticipation031(experience, 0);
                anticipations.Add(anticipation);
            }
            return anticipations;
        }

        public Experiment SelectExperience(List<IAnticipation> anticipations)
        {
            // The list of anticipations is never empty because all the experiences are proposed by default with a proclivity of 0
            anticipations.Sort();
            foreach (IAnticipation anticipation in anticipations)
                Console.WriteLine("propose " + anticipation.ToString());

            Anticipation031 selectedAnticipation = (Anticipation031)anticipations[0];
            return selectedAnticipation.GetExperience();
        }

        protected new Interaction031 GetInteraction(string label)
        {
            return (Interaction031)Interactions[label];// this makes sense when the key is not found in the dictionary.
        }

        public new Interaction031 GetEnactedInteraction()
        {
            return (Interaction031)base.GetEnactedInteraction();
        }

        // Environment031: Before time T1 and after time T2: E1 results in R1; E2 results in R2 between time T1 and time T2: E1 results R2; E2 results in R1.
        protected readonly int T1 = 8;
        protected readonly int T2 = 15;
        private int clock = 0;

        protected int GetClock()
        {
            return clock;
        }

        protected void IncClock()
        {
            clock++;
        }

        public Result ReturnResult031(Experiment experience)
        {
            Result result = null;

            IncClock();

            if (GetClock() <= T1 || GetClock() > T2)
            {
                if (experience.Equals(AddOrGetExperience(LABEL_E1)))
                    result = CreateOrGetResult(LABEL_R1);
                else
                    result = CreateOrGetResult(LABEL_R2);
            }
            else
            {
                if (experience.Equals(AddOrGetExperience(LABEL_E1)))
                    result = CreateOrGetResult(LABEL_R2);
                else
                    result = CreateOrGetResult(LABEL_R1);
            }
            return result;
        }
    }
}
