#ifndef I_Sound_H_
#define I_Sound_H_

class ISound
{
private:

public:	
	ISound::ISound();
	virtual void ISound::MakeLaserHit();
	virtual void ISound::MakeLaserMiss();
	virtual void ISound::MakeStandard();
	virtual void ISound::MakeEnemyDeath();
};
#endif
