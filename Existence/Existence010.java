package existence;

import java.util.HashMap;
import java.util.Map;
import coupling.Experiment;
import coupling.Result;
import coupling.interaction.Interaction;
import coupling.interaction.Interaction010;

/**
 * An Existence010 simulates a "stream of intelligence" made of a succession of Experiences and Results.   
 * The Existence010 is SELF-SATISFIED when the Result corresponds to the Result it expected, and FRUSTRATED otherwise.
 * Additionally, the Existence0 is BORED when it has been SELF-SATISFIED for too long, which causes it to try another Experience.  
 * An Existence1 is still a single entity rather than being split into an explicit Agent and Environment.
 */
public class Existence010 implements Existence {
	
	public final String LABEL_E1 = "e1"; 
	public final String LABEL_E2 = "e2"; 
	public final String LABEL_R1 = "r1";
	public final String LABEL_R2 = "r2";
	public enum Mood {SELF_SATISFIED, FRUSTRATED, BORED, PAINED, PLEASED};

	protected Map<String ,Experiment> EXPERIENCES = new HashMap<String ,Experiment>();
	protected Map<String ,Result> RESULTS = new HashMap<String ,Result>();
	protected Map<String , Interaction> INTERACTIONS = new HashMap<String , Interaction>() ;

	protected final int BOREDOME_LEVEL = 4;	
	
	private Mood mood;
	private int selfSatisfactionCounter = 0;
	private Experiment previousExperience;
	
	public Existence010(){
		initExistence();
	}	
	
	protected void initExistence(){
		Experiment e1 = addOrGetExperience(LABEL_E1);
		addOrGetExperience(LABEL_E2);
		this.setPreviousExperience(e1);
	}

	@Override
	public String step() {
		
		Experiment experience = this.getPreviousExperience();
		if (this.getMood() == Mood.BORED){
			experience = getOtherExperience(experience);		
			this.setSelfSatisfactionCounter(0);
		}
		
		Result anticipatedResult = predict(experience);
		
		Result result = returnResult010(experience);
	
		this.addOrGetPrimitiveInteraction(experience, result);
		
		if (result == anticipatedResult){
			this.setMood(Mood.SELF_SATISFIED);
			this.incSelfSatisfactionCounter();
		}
		else{
			this.setMood(Mood.FRUSTRATED);
			this.setSelfSatisfactionCounter(0);
		}
		if (this.getSelfSatisfactionCounter() >= BOREDOME_LEVEL)
			this.setMood(Mood.BORED);
		
		this.setPreviousExperience(experience);
		
		return experience.getLabel() + result.getLabel() + " " + this.getMood();
	}

	/**
	 * Create an interaction as a tuple <experience, result>.
	 * @param experience: The experience.
	 * @param result: The result.
	 * @return The created interaction
	 */
	protected Interaction addOrGetPrimitiveInteraction(Experiment experience, Result result) {
		Interaction interaction = addOrGetInteraction(experience.getLabel() + result.getLabel()); 
		interaction.setExperience(experience);
		interaction.setResult(result);
		return interaction;
	}

	/**
	 * Records an interaction in memory.
	 * @param label: The label of this interaction.
	 * @return The interaction.
	 */
	protected Interaction addOrGetInteraction(String label) {
		if (!INTERACTIONS.containsKey(label))
			INTERACTIONS.put(label, createInteraction(label));			
		return INTERACTIONS.get(label);
	}
	
	protected Interaction010 createInteraction(String label){
		return new Interaction010(label);
	}
	
	/**
	 * Finds an interaction from its label
	 * @param label: The label of this interaction.
	 * @return The interaction.
	 */
	protected Interaction getInteraction(String label){
		return INTERACTIONS.get(label);
	}
	
	/**
	 * Finds an interaction from its experience
	 * @return The interaction.
	 */
	protected Result predict(Experiment experience){
		Interaction interaction = null;
		Result anticipatedResult = null;
		
		for (Interaction i : INTERACTIONS.values())
			if (i.getExperience().equals(experience))
				interaction = i;
		
		if (interaction != null)
			anticipatedResult = interaction.getResult();
		
		return anticipatedResult;
	}

	/**
	 * Creates a new experience from its label and stores it in memory.
	 * @param label: The experience's label
	 * @return The experience.
	 */
	protected Experiment addOrGetExperience(String label) {
		if (!EXPERIENCES.containsKey(label))
			EXPERIENCES.put(label, createExperience(label));			
		return EXPERIENCES.get(label);
	}
	
	protected Experiment createExperience(String label){
		return new Experiment(label);
	}

	/**
	 * Finds an experience different from that passed in parameter.
	 * @param experience: The experience that we don't want
	 * @return The other experience.
	 */
	protected Experiment getOtherExperience(Experiment experience) {
		Experiment otherExperience = null;
		for (Experiment e : EXPERIENCES.values()){
			if (e!=experience){
				otherExperience =  e;
				break;
			}
		}		
		return otherExperience;
	}

	/**
	 * Creates a new result from its label and stores it in memory.
	 * @param label: The result's label
	 * @return The result.
	 */
	protected Result createOrGetResult(String label) {
		if (!RESULTS.containsKey(label))
			RESULTS.put(label, new Result(label));			
		return RESULTS.get(label);
	}	
	
	public Mood getMood() {
		return mood;
	}
	public void setMood(Mood mood) {
		this.mood = mood;
	}

	public Experiment getPreviousExperience() {
		return previousExperience;
	}
	public void setPreviousExperience(Experiment previousExperience) {
		this.previousExperience = previousExperience;
	}

	public int getSelfSatisfactionCounter() {
		return this.selfSatisfactionCounter;
	}
	public void setSelfSatisfactionCounter(int selfSatisfactionCounter) {
		this.selfSatisfactionCounter = selfSatisfactionCounter;
	}
	public void incSelfSatisfactionCounter(){
		this.selfSatisfactionCounter++;
	}

	/**
	 * The Environment010
	 * E1 results in R1. E2 results in R2.
	 * @param experience: The current experience.
	 * @return The result of this experience.
	 */
	public Result returnResult010(Experiment experience){
		if (experience.equals(addOrGetExperience(LABEL_E1)))
			return createOrGetResult(LABEL_R1);
		else
			return createOrGetResult(LABEL_R2);
	}

}
