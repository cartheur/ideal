using Ideal.Coupling;
using Ideal.Coupling.Interaction;

namespace Ideal.Existence
{
    public class Existence010 : IExistence
    {
        private Mood mood;
        private int selfSatisfactionCounter = 0;
        private Experiment previousExperience;

        protected Dictionary<string, Experiment> Experiences = new Dictionary<string, Experiment>();
        protected Dictionary<string, Result> Results = new Dictionary<string, Result>();
        protected Dictionary<string, Interaction> Interactions = new Dictionary<string, Interaction>();

        protected int BOREDOME_LEVEL = 4;

        public string LABEL_E1 = "e1"; 
	    public string LABEL_E2 = "e2"; 
	    public string LABEL_R1 = "r1";
	    public string LABEL_R2 = "r2";

	    public enum Mood { SELF_SATISFIED, FRUSTRATED, BORED, PAINED, PLEASED };
        /// <summary>
        /// Initializes a new instance of the <see cref="Existence010"/> class.
        /// </summary>
        public Existence010() 
        { 
            InitExistence();
        }

        protected void InitExistence()
        {
            Experiment e1 = AddOrGetExperience(LABEL_E1);
            AddOrGetExperience(LABEL_E2);
            SetPreviousExperience(e1);
        }

        #region Interaction objects
        /// <summary>
        /// Records an interaction in memory.
        /// </summary>
        /// <param name="label">The label of this interaction.</param>
        /// <returns>The interaction.</returns>
        protected Interaction AddOrGetInteraction(string label)
        {
            if (!Interactions.ContainsKey(label))
                Interactions.Add(label, CreateInteraction(label));
            return Interactions.ContainsKey(label) ? Interactions[label] : null;
        }
        /// <summary>
        /// Create an interaction as a tuple <experience, result>.
        /// </summary>
        /// <param name="experience">The experience.</param>
        /// <param name="result">The result.</param>
        /// <returns>The created interaction.</returns>
        protected Interaction AddOrGetPrimitiveInteraction(Experiment experience, Result result)
        {
            Interaction interaction = AddOrGetInteraction(experience.GetLabel() + result.GetLabel());
            interaction.SetExperience(experience);
            interaction.SetResult(result);
            return interaction;
        }
        /// <summary>
        /// Creates an interaction.
        /// </summary>
        /// <param name="label">The label.</param>
        /// <returns></returns>
        protected static Interaction010 CreateInteraction(string label)
        {
            return new Interaction010(label);
        }
        /// <summary>
        /// Finds an interaction from its label.
        /// </summary>
        /// <param name="label">The label of this interaction.</param>
        /// <returns>The interaction.</returns>
        protected Interaction GetInteraction(string label)
        {
            return (Interaction)Interactions[label];
        }
        #endregion

        #region Experiment objects

        /// <summary>
        /// Creates a new experience from its label and stores it in memory.
        /// </summary>
        /// <param name="label">The experience's label</param>
        /// <returns>The experience.</returns>
        protected Experiment AddOrGetExperience(string label)
        {
            if (!Experiences.ContainsKey(label))
                Experiences.Add(label, CreateExperience(label));
            return Experiences.ContainsKey(label) ? Experiences[label] : null;
        }

        protected static Experiment CreateExperience(string label)
        {
            return new Experiment(label);
        }
        /// <summary>
        /// Finds an experience different from that passed in parameter.
        /// </summary>
        /// <param name="experience">The undesired experience.</param>
        /// <returns>The other experience.</returns>
        protected Experiment GetOtherExperience(Experiment experience)
        {
            Experiment otherExperience = null;
            foreach (Experiment e in Experiences.Values)
            {
                if (e != experience)
                {
                    otherExperience = e;
                    break;
                }
            }
            return otherExperience;
        }

        #endregion

        #region Results to be returned
        /// <summary>
        /// Creates a new result from its label and stores it in memory.
        /// </summary>
        /// <param name="label">The result's label.</param>
        /// <returns>The result.</returns>
        protected Result CreateOrGetResult(string label)
        {
            if (!Results.ContainsKey(label))
                Results[label] = new Result(label);
            return Results[label];
        }
        /// <summary>
        /// Finds an interaction from its experience.
        /// </summary>
        /// <param name="experience">The experience.</param>
        /// <returns>The interaction.</returns>
        protected Result Predict(Experiment experience)
        {
            Interaction interaction = null;
            Result anticipatedResult = null;

            foreach (Interaction i in Interactions.Values)
                if (i.GetExperience().Equals(experience))
                    interaction = i;

            if (interaction != null)
                anticipatedResult = interaction.GetResult();

            return anticipatedResult;
        }
        /// <summary>
        /// The Environment010 
        /// * E1 results in R1.E2 results in R2.
        /// </summary>
        /// <param name="experience">The current experience.</param>
        /// <returns>The result of this experience.</returns>
        public Result ReturnResult010(Experiment experience)
        {
            if (experience.Equals(AddOrGetExperience(LABEL_E1)))
                return CreateOrGetResult(LABEL_R1);
            else
                return CreateOrGetResult(LABEL_R2);
        }

        #endregion

        #region Methods
        public Mood GetMood()
        {
            return mood;
        }
        public void SetMood(Mood mood)
        {
            this.mood = mood;
        }

        public Experiment GetPreviousExperience()
        {
            return previousExperience;
        }
        public void SetPreviousExperience(Experiment previousExperience)
        {
            this.previousExperience = previousExperience;
        }

        public int GetSelfSatisfactionCounter()
        {
            return selfSatisfactionCounter;
        }
        public void SetSelfSatisfactionCounter(int selfSatisfactionCounter)
        {
            this.selfSatisfactionCounter = selfSatisfactionCounter;
        }
        public void IncrementSelfSatisfactionCounter()
        {
            selfSatisfactionCounter++;
        }
        #endregion

        #region Overrides
        /// <summary>
        /// Perform one step of a "stream of intelligence".
        /// </summary>
        /// <returns>
        /// S string representing the "event of intelligence" that was performed.
        /// </returns>
        public string Step()
        {

            Experiment experience = GetPreviousExperience();
            if (GetMood() == Mood.BORED)
            {
                experience = GetOtherExperience(experience);
                SetSelfSatisfactionCounter(0);
            }

            Result anticipatedResult = Predict(experience);

            Result result = ReturnResult010(experience);

            AddOrGetPrimitiveInteraction(experience, result);

            if (result == anticipatedResult)
            {
                SetMood(Mood.SELF_SATISFIED);
                IncrementSelfSatisfactionCounter();
            }
            else
            {
                SetMood(Mood.FRUSTRATED);
                SetSelfSatisfactionCounter(0);
            }
            if (GetSelfSatisfactionCounter() >= BOREDOME_LEVEL)
                SetMood(Mood.BORED);

            SetPreviousExperience(experience);

            return experience.GetLabel() + result.GetLabel() + " " + GetMood();
        }
        #endregion
    }
}
