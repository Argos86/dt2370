#include <OgreSceneNode.h>
#include <OgreSceneManager.h>
#include <OgreVector3.h>
#include <OgreQuaternion.h>
#include <OgreString.h>

#include "Weapon.h"
#include "WeaponBase.h"
#include "Projectile.h"
#include "WeaponSight.h"
#include "..\IModel.h"
#include "..\..\Controller\IEvent.h"

Weapon::Weapon( Ogre::SceneManager *a_scenemgr, Ogre::Vector3 a_relativePosition, Ogre::String a_name, IEvent *a_eventToView, IModel *a_eventToModel, ISound *a_soundEffects) 
	: WeaponBase ( a_scenemgr, a_relativePosition, a_name, a_eventToView, a_eventToModel, a_soundEffects)
{

	std::stringstream name;
	name << m_uniqueName << "StandardWeapon:" ;
    m_uniqueName = name.str();

	m_weaponPipeEntity = m_scenemgr->createEntity( m_uniqueName, "Mesh/Tube01.mesh" );
	m_weaponPipeEntity->setMaterialName("test1");
	m_weaponPipeNode = m_weaponNode->createChildSceneNode(m_uniqueName, Ogre::Vector3(0,0,0));
	m_weaponPipeNode->attachObject(m_weaponPipeEntity);
	m_weaponPipeNode->setPosition(Ogre::Vector3(0,+5,-50));

	for (int x = 0; x < MAX_PROJECTILES-1 ; x++)
	{
		m_projectile[x] = NULL;
	}
	m_timeSinceFired = 0.0;
	m_timeBetweenFire = 80.0;
	m_recoil = 0;
} 

void Weapon::Update( float a_timeSinceLastFrame)
{
	m_timeSinceFired += a_timeSinceLastFrame;
	m_weaponPipeNode->setPosition( Ogre::Vector3::ZERO + m_weaponPipeNode->getOrientation() * Ogre::Vector3(0,+5,-50 + m_recoil));

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
		//Kollar om jag träffar, om jag gör det så skadar jag fienden som projektilen kommer träffa.
		if (m_eventToModel->CollisiontestEnemies(m_weaponNode->getParentSceneNode()->getPosition() + m_weaponNode->getParentSceneNode()->getOrientation() * m_weaponNode->getPosition(),m_weaponNode->getOrientation())) {
			m_eventToModel->DamageEnemies(ATTACK_DAMAGE, 30);
		}
		m_soundEffects->MakeStandard();
		m_projectile[m_fireId] = new Projectile( m_weaponNode->getParentSceneNode()->getPosition() + m_weaponNode->getParentSceneNode()->getOrientation() * m_weaponNode->getPosition(), m_weaponNode->getOrientation(), m_scenemgr, m_fireId, m_uniqueName);
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