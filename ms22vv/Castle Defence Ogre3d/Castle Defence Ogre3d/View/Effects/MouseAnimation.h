#ifndef Mouse_Animation_H_
#define Mouse_Animation_H_

#include <OgreSceneNode.h>
#include <OgreSceneManager.h>
#include <OgreVector3.h>
#include <OgreString.h>
#include <OgreEntity.h>
#include <OgreQuaternion.h>
#include <OgreAxisAlignedBox.h>

class MouseAnimation 
{
private:
	Ogre::SceneNode *m_mouseNode;
	Ogre::Entity *m_mouseEntity;
	Ogre::SceneManager *m_scenemgr;
	bool m_isVisible;
	float m_sense;
	float m_radius;

	static const int SCALE_AT_ABOVE = 4;
	static const int SCALE_AT_NORMAL = 3;

public:	
	MouseAnimation::MouseAnimation(Ogre::SceneManager *a_scenemgr);
	void MouseAnimation::Update( float a_timeSinceLastFrame);

	void MouseAnimation::MoveRelative(Ogre::Vector2 a_movement, float a_timeSinceLastFrame);
	void MouseAnimation::SetVisibility(bool a_state);
	void MouseAnimation::SetAboveEnemy();
	void MouseAnimation::SetAboveWeapon();
	void MouseAnimation::SetNoTarget();

	Ogre::Vector3 MouseAnimation::GetPosition();
	float MouseAnimation::GetRadius();
	MouseAnimation::~MouseAnimation();
};
#endif
