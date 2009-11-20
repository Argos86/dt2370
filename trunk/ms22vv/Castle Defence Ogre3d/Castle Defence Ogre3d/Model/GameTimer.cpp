#include <OgreTimer.h>

#include "GameTimer.h"
#include <cstdlib> 

GameTimer::GameTimer()
{
	m_timer = new Ogre::Timer;
	m_lastFrame = 0;
	m_temp = 0;
	m_timeSinceLastFrame = 0;
}

float GameTimer::GetTimeSinceLastFrame()
{
	m_temp = m_timer->getMillisecondsCPU();
	m_timeSinceLastFrame = m_temp - m_lastFrame;
	m_lastFrame = m_temp;

	return m_timeSinceLastFrame;	
}



GameTimer::~GameTimer()
{
	m_timer = NULL;
	//samma problem med sceneManagern.. ska förstöra Ogre::Timer
}


