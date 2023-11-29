# ideal
An ideal algorithm for control in the manner of Cybernetics. This branch contains details of interactional motivation.

## Code module

This branch contains the module 020 that expands upon the code to create continuity from the previous branch.

# Section 2: An introduction to the sensorimotor paradigm

The key concept to convey in Section 2 is:

`Focus on sensorimotor interactions rather than separating perception from action.`

In Section 1, we saw that the agent's input data should not be confused with the agent's perception. Now, we need to better understand how exactly we should consider the input data. If input data is not perception, then what is it?

The sensorimotor paradigm suggests that input data should be taken in association with output data, by combining both of them into a single entity called a sensorimotor interaction. With the formalism introduced in §1.2, this gives $i = ⟨e,r⟩$: An interaction $i$ is a tuple `⟨experiment, result⟩`.

In his theory of mental development, Jean Piaget coined the term sensorimotor scheme to refer to a pattern of interaction between the agent and its environment. In this model, an interaction is a chunk of data that represents a primitive sensorimotor scheme.

From now on, we use the expression _to enact an interaction_ to refer to performing the experiment and receiving the result that compose a given interaction. The expression _to intend to enact_ interaction $⟨e,r⟩$ means that the agent performs experiment $e$ while expecting result $r$. As a result of this intention, the agent may _actually enact_ interaction $⟨e,r'⟩$ if it receives result $r'$ instead of $r$.

## 2.2: Interactional motivation in agents and robots

The sensorimotor paradigm allows implementing a type of motivation called _interactional motivation_. Using the embodied model introduced on §1.2, Figure 2.2 compares traditional reinforcement learning (left) with interactional motivation (right).

![Figure-22](/images/022-1.png)
Figure 2.2: Interactional motivation (right) compared to traditional reinforcement learning (left).

In reinforcement learning (Figure 2.2/left), the agent receives a reward $r$ that specifies desirable goals to reach. In the case of simulated environments, the designer programs the reward function $r(s)$ as a function of the environment's state. In the case of robots and automata, a "reward button" is pressed either automatically when the goal is reached, or manually by the experimenter to train the automata to reach the goal. The agent's policy is designed to choose actions based on their estimated utility for getting the reward. As a result, to an observer of the agent's behavior, the agent appears motivated to reach the goal defined by the experimenter. For example, to model an agent that seeks food, the designer assigns a positive reward to states of the world in which the agent reaches food.

In interactional motivation (Figure 2.2/right), the agent has no predefined goal to reach. The environment may even lack a state, as discussed in §1.3 and revisted in the next section. However, the agent has predefined interactions that it can enact. The designer may associate a scalar valence $v(i)$ with interactions and design the agent's policy to try to enact interactions that have a positive valence. To model an agent that seeks food, the designer specifies an interaction corresponding to eating -- the experiment of biting with the result of tasting good food -- and assigns a positive valence to this interaction. In doing so, the designer predefines the agent's preferences of interaction without predefining which states or entities of the world constitute food.

Overall, the sensorimotor paradigm allows designing self-motivated agents without modeling the world as a predefined set of states. Instead, the agent is left alone to construct its own model of the world through its individual experience of interaction. Since there is no predefined model of the world, the agent is not bound to a predefined set of goals. For example, it can discover/categorize new edible entities in the world.

Interactional motivation is not the only possible motivational drive for sensorimotor agents. Recall that we introduced the drive to learn to predict the result of experiments in §1.4. Importantly, since there is no data in a sensorimotor agent that directly represents the environment's state, a sensorimotor agent can hardly be pre-programmed to perform a predefined task. If you want them to perform a predefined task, you will have to train them rather than program them.

Since sensorimotor agents are not programmed to seek predefined goals, we do not assess their learning by measuring their performance in reaching predefined goals, but by demonstrating the emergence of cognitive behaviors through behavioral analysis. We did our first behavior analysis on §1.4, and we will continue doing behavioral analysis subsequently.

## 2.3: Implementation of interactional motivation

Interactional motivation is compatible with the motivation to predict results introduced in §1.3. We can imagine agents implemented with the two motivational principles concurrently. For simplicity, however, the algorithm in Table 2.3 only implements interactional motivation.

```
Table 2.3: Algorithm of a rudimentary interactionally motivated system.

01   createPrimitiveInteraction(e1, r1, -1)
02   createPrimitiveInteraction(e2, r2, 1)
03   experiment = e1
04   While()
05      if (mood = PAINED)
06         experiment = getOtherExperiment(experiment) 
07      if (experiment = e1)
08         result = r1
09      else
10         result = r2        
11      enactedInteraction = getInteraction(experiment, result)
12      if (enactedInteraction.valence ≥ 0)
13         mood = PLEASED
14      else
15         mood = PAINED
16      print experiment, result, mood
```

Table 2.3, line 01: Interactions `⟨e1,r1⟩` is stored in memory and set with a negative valence `(-1)`. 02: Interaction `⟨e2,r2⟩` is stored in memory and set with a positive valence `(1)`. 04: If the agent is pained then it picks another experiment. Lines 07 to 10 implement the same environment as in §1.3. 11: Retrieves the enacted interaction from the experiment and the result 12 to 15: if the enacted interaction has a positive valence then the agent is `PLEASED`, otherwise it is `PAINED`.

Notably, if more interactions were specified, the agent would stick to the first interaction it tried that has a positive valence. If all the interactions had a negative valence, the agent would keep trying other interactions. This behavior is still very primitive. In the following Sections, we will address the problem of "scaling up" towards more intelligent behaviors in more complex environments.

If you have no interest in programming, then you can skip the rest of this section and proceed to the next.

If you are doing the programming activities, then begin with running Existence020.cs (below), which implements the algorithm in Table 23, and observe that you obtain the trace shown in Table 2.4.

Project 2 [sensimotor branch] (files to modify or to add to Project 1):

```
Program.cs ← Uncomment the instruction to run Existence020 instead of Existence010.
existence / Existence020.cs ← Overrides Existence010 and implements the algorithm in Table 2.3.
coupling / interaction / Interaction020.cs ← Overrides Interaction010 and adds a valence attribute.
```

Additionally, your programming activity is to modify Existence020.cs so that the agent implements self-satisfaction, as illustrated in §1.3, in addition to interactional motivation.

You can observe in the trace that, as long as the agent is not `BORED`, it chooses experiments that result in interactions that have a positive valence, and when it gets bored, it chooses another experiment even though it knows that this experiment may result in a painful interaction.

You will find that there is not a unique solution to associate these two motivational principles. For example, you will have to choose where the boredom comes from; it may come from enacting always the same interaction or simply from always being pleased. It is likely that different developers will implement different agents that generate different behaviors, and this is perfectly fine. You can just follow the _ideal guideline_: Implement the simplest you can.

If you are re-implementing this project in a different language, it is not necessary to reproduce the hierarchy of classes. You can simply modify your previous version to integrate the changes shown in Project 2. The hierarchy of classes has only been done for pedagogical purpose, to highlight the changes from one version to the next. This incremental approach will be followed throughout the next sections.

## 2.4: Behavioral analysis of an interactionally motivated agent

Table 2.4 shows the trace you should see in the console if you ran Existence020. If you did not run it, you can refer to the pseudocode presented in Table 2.3 to understand this trace.

```
Table 2.4: Activity trace of a rudimentary interactionally motivated agent.

0: e1r1 PAINED
1: e2r2 PLEASED
2: e2r2 PLEASED
3: e2r2 PLEASED
4: e2r2 PLEASED
5: e2r2 PLEASED
6: e2r2 PLEASED
```

For Section 2, your activity is to understand the trace listed in Table 2.4. Why is the agent `PAINED` on Cycle 0? What does the agent do on Cycle 1? What will be the agent's mood after Cycle 6? If we initialize the interaction `e1r1` with a positive valence and re-run the algorithm, what behavior do we obtain?

## 2.5: Selected readings about the sensorimotor paradigm

For more information about the sensorimotor paradigm, here is a short list of selected readings:

* The wikipedia article on Piaget's theory of cognitive development. Jean Piaget coined the term sensorimotor scheme to refer to an atomic pattern of interaction between a developing infant and the world. His work on developmental psychology and constructivist epistemology is often considered foundational to the sensorimotor approach to artificial intelligence.
* A short paper in which we present interactional motivation: Olivier L. Georgeon, James B. Marshall, and Simon L. Gay (2012). Interactional Motivation in Artificial Systems: Between Extrinsic and Intrinsic Motivation. The 2nd Joint IEEE International Conference on Development and Learning and on Epigenetic Robotics (EPIROB 2012), San Diego, pp. 1-2.
* A famous paper that argues that perceiving the world is to "master the laws of sensorimotor contigencies": O'Regan, K. & Noe, A. (2001). A sensorimotor account of vision and visual consciousness. Behavioral and Brain Sciences, 24(5), 939-1031.

This ends Section 2.