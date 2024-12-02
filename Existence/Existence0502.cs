using Ideal.Agent;
using Ideal.Coupling.Interaction;
using Ideal.Environment;

namespace Ideal.Existence
{
    /// <summary>
    /// Existence050 implements radical interactionism.
    /// </summary>
    public class Existence0502 : Existence050
    {
        private IEnvironment _environment;

        protected IEnvironment GetEnvironment()
        {
            return this._environment;
        }

        protected override void InitExistence()
        {
            // You can instantiate another environment here.
            this._environment = new Environment050(this);
            //_environment = new EnvironmentMaze(this);
        }

        public override string Step()
        {
            List<IAnticipation> anticipations = Anticipate();
            Interaction040 intendedInteraction = (Interaction040)SelectInteraction(anticipations);

            Interaction040 enactedInteraction = Enact(intendedInteraction);
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

            this.LearnCompositeInteraction(enactedInteraction);

            this.SetPreviousSuperInteraction(this.GetLastSuperInteraction());
            this.SetEnactedInteraction(enactedInteraction);

            return "" + this.GetMood();
        }
        /// <summary>
        /// Create an interaction from its label..
        /// </summary>
        /// <param name="label">This interaction's label..</param>
        /// <param name="valence">The interaction's valence</param>
        /// <returns>The created interaction</returns>
        public override Interaction040 AddOrGetPrimitiveInteraction(string label, int valence)
        {
            if (!Interactions.ContainsKey(label))
            {
                Interaction040 interaction = CreateInteraction(label);
                interaction.SetValence(valence);
                Interactions[label] = interaction;
            }
            Interaction040 existingInteraction = (Interaction040)Interactions[label];
            return existingInteraction;
        }

        public override Interaction040 Enact(Interaction030 intendedInteraction)
        {
            if (intendedInteraction.IsPrimitive())
                return (Interaction040)this.GetEnvironment().Enact(intendedInteraction);
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

        protected Interaction032 SelectInteraction(List<IAnticipation> anticipations)
        {
            anticipations.Sort();
            foreach (IAnticipation anticipation in anticipations)
                Console.WriteLine("anticipate " + anticipation.ToString());

            Anticipation030 selectedAnticipation = (Anticipation030)anticipations[0];
            Interaction032 intendedInteraction = (Interaction032)selectedAnticipation.GetInteraction();

            return intendedInteraction;
        }
        /// <summary>
        /// Computes the list of anticipations.
        /// </summary>
        /// <returns>
        /// The list of aniticipations.
        /// </returns>
        public override List<IAnticipation> Anticipate()
        {
            List<IAnticipation> anticipations = GetDefaultAnticipations();
            List<Interaction> activatedInteractions = this.GetActivatedInteractions();

            foreach (Interaction activatedInteraction in activatedInteractions)
            {
                Interaction032 proposedInteraction = (Interaction032)((Interaction032)activatedInteraction).GetPostInteraction();
                int proclivity = ((Interaction032)activatedInteraction).GetWeight() * proposedInteraction.GetValence();
                Anticipation032 anticipation = new Anticipation032(proposedInteraction, proclivity);
                int index = anticipations.IndexOf(anticipation);
                if (index < 0)
                    anticipations.Add(anticipation);
                else
                    ((Anticipation032)anticipations[index]).AddProclivity(proclivity);
            }

            foreach (IAnticipation anticipation in anticipations)
            {
                foreach (Interaction interaction in ((Interaction032)((Anticipation032)anticipation).GetInteraction()).GetAlternateInteractions())
                {
                    foreach (Interaction activatedInteraction in activatedInteractions)
                    {
                        if (interaction == ((Interaction032)activatedInteraction).GetPostInteraction())
                        {
                            int proclivity = ((Interaction032)activatedInteraction).GetWeight() * ((Interaction032)interaction).GetValence();
                            ((Anticipation032)anticipation).AddProclivity(proclivity);
                        }
                    }
                }
            }

            return anticipations;
        }

        protected override List<IAnticipation> GetDefaultAnticipations()
        {
            List<IAnticipation> anticipations = new List<IAnticipation>();
            foreach (Interaction i in Interactions.Values)
            {
                Interaction032 interaction = (Interaction032)i;
                if (interaction.IsPrimitive())
                {
                    Anticipation032 anticipation = new Anticipation032(interaction, 0);
                    anticipations.Add(anticipation);
                }
            }
            return anticipations;
        }
    }
}
