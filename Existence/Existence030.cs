using Ideal.Coupling;
using Ideal.Coupling.Interaction;
using Ideal.Agent;

namespace Ideal.Existence
{
    /// <summary>
    /// Existence030 is a sort of Existence020. It learns composite interactions(Interaction030).  It bases its next choice on anticipations that can be made from reactivated composite interactions.
    /// Existence030 illustrates the benefit of basing the next decision upon the previous enacted Interaction.
    /// </summary>
    public class Existence030 : Existence020
    {
        private Interaction030 enactedInteraction;

        protected new void InitExistence()
        {
            Experiment e1 = AddOrGetExperience(LABEL_E1);
            Experiment e2 = AddOrGetExperience(LABEL_E2);
            Result r1 = CreateOrGetResult(LABEL_R1);
            Result r2 = CreateOrGetResult(LABEL_R2);
            AddOrGetPrimitiveInteraction(e1, r1, -1);
            AddOrGetPrimitiveInteraction(e1, r2, 1);
            AddOrGetPrimitiveInteraction(e2, r1, -1);
            AddOrGetPrimitiveInteraction(e2, r2, 1);
        }

        public new string Step()
        {
            List<IAnticipation> anticipations = Anticipate();
            Experiment experience = SelectInteraction(anticipations).GetExperience();

            Result result = ReturnResult030(experience);

            Interaction030 enactedInteraction = GetInteraction(experience.GetLabel() + result.GetLabel());
            Console.WriteLine("Enacted " + enactedInteraction.ToString());

            if (enactedInteraction.GetValence() >= 0)
                SetMood(Mood.PLEASED);
            else
                SetMood(Mood.PAINED);

            LearnCompositeInteraction(enactedInteraction);
            SetEnactedInteraction(enactedInteraction);

            return "" + GetMood();
        }
        /// <summary>
        /// Learn the composite interaction from the previous enacted interaction and the current enacted interaction.
        /// </summary>
        /// <param name="interaction">The interaction.</param>
        public void LearnCompositeInteraction(Interaction030 interaction)
        {
            Interaction030 preInteraction = GetEnactedInteraction();
            Interaction030 postInteraction = interaction;
            if (preInteraction != null)
                AddOrGetCompositeInteraction(preInteraction, postInteraction);
        }
        /// <summary>
        /// Records a composite interaction in memory
        /// </summary>
        /// <param name="preInteraction">The composite interaction's pre-interaction.</param>
        /// <param name="postInteraction">The composite interaction's post-interaction.</param>
        /// <returns>The learned composite interaction.</returns>
        public Interaction030 AddOrGetCompositeInteraction(
            Interaction030 preInteraction, Interaction030 postInteraction)
        {
            int valence = preInteraction.GetValence() + postInteraction.GetValence();
            Interaction030 interaction = (Interaction030)AddOrGetInteraction(preInteraction.GetLabel() + postInteraction.GetLabel());
            interaction.SetPreInteraction(preInteraction);
            interaction.SetPostInteraction(postInteraction);
            interaction.SetValence(valence);
            Console.WriteLine("learn " + interaction.GetLabel());
            return interaction;
        }

        protected new Interaction030 CreateInteraction(String label)
        {
            return new Interaction030(label);
        }
        /// <summary>
        /// Computes the list of anticipations.
        /// </summary>
        /// <returns>The list of anticipations.</returns>      
        public List<IAnticipation> Anticipate()
        {
            List<IAnticipation> anticipations = new List<IAnticipation>();
            if (GetEnactedInteraction() != null)
            {
                foreach (Interaction activatedInteraction in GetActivatedInteractions())
                {
                    Interaction030 proposedInteraction = ((Interaction030)activatedInteraction).GetPostInteraction();
                    anticipations.Add(new Anticipation030(proposedInteraction));
                    Console.WriteLine("afforded " + proposedInteraction.ToString());
                }
            }
            return anticipations;
        }

        protected Interaction030 SelectInteraction(List<IAnticipation> anticipations)
        {
            anticipations.Sort();
            Interaction intendedInteraction;

            if (anticipations.Count > 0)
            {
                Interaction030 affordedInteraction = anticipations[0].GetInteraction() as Anticipation030;
                if (affordedInteraction.GetValence() >= 0)
                    intendedInteraction = affordedInteraction;
                else
                    intendedInteraction = (Interaction030)GetOtherInteraction(affordedInteraction);
            }
            else
                intendedInteraction = GetOtherInteraction(null);

            return (Interaction030)intendedInteraction;
        }
        /// <summary>
        /// Get the list of activated interactions. An activated interaction is a composite interaction whose preInteraction matches the enactedInteraction.
        /// </summary>
        /// <returns>The list of anticipations</returns>
        public List<Interaction> GetActivatedInteractions()
        {
            List<Interaction> activatedInteractions = new List<Interaction>();
            if (GetEnactedInteraction() != null)
                foreach (Interaction activatedInteraction in Interactions.Values)
                    if (((Interaction030)activatedInteraction).GetPreInteraction() == GetEnactedInteraction())
                        activatedInteractions.Add((Interaction030)activatedInteraction);
            return activatedInteractions;
        }

        protected new Interaction030 GetInteraction(string label)
        {
            return (Interaction030)Interactions[label];
        }
        /// <summary>
        /// Gets the other interaction.
        /// </summary>
        /// <param name="interaction">The interaction.</param>
        /// <returns></returns>
        public Interaction GetOtherInteraction(Interaction interaction)
        {
            Interaction otherInteraction = (Interaction)Interactions.Values.ToArray()[0];
            if (interaction != null)
                foreach (Interaction e in Interactions.Values)
                {
                    if (e.GetExperience() != null && e.GetExperience() != interaction.GetExperience())
                    {
                        otherInteraction = e;
                        break;
                    }
                }
            return otherInteraction;
        }

        protected void SetEnactedInteraction(Interaction030 enactedInteraction)
        {
            this.enactedInteraction = enactedInteraction;
        }
        protected Interaction030 GetEnactedInteraction()
        {
            return this.enactedInteraction;
        }

        /// <summary>
        /// Environment030
        /// * Results in R1 when the current experience equals the previous experience
        /// * and in R2 when the current experience differs from the previous experience.
        /// </summary>
        /// <param name="experience">The experience.</param>
        /// <returns></returns>       
        protected Result ReturnResult030(Experiment experience)
        {
            Result result = null;
            if (GetPreviousExperience() == experience)
                result = CreateOrGetResult(this.LABEL_R1);
            else
                result = CreateOrGetResult(this.LABEL_R2);
            SetPreviousExperience(experience);

            return result;
        }
    }
}
