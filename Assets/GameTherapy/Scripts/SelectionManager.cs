using UnityEngine;

public class SelectionManager : MonoBehaviour
{
    private ISelectable _currentSelection;
    
    public ISelectable CurrentSelection
    {
        get => _currentSelection;
        set
        {
            if (_currentSelection == value)
            {
                return;
            }
            
            _currentSelection?.Deselect();
            _currentSelection = value;
            _currentSelection?.Select();
        }
    }

}
