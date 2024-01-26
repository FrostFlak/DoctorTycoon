using System;


[Serializable]
public class Bed
{
    public bool _isBusy;
    
    public bool IsFree()
    {
        if (!_isBusy)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}