# ideal
An ideal algorithm for control in the manner of Cybernetics. This branch contains details of cognitive architectures.

## Code module

There are no code modules for this branch.

# Section 6: An introduction to developmental cognitive architectures

## Introduction to developmental cognitive architectures

In §5.3, the issue was raised of designing agents that can deal with more complex possibilities of interaction. There was defined three levels of coupling:

1) cognitive coupling, 
2) policy coupling, and, 
3) physical coupling.

So far, we have been working with a policy coupling that is discrete and small: for example, a set of ten primitive interactions in Video 5.5. A legitimate question is: what would happen if the policy coupling contained a greater number of primitive interactions, or even if the policy coupling was modeled as a continuous space rather than a discrete set? More broadly, how does the algorithm scale up when the policy coupling gets more complex?

The answer is simple: the algorithm that we have been using thus far can't scale up when the complexity of the policy coupling increases arbitrarily. The scaling limitation is because the time to discover and exploit regularities of interaction increases exponentially as the number of primitive interactions increases, and as length of the regularities that are afforded by the policy coupling grows.

To scale up towards "something more complex", we must examine what scaling problems we are facing and what "more complex results" we want to obtain.

Our demonstration in Video 5.3 shows that our algorithm can interact with the real world. However, it also shows that the behavior is still very rudimentary. This demonstration illustrates that our scaling problem consists of generating more sophisticated behaviors at the physical coupling level, but it does not involve dealing with an arbitrarily complex policy coupling.

Indeed, we must deal with the complexity of the physical coupling because this complexity is imposed by the real world. The complexity of the policy coupling, however, is not imposed. We are free to design the policy coupling to suite our needs. We need a well-designed policy coupling that makes learning smarter behaviors possible when the robot interacts with the real world at the physical coupling level.

Note that it is fortunate that we do not need to face an arbitrarily complex policy coupling because we have no reason to believe that an algorithm capable of scaling up with this complexity would even be possible. In contrast, the example of natural cognitive systems -- animals and humans -- illustrates that it is possible to deal with the complexity associated with the physical coupling. Even animals with modest computational resources, e.g., animals with small brains like insects and small vertebrates, exhibit relatively smart behaviors and complex learning in the real world.

In §4.2, we discussed why the agent should construct a coherent model of the world on the basis of regularities of interactions that it discovers. The agent must learn that regularities of interaction are caused by entities that exist in the world. Knowledge of entities that exist in the world is called _ontological knowledge_.

The terms _ontological_ and _ontology_ have the advantage of carrying with them centuries of discussions about the question "what is there in the world?", and about the possibility of even answering this question. See the Wikipedia article about [Ontology (philosophy)](https://en.wikipedia.org/wiki/Ontology), or [Willard Van Orman Quine's article](https://en.wikisource.org/wiki/On_What_There_Is) that examines this question not without humor. The Wikipedia article about [Ontology (information science)](https://en.wikipedia.org/wiki/Ontology_%28information_science%29) also gives an overview of how a designer can pre-encode ontological knowledge in a traditional AI system.

For the purposes of this course, let us take from these philosophical discussions that ontological knowledge is always pragmatical. That is to say, people or groups of people construct ontological knowledge, and this construction process is fundamentally influenced by their motivations. In contrast with traditional AI, designers of developmental agents do not encode the agent with presupposed ontological knowledge, because, if they did so, the agent's ontological knowledge would not be grounded in the agent's experiences and motivations -- it would not be the agent's knowledge but the designer's knowledge. Instead, the developmental machine intelligence approach aims at designing agents capable of constructing their own ontological knowledge on the basis of their experiences interacting with the world and with reference to their own motivations.

Let us also take from these philosphical discussions that entities of the world exist in the three-dimensional real space. This leads us to the conclusion that developmental agents should not only be sensitive to temporal regularities (as are the algorithms thus far), but also to spatial regularities; hence the key concept of Section 6:

`Spatio-temporal regularities of interaction lead to ontological knowledge of the world.`

To design agents that can construct ontological knowledge from spatio-sequential regularities of interaction, we draw inspiration from natural organisms. Natural organisms generally have inborn brain structures that encode space, preparing them to detect and learn spatio-sequential regularities of interaction. We design the policy coupling of our agents by pulling sections from these natural brain structures, which leads us to a biologically-inspired developmental cognitive architecture.

## 6.2: Developmental cognitive architecture

There are many inborn brain structures that encode spatial information. For example, Gross & Graziano's (1995) Multiple representations of space in the brain discusses the encoding of spatial knowledge in primates. From these kinds of studies, we conclude that it is reasonable to endow our agent with a hard-coded spatial memory, rather then expecting spatial memory to emerge spontaneously. In so doing, we hard-code presupposed knowledge about the environment, namely, knowledge that the environment has a three-dimensional structure. By making this assumption, we restrict our scope to agents that indeed exist in a three-dimensional world, such as automata and physical robots. We could probably not encode the same assumption with agents that exist in fancy abstract worlds, for example bots that crawl the internet.

To remain consistent with constructivist epistemology, spatial memory should not encode presupposed ontological knowledge about the environment. Recall that the agent never knows which entities "as such" exist in the world, but only knows possibilities of interaction. Accordingly, our spatial memory only encodes the knowledge that certain interactions have been or can be enacted in certain regions of space. Using again the terms _experiment_ and _result_, the agent knows that, if it performed a certain experiment in a certain region of space, it would obtain a certain result, but the agent does not know the essence of the entity that occupies this region of space.

Radical interactionism, introduced in Section 5, facilitates the implementation of a constructivist spatial memory. In the RI formalism, spatial memory simply maintains the position of enacted interactions relative to the agent. Figure 6.2 shows our cognitive architecture implemented with RI.

![Figure-62](/images/062-1.png)
Figure 6.2: The Enactive Cognitive Architecture (ECA).

Bottom: the _Interaction Timeline_ shows the stream of interactions enacted over time. Enacted interactions are represented by colored symbols as shown on the bottom line of Figure 4.2. Here, enacted interactions come from the example in Video 4.1: green trapezoids represent _turning towards a prey_, green squares represent _stepping towards a prey_, blue squares represent _eating a prey_.

Top: the _Sequential System_ represents the hierarchical sequential regularity learning mechanism that we have been developing thus far.

Center: _Spatial Memory_ keeps track of the position (relative to the agent) of enacted interactions over the short term. The orange arrowhead represents the agent's position at the center of spatial memory, oriented towards the right. The blue square and the green triangle represent interactions that have been enacted in the front right of the agent. When the agent moves, spatial memory is updated to reflect the relative displacement of enacted interactions, as we will develop on the next section. This egocentric spatial memory is inspired by the [Superior Colliculus](https://en.wikipedia.org/wiki/Superior_colliculus) in the mammal's brain.

Spatial memory is only short-term. It is not intended to construct a long-term map of the environment, but only to keep track of the relative positions where interactions have been recently enacted, in order to detect spatial overlap. For example, the spatial overlap between the interactions _turning towards a prey_ (green trapezoid) and _eating a prey_ (blue square) allows the agent to infer that it is the same _object_ that it has seen earlier and then eaten. The agent can thus associate these two interactions in a _bundle_ that represents the category of entities corresponding to preys.

Left: the _Ontology_ mechanism records _bundles_ of interactions based on their spatial overlap observed in spatial memory. For example, the blue circle represents the bundle that represents preys; it gathers the interactions that are afforded by preys. The large red square represents the wall object that the agent can experience through the _bump_ interaction (small red square), but that it cannot see in the example in Video 4.1.

Once constructed, bundles allow the evocation of types of objects in spatial memory. In turn, evoked types of objects propose the interactions that they afford. For example, over time, the fact of seeing a prey in a certain region of space evokes the eat interaction in this region.

Note that the term _Ontology Mechanism_ does not mean to imply that the agent has access to the ontological essence of entities in its environment. Instead, this term denotes the fact that the agent constructs its own ontology of the world on the basis of its interaction experiences.

Right: _Behavior Selection_ mechanism balances the propositions made by the sequential system and by spatial memory, and then selects the next sequence of interactions to try to enact. For example, if the eat interaction is evoked in spatial memory in front of the agent, then the behavior selection mechanism may select eat as the next intended interaction to try to enact.

## 6.3: Demonstrations of a developmental cognitive architecture

Video 6.1 demonstrates the ECA architecture presented in Figure 6.2. It uses a similar experiment as the one used in the E-puck Experiment in Video 5.3, and in the Little Loop Experiment in Video 5.5.

[VIDEO](https://www.youtube.com/watch?v=HCDf3Vzl7GM)
Video 6.1: Demonstration of the enactive cognitive architecture in the Little Loop Problem.

Video 6.2 demonstrates the ECA architecture when the agent has a visual system similar to that in Video 4.1. In this video, the two grey sharks implement ECA, whereas the blue fish are simply moving on a straight ligne to illustrate a dynamic environment. In addition to preys (blue fish), sharks can also see other salient objects: flowers, colored bricks of walls, and other sharks (but not dark green walls because they blend into the background).

Due to their interactional motivation, the sharks tend to move towards salient objects -- as in Video 4.1, interactions consisting of getting closer to salient objects have a predefined positive valence.

Once they reach a salient object, the sharks can experience specific interactions: They can eat a fish, bump into a wall, or cuddle with another shark, flowers afford the _move forward_ interaction as empty cells, the sharks just swim through them.

As explained in §6.2, the ontology mechanism associates interactions when they overlap in space. As a result, the sharks learn bundles of interactions that represent fish, brick walls, other sharks, and flowers. Once these bundles are learned, the behavior selection mechanism favors moving towards fish or other sharks because they afford interactions that have a positive valence such as eating and cuddling.

[VIDEO](https://www.youtube.com/watch?v=LjOck5ts_2g)
Video 6.2: Demonstration of the enactive cognitive architecture in a continuous, open, and dynamic environment.

## 6.4: Formalism for a spatio-sequential policy coupling

The architecture in Figure 6.2 raises the issue of localizing interactions in space, and of updating spatial memory as the agent moves. For example, if the agent touched an object to the right, then turned left, the agent must update its spatial memory to keep track of the fact that the _touch right_ interaction had been enacted in a position that is now behind the agent.

To address this issue, we designed the _spatio-sequential policy_ coupling shown in Figure 6.3, as an extension of the RI model introduced in Figure 5.1.

![Figure-64](/images/064-1.png)
Figure 6.4: Spatio-sequential policy coupling. The agent sends a data structure called _Intention_ to the environment, containing the intended interaction $i_t$. In return, the agent receives a data structure called _Obtention_, containing the enacted interaction $e_t$, the enacted interaction's position relative to the agent $σ_t$, and the geometrical transformation resulting from the agent's displacement $τ_t$.

The spatio-sequential policy coupling extends the RI model by adding information $σ_t$ and $τ_t$ to provide the agent with spatial information related to the enaction of $e_t$.

$σ_t$ specifies a point in the space surrounding the agent where $e_t$ can be approximately situated. In a two-dimensional environment, $σ_t ∈ ℝ^2$ represents the Cartesian coordinates of this point in the agent's egocentric referential.

$τ_t$ specifies a geometrical transformation that approximately represents the agent's movement in space resulting from the enaction of $e_t$. In a two-dimensional environment, $τ_t = (θ_t, ρ_t)$ with $θ_t ∈ ℝ$ being the angle of rotation of the environment relative to the agent, and $ρ_t ∈ ℝ^2$ the two dimensional vector of translation of the environment relative to the agent.

The intuition for σt is that the agent has sensory information available to it that helps it situate an interaction in space. For example, humans are known to use eye convergence, kinesthetic information, and interaural time delay -- among other forms of sensory information -- to infer the spatial origin of their visual, tactile, and auditory experiences.

The intuition for $τ_t$ is that the agent has sensory information available that helps it keep track of its own displacements in space. Humans are known to use vestibular and optic flow information to realize such tracking.

In Videos 6.1 and 6.2 these sensors were simulated by a function that directly passed $σ_t$ and $τ_t$ from the environment to the agent.

In automata, $σ_t$ and $τ_t$ can be derived from spatial sensors such as telemeters and accelerometers. The automaton, however, would need to calibrate its spatial sensors to generate $σ_t$ and $τ_t$ with enough precision. If the spatial sensors cannot be satisfactorily calibrated, then the agent's spatial memory would need to implement more complex geometrical operations than simple affine transformations. We still need to investigate how an automaton can autonomously calibrate its spatial sensors, or how spatial memory can implement more complex geometrical operations to deal with uncalibrated sensors.

Notably, the policy coupling in Figure 6.4 also opens the way to including more information in the _Intention_ data structure. This data structure could include more than one intended interaction on each interaction cycle, allowing the robot to control different body parts simultaneously. For example, a two-wheel robot could control each of its wheels separately, by sending an intended interaction associated with each wheel. This would allow generating more sophisticated behaviors than in the experiment of Video 5.3, in which the _move forward_ and _turn_ interactions controlled the two wheels simultaneously in a predefined manner.

The _Intention_ data structure could also include spatial information allowing the agent to specify the position in which it intends to enact an interaction. This position would correspond to an _intended_ $σ_t$, identical to the _obtained_ $σ_t$ in the _Obtention_ data structure. For example, we imagine a different implementation of the experiment in Video 6.1, in which the agent could choose where it intends to enact the touching interaction by specifying its intended position, e.g., [1,0]: (front), [0,1]: (left),...etc.

## 6.5: Implementation

Section 6 does not propose any programming activities. The principles in Section 6 can be implement in many different ways. It is being contemplated these different possibilities as no stable solution has presented itself.

Nonetheless, some notions are offered in §6.3:

1. Video 6.1 using revision 186 of the Vacuum Environment Project, and revision 261 of the Ernest Agent Project.

2. Video 6.2 using revision 165 of the Vacuum Environment Project, and revision 225 of the Ernest Agent Project.

Refer to §5.8 for recommendations on how to use this code. It is sinerely hoped that you now have the necessary background to continue on your own. From this point, any programming work you may do would constitute the original exploration of unknown territory and an innovative contribution to research. :-)

## 6.6: Selected readings on cognitive architectures

An article on a recent implementation of the Enactive Cognitive Architecture (ECA), similar to that used in the demonstrations in §6.3:

* Georgeon, Marshall, and Manzotti (2013). ECA: An enactivist cognitive architecture based on sensorimotor modeling. Biologically Inspired Cognitive Architectures, 6:46-57.

As a basis for comparison, you can get a sense of the number and variety of BICAs that are out there (but we don't know any of them who would claim affiliation to the embodiment and developmental paradigm):

* The BICA society's repository of cognitive architectures.

To move on towards higher-level intelligence, we expect that internal simulation of courses of events will be decisive, and ECA supports this. In particular, we believe in the hypothesis supported by Prof. Buzsáki that spatial navigation might ground higher-level reasoning:

* Buzsáki & Moser (2013), Memory, navigation and theta rhythm in the hippocampal-entorhinal system, Nature Neuroscience, 16(2):130-8.

This ends Section 6.