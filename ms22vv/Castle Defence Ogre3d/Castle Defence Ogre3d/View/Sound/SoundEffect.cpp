#include <OgreSceneNode.h>
#include <OgreSceneManager.h>
#include <OgreVector3.h>
#include <OgreQuaternion.h>
#include <OgreString.h>

#include "SoundEffect.h"
#include "SoundManager.h"
#include "ISound.h"

SoundEffect::SoundEffect(Ogre::SceneManager *a_scenemgr) 
:ISound()
{
	m_soundMgr = new SoundManager;
	m_soundMgr->Initialize();

	m_soundLaserHit = m_soundMgr->CreateSound(Ogre::String("LaserHit.wav"), SOUND_TYPE_2D_SOUND);
	m_soundLaserMiss = m_soundMgr->CreateSound(Ogre::String("LaserMiss.wav"), SOUND_TYPE_2D_SOUND);
	m_soundStandard = m_soundMgr->CreateSound(Ogre::String("WeaponFast01.wav"), SOUND_TYPE_2D_SOUND);
	m_soundEnemyDeath = m_soundMgr->CreateSound(Ogre::String("Scream01.wav"), SOUND_TYPE_2D_SOUND);

	m_channelLaserHit = 1;
	m_channelLaserMiss = 2;
	m_channelStandard = 3;
	m_channelEnemyDeath = 4;

	tempNode = a_scenemgr->getRootSceneNode()->createChildSceneNode(Ogre::Vector3(0,0,+800));	
}

void SoundEffect::MakeLaserHit()
{
	m_soundMgr->StopSound(&m_channelLaserHit);
	m_soundMgr->PlaySound(m_soundLaserHit, tempNode, &m_channelLaserHit);
}

void SoundEffect::MakeLaserMiss()
{
	m_soundMgr->StopSound(&m_channelLaserMiss);
	m_soundMgr->PlaySound(m_soundLaserMiss, tempNode, &m_channelLaserMiss);
}

void SoundEffect::MakeStandard()
{
	m_soundMgr->StopSound(&m_channelStandard);
	m_soundMgr->PlaySound(m_soundStandard, tempNode, &m_channelStandard);
}

void SoundEffect::MakeEnemyDeath()
{
	m_soundMgr->StopSound(&m_channelEnemyDeath);
	m_soundMgr->PlaySound(m_soundEnemyDeath, tempNode, &m_channelEnemyDeath);
}

SoundEffect::~SoundEffect()
{

}
