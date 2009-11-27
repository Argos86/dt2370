#ifndef Sound_Effect_H_
#define Sound_Effect_H_

#include <OgreSceneNode.h>
#include <OgreSceneManager.h>
#include <OgreVector3.h>
#include <OgreQuaternion.h>
#include <OgreString.h>

#include "SoundManager.h"
#include "ISound.h"

class SoundEffect : public ISound
{
private:

	SoundManager *m_soundMgr;
	Ogre::SceneNode *tempNode;

	int m_soundLaserHit;
	int m_soundLaserMiss;
	int m_soundStandard;
	int m_soundEnemyDeath;

	int m_channelLaserHit;
	int m_channelLaserMiss;
	int m_channelStandard;
	int m_channelEnemyDeath;


public:	
	SoundEffect::SoundEffect(Ogre::SceneManager *a_scenemgr);
	void SoundEffect::MakeLaserHit();
	void SoundEffect::MakeLaserMiss();
	void SoundEffect::MakeStandard();
	void SoundEffect::MakeEnemyDeath();
	SoundEffect::~SoundEffect();
};
#endif









