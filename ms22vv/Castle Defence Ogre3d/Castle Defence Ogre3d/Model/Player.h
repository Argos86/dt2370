#ifndef Player_H_
#define Player_H_

#include <OgreSceneNode.h>
#include <OgreSceneManager.h>
#include <OgreVector2.h>
#include <OgreVector3.h>
#include <OgreQuaternion.h>
#include <OgreString.h>
#include <OgreEntity.h>

#include "Weapon\WeaponBase.h"
#include "..\Controller\IEvent.h"


class Player
{
private:
	Ogre::SceneNode *m_playerNode;
	Ogre::SceneNode *m_playerEntityNode;
	Ogre::Entity *m_playerEntity;

	Ogre::SceneNode *m_stativNode;
	Ogre::Entity *m_stativEntity;
	float m_playerVelocity;

	//Weapon
	WeaponBase *m_activeWeapon;

public:	
	Player::Player(Ogre::SceneManager *a_scenemgr );
	void Player::Move(Ogre::Vector3 movementVector, float a_timeSinceLastFrame);
	void Player::UpdateWeapon( float a_timeSinceLastFrame );
	void Player::Rotate(Ogre::Vector2 a_mousePosition);
	void Player::SetActiveWeapon(WeaponBase *a_activeWeapon);

	Ogre::Vector3 Player::GetPosition();
	Ogre::Quaternion Player::GetOrientation();
	void Player::ResetOrientation();
	float Player::GetVelocity();
	void Player::FireWeapon();

	Ogre::Vector3 Player::GetWeaponPosition();
	Ogre::Quaternion Player::GetWeaponOrientation();

	Player::~Player();
};
#endif
