using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WireTask : MonoBehaviour
{
    public List<Color> _wiresColors = new List<Color>();
    public List<Wires> _leftWires = new List<Wires>();
    public List<Wires> _rightWires = new List<Wires>();

    private List<Color>  _availableColors;
    private List<int> _availableLeftWireIndex;
    private List<int> _availableRightWireIndex;
    public Wires CurrentDraggedWire;
    public Wires CurrentHoveredWire;
    public bool IsTaskCompleted = false;
    private void Start() 
    {
        _availableColors = new List<Color>(_wiresColors);
        _availableLeftWireIndex = new List<int>();
        _availableRightWireIndex = new List<int>();

        for (int i = 0;i < _leftWires.Count; i++) {_availableLeftWireIndex.Add(i); }
        for (int i = 0; i < _rightWires.Count; i++) { _availableRightWireIndex.Add(i); }
        while(_availableColors.Count > 0 && _availableLeftWireIndex.Count > 0 && _availableRightWireIndex.Count > 0) 
        {
            Color pickedColor = _availableColors[Random.Range (0, _availableColors.Count)];
            int pickedLeftWireIndex = Random.Range (0, _availableLeftWireIndex.Count);
            int pickedRightWireIndex = Random.Range (0, _availableRightWireIndex.Count);

            _leftWires[_availableLeftWireIndex[pickedLeftWireIndex]].SetColor(pickedColor);
            _rightWires[_availableRightWireIndex[pickedRightWireIndex]].SetColor (pickedColor);

            _availableColors.Remove(pickedColor);
            _availableLeftWireIndex.RemoveAt(pickedLeftWireIndex);
            _availableRightWireIndex.RemoveAt(pickedRightWireIndex);
        }
        StartCoroutine(CheckTaskCompletion());
    }
    private IEnumerator CheckTaskCompletion()
    {
        while(!IsTaskCompleted)
        {
            int succssfulWires = 0;
            for(int i = 0; i < _rightWires.Count; i++)
            {
                if(_rightWires[i].isSuccess) {succssfulWires++;}
            } 
            if(succssfulWires >= _rightWires.Count)
            {
                Debug.Log("TASK COMPLETED");
            }
            else
            {
                Debug.Log("TASK NOT COMPLETE");
            }
            yield return new WaitForSeconds(0.5f);
        }
    }
}