#ifndef Perspective_H_
#define Perspective_H_

#include <OgreRoot.h>
#include <OgreSceneManager.h>
#include <OgreCamera.h>
#include <OgreVector2.h>
#include <OgreVector3.h>
#include <OgreQuaternion.h>
#include <OgreRenderWindow.h>
#include <OgreViewport.h>

#include "Camera.h"

class Perspective : public CameraManager
{
private:


public:	
	Perspective::Perspective( Ogre::SceneManager *a_scenemgr, Ogre::RenderWindow *a_window, Ogre::String a_name );
	void Perspective::Update( Ogre::Vector3 a_weaponPosition, Ogre::Quaternion a_weaponOrientation, Ogre::Vector2 a_mousePosition );
	void Perspective::ResetOrientation();
	Perspective::~Perspective();
};
#endif
