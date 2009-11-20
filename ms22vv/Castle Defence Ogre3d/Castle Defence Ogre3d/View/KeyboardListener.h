#ifndef Keyboard_Listener_H_
#define Keyboard_Listener_H_

#include <Ogre.h>
#include <OIS/OIS.h>
//#include <OgreEventListeners.h>



class KeyboardListener : public Ogre::FrameListener//, public OIS::KeyListener
{
private:
    OIS::Keyboard *m_keyboard;
	OIS::KeyListener *m_keyListener;

public:
	KeyboardListener::KeyboardListener(OIS::Keyboard *keyboard);
	bool frameStarted(const Ogre::FrameEvent& evt);
	KeyboardListener::~KeyboardListener();

	//bool keyPressed(const OIS::KeyEvent &e); 
    //bool keyReleased(const OIS::KeyEvent &e);

};

#endif