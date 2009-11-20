#include <OgreSceneNode.h>
#include <OgreSceneManager.h>
#include <OgreVector3.h>
#include <OgreString.h>

#include "WeaponFactory.h"
#include "Weapon.h"
#include "Laser.h"
#include "..\..\Controller\IEvent.h"

WeaponFactory::WeaponFactory( )
{
	
} 


WeaponBase* WeaponFactory::CreateWeapon(Ogre::SceneNode *a_node, Ogre::SceneManager *a_scenemgr, Ogre::Vector3 a_relativePosition, int a_weaponType, Ogre::String a_name, IEvent *a_eventToView, IEvent *a_eventToModel)
{
	switch (a_weaponType)
	{
		case STANDARD:
			return new Weapon(a_node, a_scenemgr, a_relativePosition, a_name);
			break;
		case LASER:
			return new Laser(a_node, a_scenemgr, a_relativePosition, a_name, a_eventToView, a_eventToModel);
			break;
		//case Weapon03:
		//	break;
		default:
			std::cout << "Ingen vapentyp, returnerar Weapon01 " << std::endl; 
			return new Weapon(a_node, a_scenemgr, a_relativePosition, a_name);
			break;
	}
}





WeaponFactory::~WeaponFactory()
{

}


