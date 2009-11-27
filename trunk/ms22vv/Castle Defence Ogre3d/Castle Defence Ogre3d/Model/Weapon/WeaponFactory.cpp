#include <OgreSceneNode.h>
#include <OgreSceneManager.h>
#include <OgreVector3.h>
#include <OgreString.h>

#include "WeaponFactory.h"
#include "Weapon.h"
#include "Laser.h"
#include "..\..\Controller\IEvent.h"

WeaponFactory::WeaponFactory(IEvent *a_eventToView, IModel *a_eventToModel , ISound *a_soundEffects )
{
	m_eventToView = a_eventToView;
	m_eventToModel = a_eventToModel; 
	m_soundEffects = a_soundEffects;
}

WeaponBase* WeaponFactory::CreateWeapon( Ogre::SceneManager *a_scenemgr, Ogre::Vector3 a_relativePosition, int a_weaponType, Ogre::String a_name)
{
	switch (a_weaponType)
	{
		case STANDARD:
			return new Weapon( a_scenemgr, a_relativePosition, a_name, m_eventToView, m_eventToModel, m_soundEffects);
			break;
		case LASER:
			return new Laser( a_scenemgr, a_relativePosition, a_name, m_eventToView, m_eventToModel, m_soundEffects);
			break;
		//case Weapon03:
		//	break;
		default:
			std::cout << "Ingen vapentyp, returnerar Weapon01 " << std::endl; 
			return new Weapon( a_scenemgr, a_relativePosition, a_name, m_eventToView, m_eventToModel, m_soundEffects);
			break;
	}
}

WeaponFactory::~WeaponFactory()
{

}


