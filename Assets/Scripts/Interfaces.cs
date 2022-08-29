using UnityEngine;



interface IInteract
{
	void Interact(GameObject go);
}

interface IBorder
{
	void BorderEnter();
	void BorderLeave();
}

interface IReturn
{
	AIdir Return(Direction direction);
}

interface IFinishLine
{
	void Finishline();
}


interface IFinishPart
{
	void Finished(GameObject go);
}

interface IGameOver
{
	void GameOver(GameObject go);
}

public class Interfaces 
{

}
