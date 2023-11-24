# ideal
An ideal algorithm for control in the manner of Cybernetics. This branch contains details of self programming.

## Code module

This branch contains module 040 that expands upon the code from the continuity of the previous branches.

# Section 4: An introduction to self-programming

## Defining self-programming

We define a self-programming agent as an agent that can autonomously acquire executable code and re-execute this code appropriately.

Similar to other machine learning agents, self-programming agents record data in memory as they learn. Traditional machine learning agents, however, run a predefined program that exploits this data as parameters. Self-programming agents also run a predefined program, but this program can control the execution of learned data as sequences of instructions.

To understand the full implication of this definition, it is important to take a cognitive science perspective rather than a software development perspective. A natural cognitive system (an animal) does not have a compiler or an interpreter to exploit a programming language. The only thing at its disposal that remotely resembles an instruction set is the set of interaction possibilities it has with the world around it. The only thing at its disposal that remotely resembles an execution engine is its cognitive system which allows it to execute and learn sequences of interactions with the world.

We draw inspiration from natural cognitive systems to design self-programming agents. Like biological systems, these agents program themselves using the instruction set at their disposal (the set of interaction possibilities they have with the world), and the execution engine at their disposal (their cognitive system that executes and learns sequences of interactions with the world).

## Why self-programming is important?

There are profound theoretical reasons why self-programming is decisive to achieve artificial intelligence. Page 45 will refer you to some articles that elaborate on these reasons, specifically coming from the theory of enaction.

Nevertheless, we can already give a simple and intuitive answer. If we build two identical robots, it would be no fun if these two robots generated similar behaviors in the same circumstances. As they develop, we would like them to assess situations differently, make different choices, and carry out different behaviors. Only when we see these significant behavioral differences emerge will we be willing to consider each robot as an intelligent being as opposed to a mere automaton.

Since assessing the situation, making choices, and carrying out behaviors is the role of programs, we will only manage to make significant behavioral differences emerge autonomously if the robot can program itself autonomously.

Of course, defining "significant behavioral differences" is challenging. We recorded Video 41 to give an example. It was already presented in the IDEAL MOOC home page, but we invite you to watch it again.

[VIDEO](https://www.youtube.com/watch?v=91kKzybt8XY&t=2s)
Video 41: Example of self-programming. These two agents implement the same initial algorithm. Yet, because they go through different individual experiences, they find different strategies to catch a prey.

As has already been said, self-programming raises many questions.

## Self-programming to what end?

An obvious question is why the system should choose to learn a specific program rather than another. We must implement driving principles to give a direction to the system's development, without specifying a final goal so that the system can keep learning new programs indefinitely.

Interactional motivation, presented in Lesson 2, provides us with a possible answer to this challenge because it allows us to specify inborn behavioral preferences without specifying a predefined goal. An interactionally motivated self-programming agent will choose to learn programs that can help it enact interactions that have a high positive valence, while avoiding situations where interactions with a negative valence are likely to happen.

Interactional motivation is not the only principle that can drive self-programming, but it is the approach we will continue using in the examples in Lesson 4.

## Where do the learned programs come from?

Constructivist epistemology, presented in Lesson 3, provides us with a possible answer to this question: the learned programs can come from the same place where all of the agent's knowledge comes from: regularities of interaction. This leads us to Lesson 4's key concept:

`Self-programming consists of the re-enaction of regularities of interaction.`

## Exploiting regularities through self-programming

We already introduced the problem of learning regularities on Page 32. There was, however, no self-programming in this exemple because the system could not re-enact the learned regularities as full sequences of interactions. Figure 42 builds upon the regularity learning mechanism to present our design principles for self-programming.

![Figure-42](/images/042-1.png)
Figure 42: Hierarchical regularity learning for self-programming.

Figure 42: 1) The time line at the bottom represents the stream of interactions that occur over time as the system interacts. Symbols in this time line represent enacted interactions as in the bottom line of Figure 32. 2) The agent finds episodes of interest made of sequences of interactions. The symbols above Line 1 represent episodes delimited by curly brackets. These episodes are learned hierarchically in a bottom-up way; higher-level episodes are made of sequences of lower-level episodes. 3) At a certain level of abstraction (white vertical half-circle), the current sequence of episodes matches previously learned sequences and re-activates them. 4) Re-activated sequences propose subsequent episodes. These are, thus, the episodes that are afforded by the current context. 5) Afforded episodes are categorized as experiments (gray symbols). These experiments are proposed for selection. 6) The agent chooses an experiment from amongst the proposed experiments (gray arrow). 7) The agent tries to enact the sequence of primitive interactions that correspond to the chosen experiment. The success or failure of this tentative enaction depends on the environment. If the activated sequences do indeed represent a regularity of interaction, then it is probable that the tentative enaction will succeed (white arrow). However, it is not certain.

The self-programming effect occurs when the chosen experiment corresponds to a composite interaction. In this case, the decision engages the agent into executing several steps of interaction.

Self-programming results in a bottom-up automatization of behaviors so that the agent constructs increasingly abstract behaviors and delegates the control of their enaction to an automatic subsystem of its cognitive system. The agent can focus on the abstract behavior (Decision Time arrow in Figure 42) which helps it to deal with the complexity of its environment and recursively construct even higher levels of abstraction.

The agent represents its current context in terms of previously learned abstract episodes of interaction. This amounts to modeling the environment in terms of abstract affordances, as, for example, Gibson suggests in his [theory of affordances](http://en.wikipedia.org/wiki/Affordance).

This learning process is incremental and open-ended; it only stops when it runs out of memory for recording new sequences. Memory could be optimized, for example, by deleting (forgetting) sequences that have not been used for a while, but we did not implement this for the sake of simplicity. More fundamentally, regularities should be used to construct a coherent model of the world; we will examine this issue further in Lesson 6.

## Architecture of a recursive self-programming agent

By building upon the regularity learning algorithm presented on Page 33, Table 43 presents our first algorithm for self-programming agents.

```
Table 43: Algorithm of a recursive self-programming agent.

001   createPrimitiveInteraction(e1, r1, -1)
002   createPrimitiveInteraction(e1, r2, 1)
003   createPrimitiveInteraction(e2, r1, -1)
004   createPrimitiveInteraction(e2, r2, 1)
005   while()
006      anticipations = anticipate()
007      experiment = selectExperiment(anticipations)
008      intendedInteraction = experiment.intendedInteraction()

009      enactedInteraction = enact(intendedInteraction)

010      learn()
011      if (enactedInteraction.valence ≥ 0)
012         mood = PLEASED
013      else
014         mood = PAINED

101   function enact(intendedInteraction)
102      if intendedInteraction.isPrimitive 
103         experiment = intendedInteraction.experiment
104         result = environment.getResult(experiment)
105         return primitiveInteraction(experiment, result)
106      else
107         enactedPreInteraction = enact(intendedInteraction.preInteraction)
108         if (enactedPreInteraction ≠ intendedInteraction.preInteraction)
109            return enactedPreInteraction
110         else
111            enactedPostInteraction = enact(intendedInteraction.postInteraction)
112            return getOrLearn(enactedPreInteraction, enactedPostInteraction)

201   function environment.getResult(experiment)
202      if penultimateExperiment ≠ experiment and previousExperiment = experiment
203         return r2
204      else
205         return r1
```

Table 43, lines 001-014: The main loop of the algorithm is very similar to that on Page 33. 001-004: Initialization of the primitive interactions. 006: Get the list of anticipations. Now, the `anticipate()` function also returns anticipations for abstract experiments corresponding to enacting composite interactions. 007: Choose the next experiment from among primitive and abstract experiments in the list of anticipations. 008: Get the intended primitive or composite interaction from the selected primitive or abstract experiment.

Line 009: The enaction of the intended interaction is now delegated to the recursive function `enact(intendedInteraction)`. The intended interaction constitutes the learned program that the agent intends to execute, and the `enact()` function implements the engine that executes it. Let us emphasize the fact that the agent now chooses an _interaction to enact_ rather than an _experiment to perform_ (as it was the case on Page 33). In return, the agent receives an _enacted interaction_ rather than a result. This design choice follows from constructivist epistemology which suggests that sensorimotor patterns of interaction constitute the basic elements from which the agent constructs knowledge of the world.

Line 010: Learns composite interactions and abstract experiments from the experience gained from enacting the enacted interaction. The `learn()` function will be further explained in the subseqeunt construction section. Lines 011 to 014: like before, specify that the agent is pleased if the enacted interaction's valence is positive, and pained otherwise. The valence of a composite interaction is equal to the sum of the valences of its primitive interactions, meaning that enacting a full sequence of interactions has the same motivational valence as enacting all its elements successively.

Lines 101-116: The function `enact(intendedInteraction)` is used recursively to control the enaction of the intended interaction all the way down to the primitive interactions. It returns the enacted interaction. 102-105: If the intended interaction is primitive then it is enacted in the environment. 103: Specifies that the experiment is the intended primitive interaction's experiment. 104: The environment returns the result of this experiment. 105: The function returns the enacted primitive interaction made from the experiment and its result. 106-112: Control the enaction of a composite intended interaction. Enacting a composite interaction consists of successively enacting its pre-interaction and its post-interaction. 107: Call itself to enact the pre-interaction. 108-109: If the enacted pre-interaction differs from the intended pre-interaction the `enact()` function is interrupted and returns the enacted pre-interaction. 110-111: if the enaction of the pre-interaction was a success, then the `enact()` function proceeds to the enaction of the post-interaction. 112 The function returns the enacted composite interaction made of the enacted pre-interaction and of the enacted post-interaction.

Lines 201-205 implement the environment. This environment is the simplest we could imagine that requires the agent to program itself if it wants to be `PLEASED`. The result r2 occurs when the current experiment equals the previous experiment but differs from the penultimate experiment, and r1 otherwise. Since r2 is the only result that produces interactions that have a positive valence, and since the agent can at best obtain r2 every second step, it must learn to alternate between two e1 and two e2 experiments: `e1→r1` `e1→r2` `e2→r1` `e2→r2`, etc. The agent must not base its decision on the anticipation of what it can get in the next step, but on the anticipation of what it can get in the next two steps.

Figure 43 illustrates the architecture of this algorithm.

![Figure-43](/images/043-1.png)
Figure 43: Self-programming agent architecture.

The whole program is called _Existence_. The lower dashed line (Line 2) separates the part of existence corresponding to the agent (above Line 2) from the part corresponding to the environment (below Line 2, implemented by the function `environment.GetResult(experiment))`. The upper dashed line (Line 1) separates the proactive part of existence (above Line 1) from the reactive part of existence (below Line 1). The program that implements the proactive part is called the _Decider_; you can consider this as the "cognitive part" of the agent. It corresponds to the main loop in Table 43: lines 005 to 014. The reactive part of the existence includes both the _Enacter_ (above Line 2) and the environment (below Line 2). The enacter (function `enact()`) controls the enaction of the intended interaction by sending a sequence of experiments to the environment. The enacter receives a result after each experiment. After the last experiment, the enacter returns the enacted composite interaction to the decider.

As the agent develops, it constructs abstract possibilities of interaction (composite interactions) that it can enact with reference to the reactive part. From the agent's cognitive point of view (the proactive part), the reactive part appears as an _abstract environment_ -- abstracted away from the real environment by the agent itself. Line 1 represents what we call the _cognitive coupling_ between the agent and its environment.

In Lessons 5 and 6, we will discuss the question of increasing the complexity associated with these different levels of coupling.

## The construction of reality in the developmental agent

## Anticipating the effects of composite interactions

As showed in the algorithm in Table 33-3, the `anticipate()` function returns a list of _anticipations_. Each _anticipation_ corresponds to an _experiment_ associated with a _proclivity_ value for performing this experiment. The proclivity is computed on the basis of the possible interactions that may actually be enacted as an effect of performing this experiment, as far as the system can tell from its experience.

For the `anticipate()` function to work similarly with composite interactions as it does with primitive interactions, composite interactions must also be associated with experiments. In fact, the system must learn that a composite interaction corresponds to an abstract experiment performed with reference to an _abstract environment_ (the _reactive part_) that returns an _abstract result_.

See how this problem fits nicely with the constructivist learning challenge (introduced in the readings on Page 36): learning to interpret sensorimotor interactions as consisting of performing experiments on an external reality, and to interpret the results of these experiments as information about that reality.

The rest of this page presents the first-step towards addressing this challenge. We will develop this question further in the next lesson.

## Recursively learning composite interactions

Figure 44 illustrates the implementation of the `learnCompositeInteraction()` function so as to implement recursive learning of a hierarchy of composite interactions.

![Figure-44](/images/044-1.png)
Figure 44: Recursive learning of composite interactions.

Figure 44 distinguishes between the _Interaction Time_ (arrow at the bottom corresponding to the agent/environment coupling) and the _Decision Time_ (staircase shaped arrow corresponding to the proactive/reactive coupling that rises over time). The learning occurs at the level of the Decision Time to learn higher-level composite interactions on top of enacted composite interactions. In grey rectangles indicate the composite interactions that are learned or reinforced at the end of decision cycle $t_d$. The system learns the composite interaction ⟨ $e_{cd-1}$, $e_{cd}$⟩ made of the sequence of the previous enacted composite interaction $e_cd-1$ and the last enacted composite interaction $e_{cd}$. This is similar to Page 32 except that the learning can apply to composite interactions rather than primitive interactions only. Additionally, the system learns the composite interaction ⟨$e_{cd-2}$,$⟨e_{cd-1}$,$e_{cd}$⟩⟩. This way, if $e_{cd-2}$ is enacted again, ⟨$e_cd-2,⟨e_cd-1,e_cd⟩$⟩ will be re-activated and will propose to enact its post-interaction ⟨ecd-1,ecd⟩. The system has thus learned to re-enact ⟨ecd-1,ecd⟩ as a sequence, hence the self-programming effect. The higher-level composite interaction ⟨⟨ecd-2,ecd-1⟩,ecd⟩ is also learned so that it can be re-activated in the context when ⟨ecd-2,ecd-1⟩ is enacted again, and propose its post-interaction ecd.

## Associating abstract experiments and results with composite interactions

When a new composite interaction ic is added to the set Id of known interactions at time td, a new abstract experiment ea is added to the set Ed of known experiments at time td, and a new abstract result ra is added to the set Rd of known results at time td, such that ic = ⟨ea,ra⟩.

Abstract experiments are called abstract because the environment cannot process them directly. The environment (or robot's interface) is only programmed to interpret a predefined set of experiments that we now call concrete. To perform an abstract experiment ea, the agent must perform a series of concrete experiments and check their results. That is, the agent must try to enact the composite interaction ic from which the abstract experiment ea was constructed.

If the `chooseExperiment()` function chooses experiment ea, then the system tries to enact ic. If this tentative enaction fails and results in the enacted composite interaction ec ∈ Id+1, then the system creates the abstract result rf ∈ Rd+1, so that ec = ⟨ea,rf⟩ .

The next time the system considers choosing experiment ea, it will compute the proclivity for ea based on the anticipation of succeeding enacting ic and getting result ra, balanced with the anticipation of actually enacting ec and getting result rf.

As a result of this mechanism, composite interactions can have two forms: the sequential form ⟨pre-interaction,post-interaction⟩ and the abstract form ⟨experiment,result⟩. We differentiate between these two forms by noting abstract experiments and results in initial caps separated by the "|" symbol: ⟨EXPERIMENT|RESULT⟩. We will use this notation in the trace in Page 46.

This mechanism is a critical step to implementing self-programming agents. Nevertheless, it opens many questions that remain to be addressed. For example, how to organize experiments and results to construct a coherent model of reality?

## Implementing a self-programming agent
If you have no interest in programming then you can skip this page and proceed to the next page.

Project 4 implements the algorithm described on Page 43 and Page 44 to let you run your first self-programming agent.

```
main / Program ← Edit Main.java to instantiate Existence040.
existence / Existence040
coupling / Experiment040 ← Now, experiments have an intended interaction.
coupling / interaction / Interaction040 ← Now, interactions have an experiment to perform.
```

For Lesson 4, your programming activities are:

1. Change `Program.cs` to instantiate `Existence040` and run it. Make sure that you obtain the trace shown in the next page
2. Change `Existance040` to instantiate `Environment010` and then `Environment030` instead of `Environment040` and run it. Observe that the modified `Existence040` can still learn to be pleased.

Lesson 4 shows that `Existence040` can adapt to three different environments (`Environment010`, `Environment030`, and `Environment040`). In fact, it can adapt to environments that require enacting sequences consisting of two steps or less (first-level composite interactions).

However, `Existance040` makes bad decisions in environments that require longer sequences. This is because the algorithm does not yet process abstract experiments in the exact same way as primitive experiments, which stops it from being fully recursive. We will address this problem in the next lesson with Radical Interactionism, which removes the notion of primitive experiments and considers all experiments as abstract.

## Behavioral analysis of a self-programming agent

Table 46 shows the trace that you should see in your console if you ran Project 4. If you did not run it, you can refer to pages 44 and 45 to understand this trace. Observe that the system learns to be always PLEASED from Decision 23 (on Line 197) by alternatively enacting the composite interactions ⟨e2r1e2r2⟩ and ⟨e1r1e1r2⟩.

```
Table 46: Activity trace of a rudimentary self-programming agent.

001    propose e1 proclivity 0
002    propose e2 proclivity 0
003    Enacted e1r1 valence -1 weight 0
004    0: PAINED
005    propose e1 proclivity 0
006    propose e2 proclivity 0
007    Enacted e1r2 valence 1 weight 0
008    learn <e1r1e1r2> valence 0 weight 1
009    1: PLEASED
010    propose e1 proclivity 0
011    propose e2 proclivity 0
012    Enacted e1r1 valence -1 weight 0
013    learn <e1r2e1r1> valence 0 weight 1
014    learn <e1r1<e1r2e1r1>> valence -1 weight 1
015    learn <<e1r1e1r2>e1r1> valence -1 weight 1
016    2: PAINED
017    propose e1 proclivity 1
018    propose e2 proclivity 0
019    propose <E1R2E1R1| proclivity 0
020    Enacted e1r1 valence -1 weight 0
021    learn <e1r1e1r1> valence -2 weight 1
022    learn <e1r2<e1r1e1r1>> valence -1 weight 1
023    learn <<e1r2e1r1>e1r1> valence -1 weight 1
024    3: PAINED
025    propose e1 proclivity 0
026    propose e2 proclivity 0
027    propose <E1R2E1R1| proclivity 0
028    Enacted e1r1 valence -1 weight 0
029    reinforce <e1r1e1r1> valence -2 weight 2
030    learn <e1r1<e1r1e1r1>> valence -3 weight 1
031    learn <<e1r1e1r1>e1r1> valence -3 weight 1
032    4: PAINED
033    propose e2 proclivity 0
034    propose <E1R2E1R1| proclivity 0
035    propose e1 proclivity -2
036    propose <E1R1E1R1| proclivity -2
037    Enacted e2r1 valence -1 weight 0
038    learn <e1r1e2r1> valence -2 weight 1
039    learn <e1r1<e1r1e2r1>> valence -3 weight 1
040    learn <<e1r1e1r1>e2r1> valence -3 weight 1
041    5: PAINED
042    propose e1 proclivity 0
043    propose e2 proclivity 0
044    Enacted e1r1 valence -1 weight 0
045    learn <e2r1e1r1> valence -2 weight 1
046    learn <e1r1<e2r1e1r1>> valence -3 weight 1
047    learn <<e1r1e2r1>e1r1> valence -3 weight 1
048    6: PAINED
049    propose <E1R2E1R1| proclivity 0
050    propose e1 proclivity -1
051    propose e2 proclivity -1
052    propose <E2R1E1R1| proclivity -2
053    propose <E1R1E1R1| proclivity -2
054    propose <E1R1E2R1| proclivity -2
055    Enacted <e1r2e1r1> valence 0 weight 1
056    reinforce <e1r1<e1r2e1r1>> valence -1 weight 2
057    7: PLEASED
058    propose <E1R2E1R1| proclivity 0
059    propose e2 proclivity -1
060    propose e1 proclivity -2
061    propose <E2R1E1R1| proclivity -2
062    propose <E1R1E1R1| proclivity -2
063    propose <E1R1E2R1| proclivity -2
064    Enacted e1r1 valence -1 weight 0
065    learn <<e1r2e1r1><E1R2E1R1|E1R1>> valence -1 weight 1
066    8: PAINED
067    propose e1 proclivity 0
068    propose e2 proclivity 0
069    Enacted e1r1 valence -1 weight 0
070    learn <<E1R2E1R1|E1R1>e1r1> valence -2 weight 1
071    learn <<e1r2e1r1><<E1R2E1R1|E1R1>e1r1>> valence -2 weight 1
072    learn <<<e1r2e1r1><E1R2E1R1|E1R1>>e1r1> valence -2 weight 1
073    9: PAINED
074    propose <E1R2E1R1| proclivity 0
075    propose e1 proclivity -1
076    propose e2 proclivity -1
077    propose <E2R1E1R1| proclivity -2
078    propose <E1R1E1R1| proclivity -2
079    propose <E1R1E2R1| proclivity -2
080    Enacted e1r1 valence -1 weight 0
081    learn <e1r1<E1R2E1R1|E1R1>> valence -2 weight 1
082    learn <<E1R2E1R1|E1R1><e1r1<E1R2E1R1|E1R1>>> valence -3 weight 1
083    learn <<<E1R2E1R1|E1R1>e1r1><E1R2E1R1|E1R1>> valence -3 weight 1
084    10: PAINED
085    propose e2 proclivity 0
086    propose e1 proclivity -1
087    propose <E1R1<E1R2E1R1|E1R1|| proclivity -2
088    Enacted e2r1 valence -1 weight 0
089    learn <<E1R2E1R1|E1R1>e2r1> valence -2 weight 1
090    learn <e1r1<<E1R2E1R1|E1R1>e2r1>> valence -3 weight 1
091    learn <<e1r1<E1R2E1R1|E1R1>>e2r1> valence -3 weight 1
092    11: PAINED
093    propose e2 proclivity 0
094    propose e1 proclivity -1
095    Enacted e2r2 valence 1 weight 0
096    learn <e2r1e2r2> valence 0 weight 1
097    learn <<E1R2E1R1|E1R1><e2r1e2r2>> valence -1 weight 1
098    learn <<<E1R2E1R1|E1R1>e2r1>e2r2> valence -1 weight 1
099    12: PLEASED
100    propose e1 proclivity 0
101    propose e2 proclivity 0
102    Enacted e1r1 valence -1 weight 0
103    learn <e2r2e1r1> valence 0 weight 1
104    learn <e2r1<e2r2e1r1>> valence -1 weight 1
105    learn <<e2r1e2r2>e1r1> valence -1 weight 1
106    13: PAINED
107    propose e1 proclivity -1
108    propose e2 proclivity -1
109    propose <E1R2E1R1| proclivity -1
110    propose <<E1R2E1R1|E1R1|E2R1| proclivity -2
111    propose <E2R1E1R1| proclivity -2
112    propose <E1R1E1R1| proclivity -2
113    propose <E1R1E2R1| proclivity -2
114    Enacted e1r2 valence 1 weight 0
115    reinforce <e1r1e1r2> valence 0 weight 2
116    learn <e2r2<e1r1e1r2>> valence 1 weight 1
117    learn <<e2r2e1r1>e1r2> valence 1 weight 1
118    14: PLEASED
119    propose e2 proclivity 0
120    propose e1 proclivity -2
121    propose <E1R1E1R1| proclivity -2
122    Enacted e2r1 valence -1 weight 0
123    learn <e1r2e2r1> valence 0 weight 1
124    learn <e1r1<e1r2e2r1>> valence -1 weight 1
125    learn <<e1r1e1r2>e2r1> valence -1 weight 1
126    15: PAINED
127    propose e2 proclivity 1
128    propose <E2R2E1R1| proclivity 0
129    propose e1 proclivity -1
130    Enacted e2r2 valence 1 weight 0
131    reinforce <e2r1e2r2> valence 0 weight 2
132    learn <e1r2<e2r1e2r2>> valence 1 weight 1
133    learn <<e1r2e2r1>e2r2> valence 1 weight 1
134    16: PLEASED
135    propose e2 proclivity 0
136    propose <E1R1E1R2| proclivity 0
137    propose e1 proclivity -2
138    Enacted e2r1 valence -1 weight 0
139    learn <e2r2e2r1> valence 0 weight 1
140    learn <e2r1<e2r2e2r1>> valence -1 weight 1
141    learn <<e2r1e2r2>e2r1> valence -1 weight 1
142    17: PAINED
143    propose e2 proclivity 2
144    propose <E2R2E2R1| proclivity 0
145    propose <E2R2E1R1| proclivity 0
146    propose e1 proclivity -1
147    Enacted e2r1 valence -1 weight 0
148    learn <e2r1e2r1> valence -2 weight 1
149    learn <e2r2<e2r1e2r1>> valence -1 weight 1
150    learn <<e2r2e2r1>e2r1> valence -1 weight 1
151    18: PAINED
152    propose e2 proclivity 1
153    propose <E2R2E2R1| proclivity 0
154    propose <E2R2E1R1| proclivity 0
155    propose e1 proclivity -1
156    Enacted e2r1 valence -1 weight 0
157    reinforce <e2r1e2r1> valence -2 weight 2
158    learn <e2r1<e2r1e2r1>> valence -3 weight 1
159    learn <<e2r1e2r1>e2r1> valence -3 weight 1
160    19: PAINED
161    propose <E2R2E2R1| proclivity 0
162    propose <E2R2E1R1| proclivity 0
163    propose e1 proclivity -1
164    propose e2 proclivity -1
165    propose <E2R1E2R1| proclivity -2
166    Enacted e2r1 valence -1 weight 0
167    learn <e2r1<E2R2E2R1|E2R1>> valence -2 weight 1
168    learn <e2r1<e2r1<E2R2E2R1|E2R1>>> valence -3 weight 1
169    learn <<e2r1e2r1><E2R2E2R1|E2R1>> valence -3 weight 1
170    20: PAINED
171    propose e1 proclivity 0
172    propose e2 proclivity 0
173    Enacted e1r1 valence -1 weight 0
174    learn <<E2R2E2R1|E2R1>e1r1> valence -2 weight 1
175    learn <e2r1<<E2R2E2R1|E2R1>e1r1>> valence -3 weight 1
176    learn <<e2r1<E2R2E2R1|E2R1>>e1r1> valence -3 weight 1
177    21: PAINED
178    propose e1 proclivity 0
179    propose <E1R2E2R1| proclivity 0
180    propose e2 proclivity -1
181    propose <E1R2E1R1| proclivity -1
182    propose <E2R1E1R1| proclivity -2
183    propose <<E1R2E1R1|E1R1|E2R1| proclivity -2
184    propose <E1R1E2R1| proclivity -2
185    propose <E1R1E1R1| proclivity -2
186    Enacted e1r2 valence 1 weight 0
187    reinforce <e1r1e1r2> valence 0 weight 3
188    learn <<E2R2E2R1|E2R1><e1r1e1r2>> valence -1 weight 1
189    learn <<<E2R2E2R1|E2R1>e1r1>e1r2> valence -1 weight 1
190    22: PLEASED
191    propose <E2R1E2R2| proclivity 0
192    propose e1 proclivity -2
193    propose e2 proclivity -2
194    propose <E1R1E1R1| proclivity -2
195    Enacted <e2r1e2r2> valence 0 weight 2
196    reinforce <e1r2<e2r1e2r2>> valence 1 weight 2
197    23: PLEASED
198    propose <E1R1E1R2| proclivity 0
199    propose e1 proclivity -2
200    propose e2 proclivity -2
201    propose <E2R1E2R1| proclivity -2
202    Enacted <e1r1e1r2> valence 0 weight 3
203    learn <<e2r1e2r2><e1r1e1r2>> valence 0 weight 1
204    24: PLEASED
205    propose <E2R1E2R2| proclivity 0
206    propose e1 proclivity -2
207    propose e2 proclivity -2
208    propose <E1R1E1R1| proclivity -2
209    Enacted <e2r1e2r2> valence 0 weight 2
210    learn <<e1r1e1r2><e2r1e2r2>> valence 0 weight 1
211    25: PLEASED

```
Lines 1-2: predefined experiments are proposed with a default proclivity of `0`. Proposals are sorted by decreasing proclivity. The first proposed experiment is selected: e1. Lines 3-4: Decision 0: The interaction `e1r1` is enacted. The system is PAINED because this interaction has a negative valence (`-1`).

Line 8: the first composite interaction ⟨`e1r1e1r2`⟩ is learned from the primitive interaction e1r1 enacted on Decision 0 (Line 3) and the primitive interaction `e1r2` enacted on Decision 1 (Line 7). Simultaneously, the system records an abstract experiment noted ⟨E1R1E1R2| (not in the trace). This experiment will be proposed for the first time on Decision 16 (Line 136) but not selected. It is proposed again on Decision 24 (Line 198) and selected, resulting in the successful enaction of composite interaction ⟨e1r1e1r2⟩ (Line 202).

On Decision 8, the experiment ⟨E1R2E1R1| was selected (Line 58), leading to the tentative enaction of composite interaction ⟨`e1r2e1r1`⟩. This tentative enaction failed due to obtaining result `r1` instead of the expected result `r2`, thus resulting in the enaction of primitive interaction `e1r1` instead (Line 64). The abstract result |E1R1⟩ is created, as well as the enacted interaction ⟨E1R2E1R1|E1R1⟩, using the notation introduced on Page 44.

## Selected readings on self-programming

* The best reference I know to explain why self-programming is a prerequisite for autonomous sense-making is Froese & Ziemke (2009). Enactive artificial intelligence: Investigating the systemic organization of life and mind. Journal of Artificial Intelligence, 173(3-4): 466-500. This article invokes the concept of constitutive autonomy, which, I argue, can be achieved through self-programming.
* Georgeon & Marshall (2013). Demonstrating sensemaking emergence in artificial agents: A method and an example. International Journal of Machine Consciousness, 5(2), pp 131-144.
* An issue of AGI devoted to self-programming. Great reference to support self-programming! Thórisson, Nivel, Sanz, & Wang (2013). Approaches and Assumptions of Self-Programming in Achieving Artificial General Intelligence. Journal of Artificial General Intelligence. 3(3), 1-10.

This ends Lesson 4 (it was the hardest!).
