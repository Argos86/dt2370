#include <OgreSceneNode.h>
#include <OgreSceneManager.h>
#include <OgreVector3.h>
#include <OgreQuaternion.h>
#include <OgreString.h>

#include <OIS/OIS.h>
#include "Weapon.h"
#include "WeaponBase.h"
#include "Projectile.h"
#include "WeaponSight.h"

Weapon::Weapon(Ogre::SceneNode *a_playerNode, Ogre::SceneManager *a_scenemgr, Ogre::Vector3 a_relativePosition, Ogre::String a_name) 
	: WeaponBase (a_playerNode, a_scenemgr, a_relativePosition, a_name)
{
	for (int x = 0; x < MAX_PROJECTILES-1 ; x++)
	{
		m_projectile[x] = NULL;
	}
	m_timeSinceFired = 0.0;
	m_timeBetweenFire = 50.0;
	m_recoil = 0;
} 

void Weapon::Update( Ogre::Real a_timeSinceLastFrame)
{
	m_timeSinceFired += a_timeSinceLastFrame;
	m_weaponNode->setPosition(m_relativePosition + m_weaponNode->getOrientation() * Ogre::Vector3(0,0, + m_recoil));

	for (int x = 0; x < MAX_PROJECTILES-1 ; x++)
	{
		if(m_projectile[x] != NULL)
		{
			if (!m_projectile[x]->Update(a_timeSinceLastFrame))
			{
				delete m_projectile[x];
				m_projectile[x] = NULL;
			}
		}
	}
	if ( m_fireId == (MAX_PROJECTILES -1) )
	{
		m_fireId = 0;
	}

	if (m_recoil > 0.0)
	{
		m_recoil -= m_recoil * (a_timeSinceLastFrame/300);
	}
}

void Weapon::Fire()
{
	if (m_timeSinceFired > m_timeBetweenFire) {
		m_projectile[m_fireId] = new Projectile( m_weaponNode->getParentSceneNode()->getPosition() + m_weaponNode->getParentSceneNode()->getOrientation() * m_weaponNode->getPosition(), m_weaponNode->getParentSceneNode()->getOrientation(), m_scenemgr, m_fireId, m_uniqueName);
		m_fireId += 1;
		m_timeSinceFired = 0.0;
		this->MakeRecoil(5.0);
	}
}



Weapon::~Weapon()
{
	for (int x = 0; x < MAX_PROJECTILES-1 ; x++)
	{
		if(m_projectile[x] != NULL)
		{
			delete m_projectile[x];
			m_projectile[x] = NULL;
		}
	}
}