Controls:

Pressing C switches cameras modes between third person, above the agent camera and a free looking camera
Third person Controls:
	W: walk forward
	S: walk backwards
	A: turn left while standing
	D: turn right while standing
	A+W: turn left while walking forward
	D+W: turn right while walking forward
	A+S: turn left while walking backwards
	D+S: turn right while walking backwards
	Shift+W: run forward
	Shift+S: run backward
	A+Shift+W: turn left while running forward
	D+Shift+S: turn right while runningforward
	J: strafe left while standing still
	L: strafe right while standing still
	Space+Shift+W: jump while running forward
	Space+W: jump while walking forward
	Space: jump while standing still
Free Look Camera Controls:
	Scroll Wheel: zoom in and out along world y-axis
	Mouse: look around
	W: move forward
	S: move backward
	A: move left
	D: move right
	I: increase agent speeds
	K: decrease agent speeds
	Right Mouse Click: Select/deselect agent or select destination for selected agents depending on what was 		clicked

Implementation: blend trees for jumping, walking/running, turning

Features: With the third erson view, the character can be controlled using varius keys and animate accordingly.
With the free look enabled, characters can be selected and then destinations for those characters can be clicked. In this view, the speeds of the agents can also be controlled by pressing I or J.
	
Demo1: shows the 10 agent escape room scenario as well as an off-mesh link example
Demo2: highlights features of third person view
Demo3: shows a complex terrain with several different agents

Extra Credit(animation-physics managed navigation): implementation without the use of NavMeshAgent(more on this on website).