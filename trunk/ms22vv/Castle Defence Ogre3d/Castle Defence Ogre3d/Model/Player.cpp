#include <OgreSceneNode.h>
#include <OgreSceneManager.h>
#include <OgreVector2.h>
#include <OgreVector3.h>
#include <OgreQuaternion.h>
#include <OgreString.h>
#include <OgreEntity.h>

#include "Player.h"
#include "Weapon\Weapon.h"
#include "Weapon\WeaponFactory.h"
#include "..\Controller\IEvent.h"

Player::Player(Ogre::SceneManager *a_scenemgr)
{
	m_playerNode = a_scenemgr->getRootSceneNode()->createChildSceneNode( "PlayerNode", Ogre::Vector3(0.0f, 0.0f, 0.0f) );
	m_playerNode->setScale(1.0,1.0,1.0);

	m_stativNode = a_scenemgr->getRootSceneNode()->createChildSceneNode( "PlayerDonutNode", Ogre::Vector3(0.0f, 0.0f, 0.0f) );
	m_stativNode->setScale(1.0,1.0,1.0);

	m_playerVelocity = 0;
	m_playerNode->setPosition(Ogre::Vector3(0,+50,+800));
	m_stativNode->setPosition( m_playerNode->getPosition() + Ogre::Vector3(0, +20.0f, 0.0f) );

	m_activeWeapon = NULL;
}

void Player::UpdateWeapon( float a_timeSinceLastFrame )
{
	m_activeWeapon->Update( a_timeSinceLastFrame);
}

void Player::Move(Ogre::Vector3 a_movementVector, float a_timeSinceLastFrame)
{
	m_stativNode->roll( Ogre::Degree(- a_movementVector.x * a_timeSinceLastFrame) );
	m_stativNode->setPosition( m_stativNode->getPosition() + a_movementVector * a_timeSinceLastFrame );
	m_playerNode->setPosition( m_stativNode->getPosition() + Ogre::Vector3(0, -20.0f, 0.0f));
}

void Player::Rotate(Ogre::Vector2 a_mousePosition)
{
	m_activeWeapon->Rotate(a_mousePosition);
}

void Player::SetActiveWeapon(WeaponBase *a_activeWeapon)
{
	m_activeWeapon = a_activeWeapon;
}




void Player::ResetOrientation()
{
	std::cout << "Tvingar Player, Weapon och Camera-orientationen: Quaternion::IDENTITY " << std::endl << "Player::Quaternion = " << this->GetOrientation()  << std::endl; 
	m_playerNode->setOrientation(Ogre::Quaternion::IDENTITY);
	m_activeWeapon->ResetOrientation();
}


void Player::FireWeapon()
{
	m_activeWeapon->Fire();
}


Ogre::Vector3 Player::GetPosition()
{
	if (m_playerNode) {
		return m_playerNode->getPosition();
	}
	else {
		return Ogre::Vector3();
	}
}

Ogre::Quaternion Player::GetOrientation()
{
	if (m_playerNode) {
		return m_playerNode->getOrientation();
	}
	else {
		return Ogre::Quaternion();
	}
}


Ogre::Vector3 Player::GetWeaponPosition()
{
	if (m_activeWeapon) {
		return m_activeWeapon->GetPosition();
	}
	else {
		return Ogre::Vector3();
	}
}
Ogre::Quaternion Player::GetWeaponOrientation()
{
	if (m_activeWeapon) {
		return m_activeWeapon->GetOrientation();
	}
	else {
		return Ogre::Quaternion();
	}
}


float Player::GetVelocity()
{
	return m_playerVelocity;
}

Player::~Player()
{

}


