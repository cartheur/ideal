# ideal
An ideal algorithm for control in the manner of Cybernetics. This branch contains details of constructivist epistemology.

## Code module

This branch contains the modules 030, 031, and 032 that expand upon the code from the continuity of the previous branches.

# Section 3: An introduction to constructivist epistemology (theory of knowledge)

The key concept that we want to convey in Lesson 3 is:

`Cognitive agents must discover, learn, and exploit regularities of interaction.`

_Regularities of interaction_ (in short, _regularities_) are patterns of interaction that occur consistently. Regularities depend on the coupling between the agent and the environment. That is, they depend both on the structure of the environment, and on the possibilities of interaction that the agent has at its disposal.

At least since Immanuel Kant, philosophers have widely agreed on the fact that cognitive systems can never know "the world as such", but only the world as it appears to them through sensorimotor interactions. For example, in some situations, if you spread your arm repeatedly, and if you consistently experience the same sensorimotor pattern, you may infer that there is something constant _out there_ that always makes this same sensorimotor pattern possible. Note that regularities can be experienced through arbitrarily complex _instruments_, which may range from a stick in your hand to complex experimental settings such as those used by physicists to interact with _something out there_ known as the Higgs boson.

These philosophical ideas translate into AI when we acknowledge the fact that knowledge is constructed from regularities of interactions rather than recorded from input data. Designing a system that would construct complete knowledge of the world out there and exploit this model for the better is a part of AI's long-term objective.

To take this problem gradually, Lesson 3 begins with implementing an agent that can detect simple sequential regularities and exploit them to satisfy its rudimentary motivational system.

## 3.1: Learning regularities of interaction

Figure 3.1 presents the principles of a rudimentary system that learns and exploits two-step regularities of interaction.

![Figure-32](/images/032-1.png)
Figure 3.1: Rudimentary learning of regularities of interaction.

On time step `t`, the agent _enacts_ the interaction $i_t = ⟨e_t,r_t⟩$. Enacting it means experimenting $e_t$ and receiving a result $r_t$ (§2.1). The agent records the two-step sequence `⟨it-1,it⟩` made by the previously enacted interaction $i_t-1$ and of $i_t$. The sequence of interactions $⟨i_t-1,i_t⟩$ is called a composite interaction. $i_t-1$ is called $⟨i_t-1,i_t⟩$'s pre-interaction, and it is called $⟨i_t-1,i_t⟩$'s post-interaction. From now on, low-level interactions $i = ⟨e,r⟩$ will be called primitive interactions to differentiate them from composite interactions.

The enacted primitive interaction it activates previously learned composite interactions when it matches their pre-interaction. For example, if $i_t = a$ and if the composite interaction `⟨a,b⟩` has been learned before time `t`, then the composite interaction `⟨a,b⟩` is activated, meaning it is recalled from memory. Activated composite interactions propose their post-interaction's experiment, in this case: `b`'s experiment. If the sequence `⟨a,b⟩` corresponds to a regularity of interaction, then it is probable that the sequence `⟨a,b⟩` can be enacted again. Therefore, the agent can anticipate that performing `b`'s experiment will likely produce `b`'s result. The agent can thus base its choice of the next experiment on this anticipation.

Note that the enacted primitive interaction it may activate more than one composite interaction, each of them proposing different experiments. We create an interactionally motivated agent by implementing a decision mechanism that uses the agent's capacity of anticipation to choose experiments that will likely result in interactions that have a positive valence, and avoid experiments that will likely result in interactions that have a negative valence.

## 3.2: Algorithm for learning regularities of interaction

Here is a rudimentary interactionally motivated algorithm that enables the agent to learn and exploit two-step regularities of interaction.

Table 3.1 presents its main loop while Tables 3.2 and 3.3 present subroutines.

In Table 3.1, we chose a set of valences and a particular environment to demonstrate this learning mechanism: this agent is pleased when it receives result `r2`, but it must learn that the environment returns `r2` only if it alternates experiments `e1` and `e2` every second cycle. Your programming activities will consist of experimenting with other valences and other environments.


```
Table 3.1: Main loop of an interactionally motivated algorithm that learns two-step sequences of interaction.

01  createPrimitiveInteraction(e1, r1, -1)
02  createPrimitiveInteraction(e1, r2, 1)
03  createPrimitiveInteraction(e2, r1, -1)
04  createPrimitiveInteraction(e2, r2, 1)
05  while()
06     contextInteraction = enactedInteraction
07     anticipations = anticipate(enactedInteraction)
08     experiment = selectExperiment(anticipations)

09     if (experiment = previousExperiment)
10        result = r1
11     else
12        result = r2
13     previousExperiment = experiment

14     enactedInteraction = getInteraction(experiment, result)
15     if (enactedInteraction.valence ≥ 0)
16        mood = PLEASED
17     else
18        mood = PAINED
19     learnCompositeInteraction(contextInteraction, enactedInteraction)
```

Table 3.1, lines 01 to 04 initialize the primitive interactions (similar to §2.3) to specify the agent's preferences. In this particular configuration, interactions whose result is `r1` have a negative valence, and interactions whose result is `r2` have a positive valence. 06: the previously enacted interaction is memorized as the _context interaction_. 07: computes anticipations in the context of the previous enacted interaction. 08: selects an experiment from the anticipations.

Lines 09 to 13 implement the environment. This new environment was designed to demonstrate the benefit of learning two-step regularities of interaction. If the experiment equals the previous experiment then result is `r1`, otherwise the result is `r2`.

Lines 14 to 18: the enacted interaction is retrieved from memory; and the agent is pleased if its valence is positive (similar to §2.3). Line 19: the agent records the composite interaction as a tuple ‹`contextInteraction`, `enactedInteraction`› in memory.

Table 3.2 presents a simple version of the `learnCompositeInteraction()`, `anticipate()`, and `selectExperiment()` functions.

```
Table 3.2: Pseudocode of a simple version.

01   function learnCompositeInteraction(contextInteraction, enactedInteraction)
02      compositeInteraction = create new tuple(contextInteraction, enactedInteraction)
03      if compositeInteraction already in the list of known interactions 
04         do nothing
05      else
06         add compositeInteraction to the list of known interactions

10   function anticipate(enactedInteraction)
11      for each interaction in the list of known interactions
12          if interaction.preInteraction = enactedInteraction
13             create new anticipation(interaction.postInteraction)
14      return the list of anticipations

20   function selectExperiment(anticipations)
21      sort the list anticipations by decreasing valence of their proposed interaction.
22      if anticipation[0].interaction.valence ≥ 0
23         return anticipation[0].interaction.experiment
24      else
25         return another experiment than anticipation[0].interaction.experiment
```

The `anticipate()` function checks for known composite interactions whose pre-interactions match the last enacted primitive interaction; we call these the activated composite interactions. A new object, _anticipation_, is created for each activated composite interaction. The activated composite interaction's post-interaction is associated with this anticipation as the anticipation's _proposed interaction_. The `selectExperiment()` function sorts the list of anticipations by decreasing valence of their proposed interaction. Then, it takes the fist anticipation (index [0]), which has the highest valence in the list. If this valence is positive, then the agent wants to re-enact this proposed interaction, leading to the agent choosing this proposed interaction's experiment.

This solution works in a very simple environment that generates no competing anticipations. However, for environments that may generate competing anticipations, we want the agent to be able to balance competing anticipations based on their probabilities of realization. We may have an environment that, in a given context, makes all the four interactions likely to happen but with different probabilities. For example, in the context in which `e1r1` was enacted, both `e1` and `e2` may result sometimes in `r1` and sometimes in `r2`. But, e1 is more likely to result in `r2` than `e2`. To handle this kind of environment, we associate a weight to composite interactions, as shown in Table 3.3.

```
Table 3.3: Pseudocode for weighted anticipations.

01   function learnCompositeInteraction(contextInteraction, enactedInteraction)
02      compositeInteraction = create new tuple(contextInteraction, enactedInteraction)
03      if compositeInteraction already in the list of known interactions 
04         increment compositeInteraction's weight
05      else
06         add compositeInteraction to the list of known interactions with a weight of 1

10   function anticipate(enactedInteraction)
11      for each interaction in the list of known interactions
12         if interaction.preInteraction = enactedInteraction
13            proposedExperiment = interaction.postInteraction.experiment
14            proclivity = interaction.weight * interaction.postInteraction.valence
15            if an anticipation already exists for proposedExperience 
16               add proclivity to this anticipation's proclivity
17            else
18               create new anticipation(proposedExperiment) with proclivity proclivity
19      return the list of anticipations

20   function selectExperiment(anticipations)
21      sort the list anticipations by decreasing proclivity.
22      if anticipation[0].proclivity ≥ 0
23         return anticipation[0].experiment
24      else
25         return another experiment than anticipation[0].experiment
```

Now, the `learnCompositeInteraction()` function either records or reinforces composite interactions. The `anticipate()` function generates an anticipation for each proposed experiment. Anticipations have a proclivity value computed from the weight of the proposing activated composite interaction multiplied by the valence of the proposed interaction. As a result, the anticipations that are the most likely to result in the primitive interaction that have the highest valence receive the highest proclivity. In the example above, in the context when `e1r1` has been enacted, the agent learns to choose `e1` because it will more likely result in a positive interaction than `e2`.

## 3.3: Implementing learning of regularities of interaction

If you have no interest in programming, you can skip to the next section.

Project 3 (files to modify or to add to Project 2)

```
Program.cs ← Uncomment the instructions to instantiate Existence030 or Existence031.
existence / Existence030 ← The program that implements the algorithm in Tables 3.1 and 3.2.
existence / Existence031 ← The program that implements the algorithm in Tables 3.1 and 3.3.
agent / Anticipation ← An anticipation generated by the method computeAnticipations().
agent / Anticipation030 ← A simple anticipation based on the afforded interactions.
agent / Anticipation031 ← A more complex anticipation based on weighted composite interactions.
coupling / interaction / Interaction030 ← Now, Interactions can be primitive or composite.
coupling / interaction / Interaction031 ← Interaction031s have a weight.
```

For Lesson 3, your programming activities are:

1. Change Program.cs to instantiate `Existence030` and run it. Observe that the trace is similar to that in the next section.
2. Change `Existence030` to instantiate Environment010 instead of `Environment030` and run it. Observe that the modified `Existence030` also learns to get pleased when it implements `Environment010` instead of `Environment030`.
3. Change `Program.cs` to instantiate `Existence031` and run it. Observe that it learns to be pleased in `Environment031`.
4. Change `Existence031` to instantiate `Environment010` and then `Environment030` and run it. Observe that the modified `Existence031` also learns to be pleased when it implements `Environment010`, `Environment030`, and `Environment031`.
Lesson 3 shows that `Existence031` can adapt to three different environments (`Environment010`, `Environment030`, `Environment031`). However, `Existence031` will fail in environments that require learning regularities longer than two interaction cycles. Future lessons teach to design agents that can learn and exploit arbitrarily long regularities of interaction.

## 3.4: Behavioral analysis of a rudimentary constructivist agent

Table 3.4 shows the trace that you should see in your console if you ran Project 3. If you did not run it, you can refer to the pseudocode presented in Tables 3.1 and 3.2 to understand this trace.

```
Table 3.4: Activity trace of a rudimentary interactionally motivated regulartity learning agent.

Enacted e1r2,1
0: PLEASED
Enacted e1r1,-1
learn e1r2e1r1
1: PAINED
Enacted e1r1,-1
learn e1r1e1r1
2: PAINED
afforded e1r1,-1
Enacted e2r2,1
learn e1r1e2r2
3: PLEASED
Enacted e1r2,1
learn e2r2e1r2
4: PLEASED
afforded e1r1,-1
Enacted e2r2,1
learn e1r2e2r2
5: PLEASED
```

Your activity, for Lesson 3, is to understand the trace in Table 3.4. On Cycle 0, why did the agent choose `e1`? Why did it receive `r2`? On Cycle 1, why did it choose `e1` again? Why did it receive `r1`? On Cycle 3, why did it choose `e2` and received `r2`? What will the agent do after cycle 5, and what will be its mood?

## 3.5: Selected readings on constructivist epistemology for artificial intelligence

* An excellent introduction to constructivism in artificial intelligence: Riegler A. (2007) The radical constructivist dynamics of cognition. In: Wallace, B. (ed.) The Mind, the Body and the World: Psychology After Cognitivism? Imprint: London, pp. 91-115.
* Unfortunately, the English Wikipedia article on Constructivist epistemology is quite poor as compared to the French article which is excellent. I wonder if that is indicative of the relative weakness of American research in constructivist artificial intelligence. For those who can't read French, I would recommend trying to get a sense of the French article from an automatic translation rather than reading the English article.
* A nice argument in favor of constructivist learning in robotics, which paraphrases Piaget's famous book, is: Ziemke T. (2001). The Construction of 'Reality' in the Robot: Constructivist Perspectives on Situated Artificial Intelligence and Adaptive Robotics. Foundations of Science, special issue on "The Impact of Radical Constructivism on Science", edited by A. Riegler, 2001, vol. 6, no. 1-3: 163-233.
* For those who do read French, I hightly recommend Jean-Louis Le Moigne (1995). Les �pist�mologies constructivistes Que sais-je? Presse Universitaire de France. This short book discusses constructivist epistemology from a philosophical perspective, making it a helpful initial guide to constructivist thought and design.

This ends Lesson 3.
