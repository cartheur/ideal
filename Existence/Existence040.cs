using Ideal.Agent;
using Ideal.Coupling;
using Ideal.Coupling.Interaction;
using System.Collections.Generic;

namespace Ideal.Existence
{
    /// <summary>
    ///   Existence040 implements two-step self-programming.
    /// </summary>
    /// <seealso cref="Ideal.Existence.Existence031" />
    public class Existence040 : Existence031
    {
        private Interaction040 previousSuperInteraction;
        private Interaction040 lastSuperInteraction;

        /// <summary>
        /// Initializes a new instance of the <see cref="Existence040"/> class.
        /// </summary>
        public Existence040() : base()
        { 
            InitExistence();
        }

        protected override void InitExistence()
        {
            Experiment040 e1 = (Experiment040)AddOrGetExperience(LABEL_E1);
            Experiment040 e2 = (Experiment040)AddOrGetExperience(LABEL_E2);
            Result r1 = CreateOrGetResult(LABEL_R1);
            Result r2 = CreateOrGetResult(LABEL_R2);
            /** Change the valence depending on the environment to obtain better behaviors */
            Interaction040 e11 = (Interaction040)AddOrGetPrimitiveInteraction(e1, r1, -1);
            Interaction040 e12 = (Interaction040)AddOrGetPrimitiveInteraction(e1, r2, 1); // Use valence 1 for Environment040 and 2 for Environment041
            Interaction040 e21 = (Interaction040)AddOrGetPrimitiveInteraction(e2, r1, -1);
            Interaction040 e22 = (Interaction040)AddOrGetPrimitiveInteraction(e2, r2, 1); // Use valence 1 for Environment040 and 2 for Environment041
            e1.SetIntendedInteraction(e12); e1.ResetAbstract();
            e2.SetIntendedInteraction(e22); e2.ResetAbstract();
        }

        public override string Step()
        {

            List<IAnticipation> anticipations = Anticipate();
            Experiment040 experience = (Experiment040)SelectExperience(anticipations);

            Interaction040 intendedInteraction = experience.GetIntendedInteraction();

            Interaction040 enactedInteraction = Enact(intendedInteraction);
            Console.WriteLine("Enacted " + enactedInteraction.ToString());

            if (enactedInteraction != intendedInteraction && experience.IsAbstract())
            {
                Result failResult = CreateOrGetResult(enactedInteraction.GetLabel().Replace('e', 'E').Replace('r', 'R') + ">");
                int valence = enactedInteraction.GetValence();
                enactedInteraction = (Interaction040)AddOrGetPrimitiveInteraction(experience, failResult, valence);
            }

            if (enactedInteraction.GetValence() >= 0)
                this.SetMood(Mood.Pleased);
            else
                this.SetMood(Mood.Pained);

            this.LearnCompositeInteraction(enactedInteraction);

            this.SetPreviousSuperInteraction(this.GetLastSuperInteraction());
            this.SetEnactedInteraction(enactedInteraction);

            return "" + this.GetMood();
        }
        /// <summary>
        /// Learn composite interactions from the previous super interaction, the context interaction, and the enacted interaction.
        /// </summary>
        /// <param name="enactedInteraction">The enacted interaction.</param>
        public override void LearnCompositeInteraction(Interaction030 enactedInteraction)
        {
            Interaction040 previousInteraction = this.GetEnactedInteraction();
            Interaction040 lastInteraction = (Interaction040)enactedInteraction;
            Interaction040 previousSuperInteraction = this.GetPreviousSuperInteraction();
            Interaction040 lastSuperInteraction = null;

            // learn [previous current] called the super interaction
            if (previousInteraction != null)
                lastSuperInteraction = AddOrGetAndReinforceCompositeInteraction(previousInteraction, lastInteraction);

            // Learn higher-level interactions
            if (previousSuperInteraction != null)
            {
                // learn [penultimate [previous current]]
                this.AddOrGetAndReinforceCompositeInteraction(previousSuperInteraction.GetPreInteraction(), lastSuperInteraction);
                // learn [[penultimate previous] current]
                this.AddOrGetAndReinforceCompositeInteraction(previousSuperInteraction, lastInteraction);
            }
            this.SetLastSuperInteraction(lastSuperInteraction);
        }

        public Interaction040 AddOrGetAndReinforceCompositeInteraction(Interaction030 preInteraction, Interaction030 postInteraction)
        {
            Interaction040 compositeInteraction = AddOrGetCompositeInteraction(preInteraction, postInteraction);
            compositeInteraction.IncrementWeight();
            if (compositeInteraction.GetWeight() == 1)
                Console.WriteLine("learn " + compositeInteraction.ToString());
            else
                Console.WriteLine("reinforce " + compositeInteraction.ToString());

            return compositeInteraction;
        }
        /// <summary>
        /// Records or get a composite interaction in memory. If a new composite interaction is created, then a new abstract experience is also created and associated to it. 
        /// </summary>
        /// <param name="preInteraction">The composite interaction's pre-interaction</param>
        /// <param name="postInteraction">The composite interaction's post-interaction</param>
        /// <returns>The learned composite interaction</returns>
        public override Interaction040 AddOrGetCompositeInteraction(Interaction030 preInteraction, Interaction030 postInteraction)
        {
            string label = "<" + preInteraction.GetLabel() + postInteraction.GetLabel() + ">";
            Interaction040 interaction = (Interaction040)GetInteraction(label);
            if (interaction == null)
            {
                interaction = (Interaction040)AddOrGetInteraction(label);
                interaction.SetPreInteraction(preInteraction);
                interaction.SetPostInteraction(postInteraction);
                interaction.SetValence(preInteraction.GetValence() + postInteraction.GetValence());
                this.AddOrGetAbstractExperience(interaction);
                //interaction.SetExperience(abstractExperience);
            }
            return interaction;
        }

        public Experiment040 AddOrGetAbstractExperience(Interaction040 interaction)
        {
            string label = interaction.GetLabel().Replace('e', 'E').Replace('r', 'R').Replace('>', '|');
            if (!Experiences.ContainsKey(label))
            {
                Experiment040 abstractExperience = new Experiment040(label);
                abstractExperience.SetIntendedInteraction(interaction);
                interaction.SetExperience(abstractExperience);
                Experiences[label] = abstractExperience;
            }
            return (Experiment040)Experiences[label];
        }

        protected override Interaction040 CreateInteraction(string label)
        {
            return new Interaction040(label);
        }
        /// <summary>
        /// Get the list of activated interactions from the enacted Interaction, the enacted interaction's post-interaction if any, and the last super interaction.
        /// </summary>
        /// <returns>
        /// The list of anticipations
        /// </returns>
        public override List<Interaction> GetActivatedInteractions()
        {
            List<Interaction> contextInteractions = new List<Interaction>();

            if (this.GetEnactedInteraction() != null)
            {
                contextInteractions.Add(this.GetEnactedInteraction());
                if (!this.GetEnactedInteraction().IsPrimitive())
                    contextInteractions.Add(this.GetEnactedInteraction().GetPostInteraction());
                if (this.GetLastSuperInteraction() != null)
                    contextInteractions.Add(this.GetLastSuperInteraction());
            }

            List<Interaction> activatedInteractions = new List<Interaction>();
            foreach (Interaction interaction in activatedInteractions)
            {
                Interaction040 activatedInteraction = (Interaction040)interaction;
                if (!activatedInteraction.IsPrimitive())
                    if (contextInteractions.Contains(activatedInteraction.GetPreInteraction()))
                    {
                        activatedInteractions.Add(activatedInteraction);
                        Console.WriteLine("activated " + activatedInteraction.ToString());
                    }
            }
            return activatedInteractions;
        }

        protected override List<IAnticipation> GetDefaultAnticipations()
        {
            List<IAnticipation> anticipations = new List<IAnticipation>();
            foreach (Experiment experience in Experiences.Values)
            {
                Experiment040 defaultExperience = (Experiment040)experience;
                if (!defaultExperience.IsAbstract())
                {
                    Anticipation031 anticipation = new Anticipation031(experience, 0);
                    anticipations.Add(anticipation);
                }
            }
            return anticipations;
        }

        public Interaction040 Enact(Interaction030 intendedInteraction)
        {
            if (intendedInteraction.IsPrimitive())
                return EnactPrimitiveInteraction(intendedInteraction);
            else
            {
                // Enact the pre-interaction
                Interaction040 enactedPreInteraction = Enact(intendedInteraction.GetPreInteraction());
                if (!enactedPreInteraction.Equals(intendedInteraction.GetPreInteraction()))
                    // if the preInteraction failed then the enaction of the intendedInteraction is interrupted here.
                    return enactedPreInteraction;
                else
                {
                    // Enact the post-interaction
                    Interaction040 enactedPostInteraction = Enact(intendedInteraction.GetPostInteraction());
                    return (Interaction040)AddOrGetCompositeInteraction(enactedPreInteraction, enactedPostInteraction);
                }
            }
        }
        /// <summary>
        /// Implements the cognitive coupling between the agent and the environment.
        /// </summary>
        /// <param name="intendedPrimitiveInteraction">The intended primitive interaction to try to enact against the environment.</param>
        /// <returns>The actually enacted primitive interaction.</returns>
        public Interaction040 EnactPrimitiveInteraction(Interaction030 intendedPrimitiveInteraction)
        {
            Experiment experience = intendedPrimitiveInteraction.GetExperience();

            // Change the returnResult() to change the environment.
            // Change the valence of primitive interactions to obtain better behaviors.

            //Result result = ReturnResult010(experience);
            //Result result = ReturnResult030(experience);
            //Result result = ReturnResult031(experience);
            Result result = ReturnResult040(experience);
            //Result result = ReturnResult041(experience);
            return (Interaction040)this.AddOrGetPrimitiveInteraction(experience, result);
        }

        protected override Experiment040 CreateExperience(string label)
        {
            return new Experiment040(label);
        }

        public new Interaction040 GetEnactedInteraction()
        {
            return (Interaction040)base.GetEnactedInteraction();
        }

        public Interaction040 GetPreviousSuperInteraction()
        {
            return previousSuperInteraction;
        }

        public void SetPreviousSuperInteraction(Interaction040 previousSuperInteraction)
        {
            this.previousSuperInteraction = previousSuperInteraction;
        }

        public Interaction040 GetLastSuperInteraction()
        {
            return lastSuperInteraction;
        }

        public void SetLastSuperInteraction(Interaction040 lastSuperInteraction)
        {
            this.lastSuperInteraction = lastSuperInteraction;
        }

        private Experiment penultimateExperience;

        protected void SetPenultimateExperience(Experiment penultimateExperience)
        {
            this.penultimateExperience = penultimateExperience;
        }

        protected Experiment GetPenultimateExperience()
        {
            return this.penultimateExperience;
        }

        public Result ReturnResult040(Experiment experience)
        {
            Result result = this.CreateOrGetResult(this.LABEL_R1);

            if (this.GetPenultimateExperience() != experience &&
                this.GetPreviousExperience() == experience)
            {
                result = this.CreateOrGetResult(this.LABEL_R2);
            }

            this.SetPenultimateExperience(this.GetPreviousExperience());
            this.SetPreviousExperience(experience);

            return result;
        }

        // Environment041
        // The agent must alternate experiences e1 and e2 every third cycle to get one r2 result the third time: e1->r1 e1->r1 e1->r2 e2->r1 e2->r1 e2->r2 etc.

        protected Experiment antepenultimateExperience;

        protected void SetAntePenultimateExperience(Experiment antepenultimateExperience)
        {
            this.antepenultimateExperience = antepenultimateExperience;
        }

        protected Experiment GetAntePenultimateExperience()
        {
            return this.antepenultimateExperience;
        }

        public Result ReturnResult041(Experiment experience)
        {
            Result result = this.CreateOrGetResult(this.LABEL_R1);

            if (this.GetAntePenultimateExperience() != experience &&
                this.GetPenultimateExperience() == experience &&
                this.GetPreviousExperience() == experience)
            {
                result = this.CreateOrGetResult(this.LABEL_R2);
            }

            this.SetAntePenultimateExperience(this.GetPenultimateExperience());
            this.SetPenultimateExperience(this.GetPreviousExperience());
            this.SetPreviousExperience(experience);

            return result;
        }
    }
}
