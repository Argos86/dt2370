#ifndef Timer_H_
#define Timer_H_

#include <OgreTimer.h>

class GameTimer 
{
private:
	Ogre::Timer *m_timer;
	float m_lastFrame;
	float m_temp;
	float m_timeSinceLastFrame;

public:	
	GameTimer::GameTimer();
	float GameTimer::GetTimeSinceLastFrame( );

	GameTimer::~GameTimer();
};
#endif
