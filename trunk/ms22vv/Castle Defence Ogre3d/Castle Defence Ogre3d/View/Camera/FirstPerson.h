#ifndef First_Person_H_
#define First_Person_H_

#include <OgreRoot.h>
#include <OgreSceneManager.h>
#include <OgreCamera.h>
#include <OgreVector2.h>
#include <OgreVector3.h>
#include <OgreQuaternion.h>
#include <OgreRenderWindow.h>
#include <OgreViewport.h>

#include "Camera.h"

class FirstPerson : public CameraManager
{
private:


public:	
	FirstPerson::FirstPerson( Ogre::SceneManager *a_scenemgr, Ogre::RenderWindow *a_window, Ogre::String a_name);
	void FirstPerson::Update( Ogre::Vector3 a_playerPosition, Ogre::Vector2 a_mousePosition );
	void FirstPerson::ResetOrientation();
	FirstPerson::~FirstPerson();
};
#endif
