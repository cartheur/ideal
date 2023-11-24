# ideal

_I_mplementation of _DE_velopment_A_l _L_earning | An _ideal_ algorithm for control in the manner of Cybernetics. This branch contains the foundational module.

Be forwarned that it is not _exactly_ like reinforcement learning, a shade that yields significantly different effects.

## Code module

Below you will find a description of the code base where and how to begin programming the _ideal_ algorithm in its entirety by building intellectual concepts of how and _why_ it works as it does.

# Section 1: An introduction to the embodied paradigm

The primary "take home" message that establishes the paradigm is:

`Do not consider the agent's input data as the agent's perception of its environment.`

The agent is not a passive observer of reality, but rather constructs a perception of reality through active interaction. The term embodied means that the agent must be a part of reality for this active interaction to happen.

Those of you who have a background in cognitive science or psychology are probably already familiar with this idea theoretically. In Lesson 1, however, we wish to introduce how this idea translates into the practical design of artificial agents and robots.

## Agent and robot design according to the embodied paradigm

The embodied paradigm suggests shifting perspective from:
- the traditional view in which the agent interprets input data as if it represented the environment (Figure 12/left),
to:
- the embodied view in which the agent constructs a perception of the environment through the active experience of interaction (Figure 12/right).

![Figure-12](/images/012-1.png)
Figure 12: Embodied model (right) compared to the traditional model (left). In the traditional model, the cycle conceptually starts with observing the environment (black circle on the environment) and ends by acting on the environment (black arrow on the environment). In the embodied model, the cycle conceptually starts with the agent performing an experiment (black circle on the agent), and ends by the agent receiving the result of the experiment (black arrow on the agent).

Most representations of the cycle agent/environment do not make explicit the conceptual starting point and end point of the cycle. Since the cycle revolves indefinitely, why should we care anyway?

We should care because, depending on the conceptual starting and end points, we design the agent's algorithm, the robot's sensors, or the simulated environment differently.

In the traditional view, we design the agent's input (called observation o in Figure 12/left) as if it represented the environment's state. In the case of simulated environments, we implement o as a function of s, where s is the state of the environment (o = f(s) in Figure 12/left). In the case of robots, we process the sensor data as if it represented the state of the real world, even though this state is not accessible. This is precisely what the embodied paradigm suggests to avoid because it amounts to considering the agent's input as a representation of the world.

In the embodied view, we design the agent's input (called result r in Figure 12/right) as a result of an experiment initiated by the agent. In simulated environments, we implement r as a function of the experiment and of the state (r = f (e,s) in Figure 12/right). In a given state of the environment, the result may vary according to the experiment. We may even implement environments that have no state, as we do in the next page. When designing robots, we process the sensor data as representing the result of an experiment initiated by the robot.

## Agent implementation according to the embodied paradigm

Table 13 presents the algorithm of a rudimentary embodied system.

```
Table 13: algorithm of a rudimentary embodied system.

01   experiment = e1
02   Loop(cycle++)
03      if (mood = BORED)
04         selfSatisfiedDuration = 0
05         experiment = pickOtherExperiment(experiment)
06      anticipatedResult = anticipate(experiment)
07      if (experiment = e1)
08         result = r1
09      else
10         result = r2
11      recordTuple(experiment, result)
12      if (result = anticipatedResult)
13         mood = SELF-SATISFIED
14         selfSatisfiedDuration++
15      else
16         mood = FRUSTRATED
17         selfSatisfiedDuration = 0
18      if (selfSatisfiedDuration > 3)
19         mood = BORED
20      print cycle, experiment, result, mood
```


Table 13, Lines 03 to 05: if the agent is bored, it picks another experiment arbitrarily from amongst the predefined list of experiments at its disposal. Line 06: the anticipate(experiment) function searches memory for a previously learned tuple that matches the chosen experiment, and returns its result as the next anticipated result. Lines 07 to 10 implement the environment: e1 always yields r1, and other experiments always yield r2. Line 11: the agent records the tuple ⟨experiment, result⟩ in memory. Lines 12 to 17: if the result was anticipated correctly then the agent is self-satisfied, otherwise it is frustrated. Lines 18 and 19: if the agent has been self-satisfied for too long (arbitrarily 3 cycles), then it becomes bored.

Notably, this system implements a single program called Existence which does not explicitly differentiate the agent from the environment. Lines 07 to 10 are considered the environment, and the other lines the agent. The environment does not have a state, as we promised in the previous page.

If you have no interest in programming, then you can skip the rest of this page and proceed to the next page.

If you an have interest in programming, but do not wish to do the optional programming activities, then we recommend you browse through Project 1 below, just to get a sense of how the code is organized.

If you wish to do the optional programming activities, then your activity for lesson 1 is to install Project 1 in your favorite development environment (any IDE, for example, we use Eclipse), and run it. You should get a trace similar to that shown on the next page. If you do not like java, well, you may reprogram it in the language of your choice.

To install Project 1, you can either:

Check out the project from the svn repository using your favorite svn tool (for example, we use subclipse in Eclipse). For now, you can ignore all the code that is not listed below, or copy/paste or download the java files from the links below.
Project 1:


```
Program.cs
existence / Existence.cs
existence / Existence010.cs ← the main program that implements the algorithm in Table 13.
coupling / Experiment.cs
coupling / Result.cs
coupling / interaction / Interaction.cs ← a tuple ‹experiment, result› is called an interaction.
coupling / interaction / Interaction010.cs
```
##Behavioral analysis of an embodied system based on its activity trace

Table 14 shows the trace that you should see in the console if you ran Project 1. If you did not run it, we suggest you review the algorithm presented on the previous page to understand the trace.


```
Table 14: activity trace of a rudimentary embodied system.

0: e1r1 FRUSTRATED
1: e1r1 SELF-SATISFIED
2: e1r1 SELF-SATISFIED
3: e1r1 SELF-SATISFIED
4: e1r1 BORED
5: e2r2 FRUSTRATED
6: e2r2 SELF-SATISFIED
7: e2r2 SELF-SATISFIED
8: e2r2 SELF-SATISFIED
9: e2r2  BORED
10: e1r1 SELF-SATISFIED
```

Your activity, for Lesson 1, is to understand the trace in Table 14. What does "e1r1" mean on Cycle 0? Why is the agent frustrated on Cycles 0 and 5? Why is it bored on Cycle 4? Why is it not frustrated on Cycle 10?

## Readings about the embodied paradigm.

For more information about the embodied paradigm, here is a short list of selected readings:

The Wikipedia article on Embodied Cognition.
* Georgeon & Cordier (2014). Inverting the interaction cycle to model embodied agents. Fifth International Conference on Biologically Inspired Cognitive Architectures (BICA2014). Boston.

We wrote this paper after this lesson; it develops the same ideas in a deeper and more academic form. It will also point you to other classical references in the domain of developmental learning.
* A book often considered as one of the founding references for embodied cognition: Varela, Thompson, and Rosch (1991). The Embodied Mind: Cognitive Science and Human Experience Cambridge, MA: The MIT Press.

In contrast, here is the famous big book that you do NOT need to read for this course:

* Russell and Norvig's Artificial Intelligence: A Modern Approach is arguably the first book everybody interested in AI should acquire for their library. However, you will not need it for this course.

You will find that it is at odds with the embodied paradigm by page iv (page 4 of the preface!) where it posits "the problem of AI is to describe and build agents that receive percepts from the environment and perform actions" (if you don't see the tension, 

This ends Lesson 1.