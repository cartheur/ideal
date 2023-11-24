# ideal
An ideal algorithm for control in the manner of Cybernetics. This branch contains details of radical interactionism.

## Code module

This branch contains the modules 050 and 051 that expand upon code from the continuity of the previous branches.

# Section 5: An introduction to radical interactionism

On Page 43, we introduced three concepts: 1) the _proactive_ and 2) the _reactive_ parts of the existence, and 3) the _cognitive coupling_ between them. The cognitive coupling works as a succession of _decision cycles_. At the beginning of each decision cycle, the proactive part decides on an intended sensorimotor interaction to try to enact with reference to the reactive part. At the end of the decision cycle, the reactive part returns the actually enacted sensorimotor interaction to the proactive part (Figure 43).

In lesson 5, we redesign the policy coupling (agent/environment) to be more consistent with the cognitive coupling. We replace the concepts of primitive _experiments_ and _results_ with the concepts of primitive _intended interactions_ and _enacted interactions_. We call this change the _Radical Interactionism_ (RI) conceptual commitment. Figure 51 compares the RI model with the embodied model introduced on Page 12 that we have been using thus far.

![Figure-51](/images/051-1.png)
Figure 51: From embodiment (left) to radical interactionism (right).

There are several differences in terminology between the embodied model (Figure 51-left) and the RI model (Figure 51-right). The agent's output data has been renamed `i` for _intended interaction_ instead of `e` for _experiment_. The agent's input data has been renamed `e` for _enacted interaction_ instead of `r` for _result_. Besides the terminology, the only formal difference is that the agent's output data and the agent's input data now belong to the same set `I` instead of two different sets `E` and `R`. This means that the primitive data of the model is only of one kind: sensorimitor interactions. This is the key concept of RI and of Lesson 5:

`Use sensorimotor interactions as primitives of the model.`

Model primitives are analogous to a mathematical system's primitives, which are used in axioms and theorems to define more complex structures, but which are themselves undefined within the system. Traditional cognitive models take _percepts_ and _actions_ as primitive notions. We, in the previous lessons, took _experiments_ and _results_ as primitive notions, and derived the notion of _sensorimotor interaction_ from them. The RI conceptual commitment implies an inversion of this reasoning, taking instead _interactions_ as the primitive notions and deriving _experiments_ and _results_ from interactions.

On Page 44, we already introduced the problem of constructing experiments and results from interactions. RI generalizes this idea by removing primitive experiments and results from the model, and considering all experiments and results as abstract. This generalization will simplify our algorithms and help us address the programming problems pending on Page 45. RI will also impact the way we design developmental robots as we are going to see on Page 53.

Figure 51-right presents the RI process in a similar way as the sensorimotor process on Page 21 and the cognitive coupling process on Page 43. At the beginning of cycle $t$, the agent chooses an intended primitive interaction it to enact on the environment. The attempt to enact it may change the environment or not. At the end of cycle $t$, the agent receives the enacted primitive interaction $e_t$. If $e_t = i_t$, then we consider the agent's enaction as a success. If $e_t ≠ i_t$, then we consider the agent's attempt to enact it as a failure.

As an example, the primitive interaction $i_t$ may correspond to actively feeling (through touching) an object in front of the agent, involving both a movement and a sensory feedback. The tentative enaction of $i_t$ may indeed result in feeling an object, in which case $e_t = i_t$. It may, however, result in feeling nothing if there is no object in front of the agent. In this case the enacted interaction $e_t$ corresponds to a different interaction: moving while feeling nothing. The agent constructs knowledge about its environment and organizes its behavior through regularities observed in the sequences of enacted interactions.

In Lesson 6, we will rely on RI to move us toward designing agents that can learn spatio-sequential regularities of interaction instead of only-sequential regularities as we have been doing thus far. We will achieve this by designing a cognitive architecture that can localize enacted interactions in space.

Overall, RI helps better understand embodiment and sensorimotor learning by placing interactions at the core of the model, as these theories recommend. This helps us address the constuctivist learning problem of constructing knowledge of reality from regularities of sensorimotor interactions.

## Recursive learning with radical interactionism

Radical interactionism models the policy coupling (i.e., the agent/environment coupling) in the same way as the cognitive coupling (i.e., the proactive/reactive coupling presented on Page 43): by using intended interactions and enacted interactions. This unification of the model helps better understand recursive self-programming. Figure 52 illustrates this recursivity.

![Figure-52](/images/052-1.png)
Figure 52: Recursive learning in radical interactionism.

At the beginning of decision cycle $t_d$ (dashed loop in Figure 52), the agent's decisional mechanism chooses the intended composite interaction $i_{cd}$ from amongst the set $C_d$ of composite interactions known at time $t_d$. The enaction of $i_{cd}$ consists of trying to enact the $k$ primitive interactions $i_{p1} ... i_{pk}$ that constitute $i_{cd} one after another (solid loops). If the enaction of $i_{pj}$ fails ($e_{pj} ≠ i_{pj}$) then the enaction of $i_{cd}$ is interrupted. The decisional mechanism then receives the actually enacted composite interaction $e_{cd}$ corresponding to the sequence $⟨e_{p1}, . . . , e_{pj}⟩, j ≤ k$. From the perspective of the decisional mechanism, $e_{cd}$ thus seems to be enacted as a single interaction in a virtual "environment known by the agent at time $t_d$" (dashed-line box). Because the primitive loop (plain line) and the decisional loop (dashed line) use the same entities (interactions), the learning mechanism that applies to the primitive loop can apply in the same way to the decisional loop, which allows recursive learning of increasingly complex composite interactions.

Because each instance of agent learns different composite interactions depending on its individual experience, the cognitive part of each agent (the "decisional mechanism" in Figure 52) develops its own individual vision of the world (the environment known at time $t_d$). As a result, over time, each instance of agent interprets the world in its own way and makes individual decisions, which leads to individual sense-making and free will. This has been specifically argued by a branch of cognitive science called theory of enaction (e.g., references in Page 47).

## A radical interactionist program

By building upon the self-programming architecture presented in Figure 43, Figure 53 presents the architecture of a developmental program based on RI.

![Figure-53](/images/053-1.png)
Figure 53: Architecture of a developmental program based on radical interactionism. The dashed arrows between the program and the physical world do not represent data transfer but physical effects.

Figure 53 modifies Figure 43 to incorporate RI: now, the coupling between the agent and the environment (Line 2) uses primitive interactions instead of experiments and results. In the case of a simulated environment, the designer designs the environment (below Line 2) to process _intended primitive interactions_ and to return _enacted primitive interactions_ as we will do in the implementation on Page 58.

If we developed an agent in a simulated environment that we then want to use to control a robot in the real world, we would retain all the program above Line 2 (since this is the agent), and modify the program below Line 2 to command the actuators and to read the sensors of the robot. The program below Line 2, thus, implements the _interface_ between the agent and the physical world. Developers may feel confused when programming the interface because they are used to thinking of the _sensor data_ as the _robot's perception_, and of the _actuator commands_ as the _robot's actions_, rather than thinking in terms of primitive interactions as RI recommends.

To understand how to program the interface, we must reject the idea that the robot "receives data from the physical world"; from where and by what would such data be sent anyway? Instead, the physical world simply has an effect on sensors, which modifies variables in the program. Interpreting the value of these variables as "data received from the physical world" is a choice that we do not make in the developmental approach.

In the developmental approach, the designer decides what primitive interactions will be available to the robot and programs the interface to control the enaction of these primitive interactions. The interface commands the actuators and reads the sensors until an end condition is reached. When an end condition is reached, the enaction is stopped, and the interface computes the enacted interaction on the basis of conditions that happened during the enaction, according to the designer's specifications. Video 53 illustrates this in an example.

[VIDEO](https://www.youtube.com/watch?v=t1RO5S4mBEY)
Video 53: Developmental e-puck robot implementing our self-programming algorithm.

Now we have three levels of coupling:

* The _cognitive coupling_ in which the robot's decisions are made (Line 1 in Figure 53). We expect this coupling to evolve as the robot develops, to allow the robot to make increasingly intelligent decisions.
* The _policy coupling_ at the level of which the robot's policy is carried out (Line 2 in Figure 53). This coupling is defined by the set of primitive interactions specified by the designer of the robot. It is determined by the interface program and by the actuators and sensors of the robot. It is not intended to change during the robot's life.
* The _physical coupling_ between the robot's program and the physical world (dashed arrows at the bottom of Figure 53). This coupling is determined by the laws of physics and by the motors and sensors. It can be continuous (as opposed to discrete), noisy, and complex.

The interface hides the complexity of the physical coupling for the agent's policy. The policy can, thus, control robots in an environment as complex as the physical world. The physical world in Video 1 was limited to an e-puck robot in a box, but it could have been arbitrarily more complex. For example, it could have included any kind of object. Those objects would just have appeared to the robot as walls, as long as it had no other way of interacting with them.

Still, the question of designing robots that can manage more complex interactions remains. In particular, this robot has no notion of space; it learns regularities in time but not in space. Lesson 6 will address the problem of managing more complex interactions in space and learning more sophisticated behaviors.

This example also raises again the question of how to design a developmental robot to perform a specific task. As introduced on Page 22, a developmental robot has no internal data generated by a direct function of the state of the world, which the designer could exploit to program the robot for performing a specific task. Instead, a developmental robot can be considered as an animal that has predefined tastes and drives, and that you must train if you want it to perform a specific task for you. The issue of training a developmental agent will be addressed on Page 55.

## Implementing radical interactionism

Table 54 presents our radical interactionist algorithm, by introducing a few modifications in our previous algorithms of Page 43. These modifications have impacts on the `learn()` and `anticipate()` functions. The participants interested in programming will find the details on Page 57.


```
Table 54: Radical interactionist algorithm.

001   i1 = createPrimitiveInteraction("i1", -1)
002   i2 = createPrimitiveInteraction("i2", 1)
003   i3 = createPrimitiveInteraction("i3", -1)
004   i4 = createPrimitiveInteraction("i4", 1)
      ... Other interactions afforded by the agent/environment coupling
      
005   constructExperiment(i1)
006   constructExperiment(i3)
      ... Other experiments constructed by default
       
007   while()
008      anticipations = anticipate()
009      experiment = selectExperiment(anticipations)
010      intendedInteraction = experiment.intendedInteraction()
011      enactedInteraction = enact(intendedInteraction)
012      learn()
013      if (enactedInteraction.valence ≥ 0)
014         mood = PLEASED
015      else
016         mood = PAINED

101   function enact(intendedInteraction)
102      if intendedInteraction.isPrimitive 
103         return environment.enact(intendedInteraction)
104      else
105         enactedPreInteraction = enact(intendedInteraction.preInteraction)
106         if (enactedPreInteraction ≠ intendedInteraction.preInteraction)
107            return enactedPreInteraction
108         else
109            enactedPostInteraction = enact(intendedInteraction.postInteraction)
110            return getOrLearn(enactedPreInteraction, enactedPostInteraction)
```

Table 54, Lines 001 to 004: Now, primitive interactions are created as primitive objects rather than being the product of experiments and results. Lines 005 and 006: Default experiments are created as secondary constructs from primitive interactions. Default experiments generate initial anticipations that can be picked by default when the agent begins.

Lines 007 to 016: The main loop is the same as on Page 43.

Lines 101 to 110: The enact() function is identical to that on Page 43, except for Line 103, which calls the `enact(primitiveInteraction)` method of the environment. The `enact(primitiveInteraction)` method replaces the previous `GiveResult(experiment)` method; it tries to enact the primitive interaction in the environment and returns the actually enacted interaction. Participants interested in programming will find an environment implemented with this principle on Page 58.

The next video explains and illustrates how this algorithm works.

## Demonstrating the emergence of perception and action from interaction

Instead of using plain-text traces to analyze our agent's activity as we did in the previous lessons, we are going to use the video recording presented in Video 55.

Importantly, Video 55 shows the emergence of active perception as the agent finds regularities of interaction and programs itself. The emergence of active perception radically differentiates the developmental approach from traditional AI approaches because it considers perception as a functional adaptation rather than as a mere interpretation of input data.

Video 55 also presents a summary explanation of our fully recursive self-programming algorithm based upon Figure 42 and our work since then. The experiment was designed to demonstrate this recursivity by offering hierarchical regularities of interaction in the policy coupling between the agent and the environment.

[VIDEO](https://www.youtube.com/watch?v=LVZ0cPpmSu8)
Video 55: Demonstrating the emergence of active perception from regularities of interaction.

The agent's active perception comes from the fact that it learns to use sensorimotor interactions that have a slightly negative valence to identify context in which it can confidently enact interactions that have a strong positive valence, and avoid enacting interactions that have a strong negative valence.

You can also play with the online demo in this page: [Small Loop](http://liris.cnrs.fr/ideal/demo/small-loop/). It allows you to remodel the environment by changing walls into empty cells and vice versa by clicking on the grid. If you rerun the agent in a different environment, you will observe that it may learn different behaviors. This is because it may program itself differently depending on its individual experience. This training effect, made possible by self-programming, is also shown in this video.

At the end of Lesson 5, participants interested in programming to reproduce this experiment with the algorithm provided on Page 58.

## Selected readings on radical interactionism

* I recommend this paper as an introduction to cognitive architectures, the topic that we will address in Lesson 6. It gives a comprehensive overview of the subject and presents challenges that remain to be addressed. One serious limitation of this review is that it does not list ontogenetic development as one of these challenges. Sun (2004) Desiderata for Cognitive Architectures Philosophical psychology, 17(3).
* Our article that reports the experiments shown in Video 53 and Video 55. Georgeon, Wolf, & Gay (2013). An Enactive Approach to Autonomous Agent and Robot Learning. IEEE Third Joint International Conference on Development and Learning and on Epigenetic Robotics (EPIROB2013). Osaka, Japan.
* The Radical Interactionism Frequently Asked Questions by Olivier. RI is a quite new approach that raises debates and questions. Please read the FAQs before asking you own questions.

If you have no interest in programming, then you may stop Lesson 5 here. If you have an interest in programming, you can find our self-programming RI algorithm below.

## More algorithms to implement radical interactionism

Radical interactionism implies a conceptual inversion of the roles of interactions and experiments. There are no _concrete_ experiments that the environment can process. Instead, the environment can only process primitive interactions, and all experiments are _abstract_.

Like in the algorithm on Page 44 and the code on Page 45, experiments have an _intended interaction_. When the agent chooses an experiment to perform, it tries to enact this experiment's intended interaction.

Now, experiments also have a list of _enacted interactions_. When the enacted interaction differs from the intended interaction, the enacted interaction is added to the experiment's list of enacted interactions. As a result, the next time the agent considers choosing this experiment, it can anticipate this possible outcome among others, and balance its decision on the basis of these different anticipations. Table 57 reports the algorithm that implements this mechanism.

```
Table 57: constructing and exploiting experiments from interactions.

101   Learn()
102      if enactedInteraction ≠ intendedInteraction
103         experiment.addEnactedInteraction(enactedInteraction)
104      superInteraction = learnCompositeInteraction(previousEnactedInteraction, enactedInteraction)
105      learnCompositeInteraction(previousSuperInteraction, enactedInteraction)
106      learnCompositeInteraction(previousSuperInteraction.preInteraction, superInteraction)

201   Anticipate()
202      activatedInteractions = getActivatedInteractions()
203      for each activatedInteraction in activatedInteractions
204         proclivity = activatedInteraction.weight * activatedInteraction.postInteraction.valence
205         addAnticipation(activatedInteraction.postInteraction.experiment, proclivity) 
206      for each anticipation in anticipations
207         for each interaction in anticipation.experiment.enactedInteractions
208            for each activatedInteraction in activatedInteractions
209               if interaction = activatedInteraction.postInteraction	
210                  proclivity = activatedInteraction.weight * interaction.valence
211                  anticipation.addProclivity(proclivity)
212      return anticipations
```

Lines 102 and 103: If the enacted interaction is not the intended interaction, add the enacted interaction to the experiment's list of enacted interactions.

Lines 104 to 106: Learn the three higher-level composite interactions as explained in Figure 44.

Lines 201 to 212: The proclivity of anticipation is now balanced depending on the odds of obtaining the afforded interaction (Lines 103 to 205) and the odds of obtaining a different enacted interaction (Lines 206 to 211).

## Implementation of radical interactionist agents

Project 5 implements the algorithm described on Page 54 and Page 57 to let you run your first fully recursive RI agent.

You can use it with the EnvironmentMaze to reproduce similar behaviors as in Video 55. The behaviors will be similar but not exactly the same because we changed the algorithm since recording the video to make the algorithm more pedagogical. Small differences in the algorithm generate different choices when the choice is arbitrary, which leads to different learning.

Project 5: modified or new files since Project 4.

```
Program.cs ← Change Main.java to instantiate Existence050.
existence / Existence050 ← The main loop of the algorithm.
coupling / Experiment050 ← Now, an Experiment has a list of enacted interactions.
environment / Environment ← The interface for the environments.
environment / Environment050 ← A new implementation of Environment040.
environment / EnvironmentMaze ← The Maze environment.
```

For Lesson 5, your only programming activity is to run `Existence050` in the Maze environment and report the trace that you obtain. You should observe that `Existence050` behaves similarly to the agent in the demonstration in Video 55.

Project 5 could also be used to replicate our various demonstrations as they are shown in Video 41, Video 53, and Video 55. However, you would have to re-implement the corresponding environments or robot, which is outside the scope of this course. The section below provides, nonetheless, a few indications on how to proceed, in case you would like to do it:

1. Video 41 was recorded using revision 238 of the Vacuum Environment Project, and revision 313 of the Ernest Agent Project. To replicate these demonstrations, checkout these two revisions in your favorite IDE (we used Eclipse). Include the Ernest project in the Java Build Path of the Vacuum project. Run the Main.java class of the Vacuum Project.

2. You can also replicate the demonstrations in Video 41 using Project 5 in the Vacuum Environment. In Project 5, create the `EnvironmentVacuum238` class as an implementation of the Environment interface. Implement your `Environment­Vacuum238.enact(intended­Interaction)` method so that it calls revision 238 of the Vacuum Project to execute the intended interaction in the Vacuum Environment and to return the enacted interaction.

3. You can also replicate the demonstration in Video 53 using Project 5. Create the `EnvironmentRobot` class as an implementation of the Environment interface. Implement your `EnvironmentRobot.enact(intendedInteraction)` method to control the enaction of the primitive interactions by activating motors and reading sensors, and to return the actually enacted interaction. Recall the discussion on how to implement a robot interface on Page 53.

4. You can replicate the demonstration in Video 55 using revision 203 of the [Vacuum Environment Project](https://code.google.com/archive/p/vacuum-sg-continuum/), and revision 296 of the [Ernest Agent Project](https://github.com/OlivierGeorgeon/ernest).

5. You can also replicate the demonstration in Video 55 using the Ernest Project in the [NetLogo](https://ccl.northwestern.edu/netlogo/) environment. Follow the instructions provided on the Online Demonstration Page. You can also re-implement our [NetLogo extension](https://code.google.com/archive/p/imos-netlogo/) to use Project 5.

Note that the Ernest project was a previous version of the algorithm which no longer complies with the notation and vocabulary used in this course. You may, thus, have difficulties to find your way around it. We do not intend to maintain it in the future. Be also aware that the Vacuum Environment was developed anarchically over the years to test various experiments as they went. It is not a standardized environment and you may find it messy. We do not provide support to use it.

This ends Lesson 5.