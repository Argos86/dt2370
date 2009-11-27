#include <OgreSceneNode.h>
#include <OgreSceneManager.h>
#include <OgreVector3.h>
#include <OgreString.h>

#include "Laser.h"
#include "WeaponBase.h"
#include "..\IModel.h"
#include "..\..\Controller\IEvent.h"

Laser::Laser( Ogre::SceneManager *a_scenemgr, Ogre::Vector3 a_relativePosition, Ogre::String a_name, IEvent *a_eventToView, IModel *a_eventToModel, ISound *a_soundEffects) 
	: WeaponBase ( a_scenemgr, a_relativePosition, a_name, a_eventToView, a_eventToModel, a_soundEffects)
{
	std::stringstream name;
	name << m_uniqueName << "LaserWeapon:" ;
    m_uniqueName = name.str();

	m_weaponPipeEntity = m_scenemgr->createEntity( m_uniqueName, "Mesh/Tube01.mesh" );
	m_weaponPipeEntity->setMaterialName("test2");
	m_weaponPipeNode = m_weaponNode->createChildSceneNode(m_uniqueName, Ogre::Vector3(0,0,0));
	m_weaponPipeNode->attachObject(m_weaponPipeEntity);
	m_weaponPipeNode->setPosition(Ogre::Vector3(0,+5,-50));

	m_timeSinceFired = 0.0;
	m_timeBetweenFire = 500.0;
	m_recoil = 0.0;
}

void Laser::Update( float a_timeSinceLastFrame)
{
	m_timeSinceFired += a_timeSinceLastFrame;
	m_weaponPipeNode->setPosition( Ogre::Vector3::ZERO + m_weaponPipeNode->getOrientation() * Ogre::Vector3(0,+5,-50 + m_recoil));
	if (m_recoil > 0.0)
	{
		m_recoil -= m_recoil * (a_timeSinceLastFrame/300);
	}
}

void Laser::Fire()
{
	if (m_timeSinceFired > m_timeBetweenFire) {
		//Kollar om jag träffar, om jag gör det så gör jag en coolare "spline" :P
		if (!m_eventToModel->CollisiontestEnemies(m_weaponNode->getParentSceneNode()->getPosition() + m_weaponNode->getParentSceneNode()->getOrientation() * m_weaponNode->getPosition(), m_weaponNode->getOrientation())) {
			m_eventToView->MakeSpline(m_weaponNode->getParentSceneNode()->getPosition() + m_weaponNode->getParentSceneNode()->getOrientation() * m_weaponNode->getPosition(), m_weaponNode->getOrientation(), m_uniqueName, MAX_DISTANCE);
			m_soundEffects->MakeLaserMiss();
		}
		else {
			m_eventToModel->DamageEnemies(ATTACK_DAMAGE, 50); // fördröjningen borde beräknas..
			m_eventToView->MakeSplineHit(m_weaponNode->getParentSceneNode()->getPosition() + m_weaponNode->getParentSceneNode()->getOrientation() * m_weaponNode->getPosition(), m_weaponNode->getOrientation(), m_uniqueName, m_eventToModel->GetLastCollisionDistance());
			m_soundEffects->MakeLaserHit();
		}
		m_timeSinceFired = 0.0;
		this->MakeRecoil(40.0);
	}
}



Laser::~Laser()
{

}
