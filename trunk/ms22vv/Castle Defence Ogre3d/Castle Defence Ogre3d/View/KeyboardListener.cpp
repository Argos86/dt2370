#include <Ogre.h>
#include <OIS/OIS.h>
#include "KeyboardListener.h"
//#include <OgreEventListeners.h>

KeyboardListener::KeyboardListener(OIS::Keyboard *keyboard) : m_keyboard(keyboard)//, m_keyListener(0)
{
	
}


bool KeyboardListener::frameStarted(const Ogre::FrameEvent& evt)
{
    m_keyboard->capture();
    return !m_keyboard->isKeyDown(OIS::KC_ESCAPE);
}


//bool keyPressed(const OIS::KeyEvent &e) 
//{ 
//	return true; 
//}
//
//bool keyReleased(const OIS::KeyEvent &e) 
//{ 
//	return true; 
//}


KeyboardListener::~KeyboardListener()
{

}